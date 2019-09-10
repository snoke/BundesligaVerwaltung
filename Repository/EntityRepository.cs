/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 18:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BundesligaVerwaltung.Migration;
using BundesligaVerwaltung.Model;
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.Repository.DataStorage;

namespace BundesligaVerwaltung.Repository
{
    public class EntityRepository
    {
        #region properties
        private DataStorage.EntityRepository _dataStorage;
        private Dictionary<string, Type> _types;
        private Dictionary<Type, int> _nextId;
        private Dictionary<Type, List<Entity>> _entities;

        //Der Value eines jeden Mementos enthält den "alive"-Zustand und bestimmt ob diese objekt gespeichert oder gelöscht wird
        private List<Dictionary<Entity,bool>> _mementos; 

        #endregion

        #region accessors
        private List<Dictionary<Entity,bool>> Mementos //Logs von Entityzuständen
        {
            get { return _mementos; }
            set { _mementos = value; }
        }
        private Dictionary<string, Type> Types
        {
            get { return _types; }
            set { _types = value; }
        }
        private Dictionary<Type,int> NextId
        {
            get { return _nextId; }
            set { _nextId = value; }
        }
        public Dictionary<Type, List<Entity>> Entities
        {
            get
            {
                return _entities;
            }
            set { _entities = value; }
        }

        private DataStorage.EntityRepository dataStorage
        {
            get { return _dataStorage; }
            set { _dataStorage = value; }
        }
        #endregion


        #region constructors
        public EntityRepository(Dictionary<string, Type> types, bool debug)
        {
            dataStorage = new SQLiteStrategy("db.sqlite", types, debug);
            Types = types;
            Entities = new Dictionary<Type, List<Entity>>();
            NextId = new Dictionary<Type,int>();
            Mementos = new List<Dictionary<Entity,bool>>();
            foreach (KeyValuePair<string, Type> entry in Types)
            {
                NextId[entry.Value] = dataStorage.GetNextId(entry.Value);
                //NextId[entry.Value] = 1;
                Entities[entry.Value] = new List<Entity>();
            }
        }
        #endregion

        #region workers
        private int GetNextId(Type eType)
        {
            int id = NextId[eType];
            NextId[eType]++;
            return id;
        }
        public void Pull()
        {
            foreach (KeyValuePair<string, Type> entry in Types)
            {
                Entities[entry.Value] = new List<Entity>();
                Load(entry.Value);
            }
        }
        public void Flush() //Speichert den letzten Zustand aller geänderten Objekte in die Datenbank und leert die Mementos
        {
            foreach (Dictionary<Entity,bool> dictionary in Mementos)
            {
                List<int> idHandled = new List<int>();
                foreach (KeyValuePair<Entity,bool> entry in dictionary.Reverse())
                {

                    Entity entity = entry.Key;
                    bool alive = entry.Value;
                    if (entity.id!=null && idHandled.Contains((int)entity.id))
                    {
                        continue;
                    } else if (alive == false)
                    {
                        this.dataStorage.RemoveEntity(entity);
                        idHandled.Add((int)entity.id);
                        Entities[entry.Value.GetType()].Remove(entity);
                    }
                    else
                    {
                        entity.id = this.dataStorage.SaveEntity(entity);
                        idHandled.Add((int)entity.id);
                    }
                }
            }
            Mementos = new List<Dictionary<Entity, bool>>();
        }

        private Entity CreateInstance(Type entityType, List<object> parameters)
        {
            Entity entity = (Entity)Activator.CreateInstance(entityType, parameters.ToArray());
            return SetEntity(entity); 
        }
        private void Load(Type entityType)
        {
            List<List<string>> rows = dataStorage.LoadEntities(entityType);
            List<PropertyInfo> properties = entityType.GetProperties().Reverse().ToList();
            //reflection lädt die erweiternden eigenschaften zuerst und die geerbten eigenschaften (id !!!) zuletzt!
            //die sonstige reihenfolge bleibt dabei bestehen
            //TODO:anderes matching für tiefere abstraktion
            List<PropertyInfo> _properties = new List<PropertyInfo>
            {
                properties[0]
            };
            properties.Reverse();
            _properties.AddRange(properties);
            _properties.RemoveAt(_properties.Count() - 1);

            foreach (List<string> row in rows)
            {
                List<object> values = new List<object>();
                for (int i = 0; i < row.Count(); i++)
                {
                    PropertyInfo property = _properties[i];

                    string type = property.PropertyType.ToString();
                    if (property.Name == "id")
                    {
                        values.Add((int?)Int32.Parse(row[i]));
                    }
                    else if (type == "System.String")
                    {
                        values.Add(row[i]);
                    }
                    else if (type == "System.Int32")
                    {
                        values.Add(Int32.Parse(row[i]));
                    }
                    else if (type == "System.Boolean")
                    {
                        values.Add(Boolean.Parse(row[i]));
                    }
                    else if (Entities.Any(o => o.Key.FullName == type))
                    {
                        string val = row[i];
                        if (val == "") //null objects
                        {
                            values.Add(null);
                        }
                        else
                        {
                            int mapId = Int32.Parse(val);
                            values.Add(Entities[Type.GetType(type)].Single(x => x.id == mapId));
                        }
                    }
                    else
                    {
                        throw new ArgumentException("(ORM) Mapping failed of type " + type);
                    }
                }
                CreateInstance(entityType, values);
            }
        }

        private Entity SetEntity(Entity entity)
        {
            if (entity.id!=null && Entities[entity.GetType()].Any(x => x.id == entity.id))
            {
                int index = Entities[entity.GetType()].FindIndex(x => x.id == entity.id);
                Entities[entity.GetType()][index] = entity;
                return Entities[entity.GetType()][index];
            }
            else
            {
                if (entity.id == null)
                {
                    entity.id = GetNextId(entity.GetType());
                } else
                {

                }
                Entities[entity.GetType()].Add(entity);
                return Entities[entity.GetType()].Last();
            }
        }
        public Entity Save(Entity entity)
        {
            entity = SetEntity(entity);
            Mementos.Add(new Dictionary< Entity, bool>()
                {
                    { entity.Clone(),true },
                }
            );
            //this.dataStorage.SaveEntity(entity);
            return entity;
        }
        public void Remove(Entity entity)
        {
            dataStorage.RemoveEntity(entity);
            Entities[entity.GetType()].Remove(entity);

            Mementos.Add(new Dictionary<Entity, bool>()
                {
                    { entity,false},
                }
            );
            Flush();
            //this.dataStorage.RemoveEntity(entity);
        }
        public void CreateSchema(Type entityType)
        {
            dataStorage.CreateSchema(entityType);
        }
        public void DefaultMigration()
        {

            new SchemaMigration(dataStorage, Types).up();
            Save(new Role((int?)null, "Spieler"));
            Save(new Role((int?)null, "Trainer"));
            Save(new Role((int?)null, "Physio"));
            Save(new League((int?)null, "Bundesliga", 18));
            List<Team> teams = Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Team")].Cast<Team>().ToList();
            List<League> leagues = Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.League")].Cast<League>().ToList();
            List<Role> roles = Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Role")].Cast<Role>().ToList();
            new TeamsMigration(this, teams, leagues).up();
            new PlayersMigration(this, Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Team")].Cast<Team>().ToList(), Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Role")].Cast<Role>().ToList()).up();
           
            new SpieltagEinsMigration(this, Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Team")].Cast<Team>().ToList()).up();

            Flush();
        }
        #endregion
    }
}
