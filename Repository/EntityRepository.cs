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

		public List<Team> teams
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
		public List<Member> members
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
		public List<Match> matches
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
				dataStorage.SaveEntity(element);
			}
		}
		public List<Match> LoadMatches()
		{
			List<Match> matches = new List<Match>();
			foreach (Match row in dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Match")).OrderBy(x => x.id))
			{
				matches.Add(row);
			}
			return matches.OrderBy(x => x.id).ToList();
		}

		public List<Team> LoadTeams()
		{
			List<Match> matches = LoadMatches().ToList();
			List<Team> teams = new List<Team>();
			foreach (Team team in dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Team")))
			{
				team.Matches = matches.Where(o => (o.TeamId == team.id) || (o.OpponentId == team.id)).ToList();
				teams.Add(team);
			}
			return teams.OrderBy(x => x.id).ThenBy(x => x.GetPoints()).ToList();
		}

		public List<Member> LoadMembers()
		{
			List<Member> members = new List<Member>();
			foreach (Member row in dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Member")).OrderBy(x => x.id))
			{
				members.Add(row);
			}
			return members.OrderBy(x => x.id).ToList();
		}
		#endregion
	}
}
