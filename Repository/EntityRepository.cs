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
using BundesligaVerwaltung.Model.Members;

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
				if (_teams == null)
				{
					_teams = LoadTeams();
				}
				return _teams;
			}
			set { _teams = value; }
		}
		public List<Member> Members
		{
			get
			{
				if (_members == null)
				{
					_members = LoadMembers();
				}
				return _members;
			}
			set { _members = value; }
		}
		public List<Match> Matches
		{
			get
			{
				if (_matches == null)
				{
					_matches = LoadMatches();
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
		public void SaveTeams(List<Team> elements)
		{
			foreach (Team element in elements)
			{
				dataStorage.SaveEntity(element);
			}

		}
		public void SaveMatches(List<Match> elements)
		{
			foreach (Match element in elements)
			{
				dataStorage.SaveEntity(element);
			}

		}
		public void SaveMembers(List<Member> elements)
		{
			foreach (Member element in elements)
			{
				dataStorage.SaveEntity(element as Member);
			}
		}
		public List<Match> LoadMatches()
		{
            return dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Match")).Cast<Match>().OrderBy(x => x.id).ToList();
		}

		public List<Team> LoadTeams()
		{
			List<Match> matches = LoadMatches();
            List<Team> teams = dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Team")).Cast<Team>().ToList();
			foreach (Team team in teams)
			{
				team.Matches = matches.Where(o => (o.TeamId == team.id) || (o.OpponentId == team.id)).OrderBy(x => x.id).ToList();
			}
			return teams.OrderBy(x => x.id).ToList();
		}

		public List<Member> LoadMembers()
        {
            List<Member> members = new List<Member>();
            members.AddRange(dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Members.Physio")).Cast<Member>().ToList());
            members.AddRange(dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Members.Player")).Cast<Member>().ToList());
            members.AddRange(dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Members.Trainer")).Cast<Member>().ToList());

            return members;
		}
		#endregion
	}
}
