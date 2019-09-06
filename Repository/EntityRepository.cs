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
using BundesligaVerwaltung.Model;

namespace BundesligaVerwaltung.Repository
{
	public class EntityRepository
	{
		#region properties
		private DataStorage.DataStorage _dataStorage;
		private List<Team> _teams;
		private List<Member> _members;
		private List<Match> _matches;
		#endregion

		#region accessors
		private DataStorage.DataStorage dataStorage
		{
			get { return _dataStorage; }
			set { _dataStorage = value; }
		}

		public List<Team> Teams
		{
			get
            {
                //lazy loading
                if (_teams == null)
                {
                    _teams = this.Load(Type.GetType("BundesligaVerwaltung.Model.Team")).Cast<Team>().ToList();
                }
                else { }
                return _teams;
			}
			set { _teams = value; }
		}
		public List<Member> Members
		{
			get
            {
                //lazy loading
                if (_members == null)
                {
                    _members = this.Load(Type.GetType("BundesligaVerwaltung.Model.Member")).Cast<Member>().ToList();
                }
                else { }
                return _members;
			}
			set { _members = value; }
		}
		public List<Match> Matches
		{
			get
			{
                //lazy loading
                if (_matches == null)
                {
                    _matches = this.Load(Type.GetType("BundesligaVerwaltung.Model.Match")).Cast<Match>().ToList();
                }
                return _matches;
            }
            set { _matches = value; }
        }
		#endregion


		#region constructors
		public EntityRepository(DataStorage.DataStorage dataStorage)
		{
			this.dataStorage = dataStorage;
		}
        #endregion

        #region workers
        public void Reload()
        {
            this.Members = null;
            this.Teams = null;
            this.Matches = null;
        }
        private void Reload(Type entityType)
        {
            if (entityType.Name == "Member")
            {
                this.Members = null;
            }
            else if (entityType.Name == "Team")
            {

                this.Teams = null;
            }
            else if (entityType.Name == "Match")
            {

                this.Matches = null;
            } else
            {

            }
        }
        public List<Entity> Load(Type entityType)
        {
            return dataStorage.LoadEntities(entityType);
        }
        public void Remove(Entity entity)
        {
            dataStorage.RemoveEntity(entity);
            Reload(entity.GetType());
        }
        public void Save(Entity entity)
        {
            dataStorage.SaveEntity(entity);
            Reload(entity.GetType());
        }
        #endregion
    }
}
