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
    //Todo: Entities im Repository bis zu einem Flush speichern. evtl Statusüberwachung mittels Memento Pattern
    public class EntityRepository
    {
        #region properties
        private DataStorage.DataStorage _dataStorage;
        private Dictionary<string, Type> _types;
        private Dictionary<Type, List<Entity>> _entities;

        #endregion

        #region accessors
        private Dictionary<string, Type> Types
        {
            get { return _types; }
            set { _types = value; }
        }
        public Dictionary<Type, List<Entity>> Entities
        {
            get
            {
                return _entities;
            }
            set { _entities = value; }
        }

        private DataStorage.DataStorage dataStorage
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
            Refresh();
        }
        #endregion

        #region workers
        public void Refresh()
        {
            Entities = new Dictionary<Type, List<Entity>>();
            foreach (KeyValuePair<string, Type> entry in Types)
            {
                Entities.Add(entry.Value, Load(entry.Value));
            }
        }
        public List<Entity> Load(Type entityType)
        {
            List<List<string>> rows = dataStorage.LoadEntities(entityType);
            List<Entity> list = new List<Entity>();
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
                List<object> parameters = values.ToList();
                list.Add((Entity)Activator.CreateInstance(entityType, parameters.ToArray()));
            }
            return list;
        }
        public void Remove(Entity entity)
        {
            dataStorage.RemoveEntity(entity);
        }
        public void Save(Entity entity)
        {
            dataStorage.SaveEntity(entity);
        }
        public void CreateSchema(Type entityType)
        {
            dataStorage.CreateSchema(entityType);
        }
        public void DefaultMigration()
        {
            new SchemaMigration(dataStorage, Types).up();
            Refresh();
            List<Role> roles = Entities[Type.GetType("BundesligaVerwaltung.Model.Role")].Cast<Role>().ToList();
            List<Team> teams = Entities[Type.GetType("BundesligaVerwaltung.Model.Team")].Cast<Team>().ToList();
            List<League> leagues = Entities[Type.GetType("BundesligaVerwaltung.Model.League")].Cast<League>().ToList();
            new TeamsMigration(dataStorage, teams, leagues).up();
            Refresh();
            teams = Entities[Type.GetType("BundesligaVerwaltung.Model.Team")].Cast<Team>().ToList();
            new PlayersMigration(dataStorage, teams, roles).up();
            new SpieltagEinsMigration(dataStorage, teams).up();
        }
        #endregion
    }
}
