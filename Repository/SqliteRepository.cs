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
using BundesligaVerwaltung.Model.Members;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Data.SQLite;

namespace BundesligaVerwaltung.Repository
{
	public class SqliteRepository:IRepository
	{
		#region properties
		SQLiteConnection dbConnection;
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
		private void migrate() {
			this.query("DROP TABLE IF EXISTS Team");
			this.query("DROP TABLE IF EXISTS Match");
			this.query("DROP TABLE IF EXISTS Member");
			this.query(
		            @"CREATE TABLE IF NOT EXISTS Match(
	             		id 		INTEGER PRIMARY KEY AUTOINCREMENT,
						teamid INTEGER,
						opponentscore INTEGER,
						score INTEGER,
						opponentid INTEGER
		            );"
		    );
			this.query(
		            @"CREATE TABLE IF NOT EXISTS Team(
			             id INTEGER PRIMARY KEY AUTOINCREMENT,
			             name TEXT
		            );"
		    );
			this.query(
		            @"CREATE TABLE IF NOT EXISTS Member(
	             		id 		INTEGER PRIMARY KEY AUTOINCREMENT,
				  		name 	STRING,
					  	teamid 	INTEGER,
					  	role 	STRING
		            );"
		    );
		}
		public SqliteRepository()
		{
			this.dbConnection = new SQLiteConnection("Data Source = db.sqlite; Version = 3;");
			this.dbConnection.Open();
			
			
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
		private List<List<object>> query(string sql) {
			SQLiteCommand Command = new SQLiteCommand(sql, this.dbConnection);
			SQLiteDataReader reader = Command.ExecuteReader();
			
			List<List<object>> rows = new List<List<object>>();
			while (reader.Read())
			{
				List<object> row = new List<object>();
				for(int i = 0;i<reader.FieldCount;i++) {
					row.Add(reader[i]);
				}
				rows.Add(row);
				//Console.WriteLine(reader.FieldCount);
				//Console.WriteLine("Dies ist der {0}. eingefügte Datensatz mit dem Wert: \"{1}\"", reader[0].ToString(), reader[1].ToString());
			}
			 
			// Beenden des Readers und Freigabe aller Ressourcen.
			reader.Close();
			reader.Dispose();
			Command.Dispose();
			return rows;
		}
		
		
		
		public override void SaveTeams(List<Team> elements) {
			
		}
		public override void SaveMatches(List<Match> elements) {
			
		}
		public override void SaveMembers(List<Member> elements) {
			
		}
		public override List<Match> LoadMatches() {
			List<Match> matches = new List<Match>();
			foreach(List<object> row in  this.LoadEntities("Match")) {
				matches.Add(new Match(row));
			}
			return matches;
		}
		
		
		public override List<Team> LoadTeams() {
			List<Team> teams = new List<Team>();
			List<Match> matches = this.LoadMatches().ToList();
			foreach(List<object> row in  this.LoadEntities("Team")) {
				Team team = new Team(row);
				team.Matches = matches.Where(o => o.teamId == team.id  || o.opponentId == team.id).ToList();
				teams.Add(team);
			}
			return teams;
		}
		
		public override List<Member> LoadMembers() {
			List<Member> members = new List<Member>();
			foreach(List<object> row in  this.query("SELECT * FROM Member;")) {
				members.Add(new Member(row));	
			}
			return members;
		}
		
		public  void SaveEntities(Entity[] entities) {
			foreach(Entity e in entities) {
				string sql = "REPLACE INTO " +e.GetClassName()+"(";
				List<string[]> keys = e.GetKeys();
				foreach(string[] k in keys) {
					sql +=  k[0] + ",";
				}
				sql = sql.Substring(0,sql.Length-1) + ") VALUES(";
				int i = 0;
				foreach(object v in e.GetValues()) {
					if (keys[i][1]=="STRING") {
						sql += "'" + (string) v.ToString() + "',";
					} else {
						sql += (string) v.ToString() + ",";
					}
					i++;
				}
				sql = sql.Substring(0,sql.Length-1) + ");";
				this.query(sql);
				Console.WriteLine(sql);
			}
		Console.ReadLine();
		}
		
		public  List<object> LoadEntities(string name) {
			List<object> list = new List<object>();
			foreach(List<object> row in  this.query("SELECT * FROM "+name+";")) {
				list.Add(row);
			}
			return list;
		}
		#endregion
	}
}
