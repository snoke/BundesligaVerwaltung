/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 18:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using BundesligaVerwaltung.Model;
using BundesligaVerwaltung.Repository.DataStorage;
using BundesligaVerwaltung.Model.Members;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Data.SQLite;

namespace BundesligaVerwaltung.Repository
{
	public class EntityRepository
	{
		#region properties
		DataStorage.DataStorage dataStorage;
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
		public EntityRepository(DataStorage.DataStorage dataStorage)
		{
			this.dataStorage = dataStorage;
			
			
			//dbConnection.ChangePassword("myPassword");
			//dbConnection.ChangePassword(String.Empty);
		}
		#endregion
		
		#region workers
//		private void execute(string sql) {
//			SQLiteCommand Command = new SQLiteCommand(sql, this.dbConnection);
//			Command.ExecuteNonQuery();
//			Command.Dispose();
//		}
		
		public  void SaveTeams(List<Team> elements) {
			
		}
		public  void SaveMatches(List<Match> elements) {
			
		}
		public  void SaveMembers(List<Member> elements) {
			
		}
		public  List<Match> LoadMatches() {
			List<Match> matches = new List<Match>();
			foreach(Match row in  this.dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Match"))) {
				matches.Add(row);
			}
			return matches;
		}
		
		
		public  List<Team> LoadTeams() {
			List<Match> matches = this.LoadMatches().ToList();
			List<Team> teams = new List<Team>();
			foreach(Team team in this.dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Team"))) {
				team.Matches = matches.Where(o => o.teamId == team.id  || o.opponentId == team.id).ToList();
				teams.Add(team);
			}
			return teams;
		}
		
		
		public  List<Member> LoadMembers() {
			List<Member> members = new List<Member>();
			foreach(Member row in  this.dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Member"))) {
				members.Add(row);	
			}
			return members;
		}
		#endregion
	}
}
