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
using System.Linq;
using System.Collections.Generic;

namespace BundesligaVerwaltung.Repository
{
	public class EntityRepository
	{
		
		#region properties
			DataStorage.DataStorage _dataStorage;
			List<Team> _teams;
			List<Member> _members;
			List<Match> _matches;
		#endregion
		
		
		#region accessors
			DataStorage.DataStorage dataStorage {
				get { return this._dataStorage; }
				set { this._dataStorage = value; }
			}
			
			public List<Team> teams {
				get { 
					if (this._teams==null) {
						this._teams=this.LoadTeams();
					}
					return this._teams; 
				}
				set { this._teams = value; }
			}
			public List<Member> members {		
				get { 
					if (this._members==null) {
						this._members=this.LoadMembers();
					}
					return this._members; 
				}
				set { this._members = value; }
			}
			public List<Match> matches {		
				get { 
					if (this._matches==null) {
						this._matches=this.LoadMatches();
					}
					return this._matches; 
				}
				set { this._matches = value; }
			}
		#endregion
		
		
		#region constructors
		public EntityRepository(DataStorage.DataStorage dataStorage)
		{
			this.dataStorage = dataStorage;
		}
		#endregion
		
		#region workers
		public virtual void SaveTeams(List<Team> elements) {
			foreach(Team element in elements) {
				this.dataStorage.SaveEntity(element);
			}
			
		}
		public virtual void SaveMatches(List<Match> elements) {
			foreach(Match element in elements) {
				this.dataStorage.SaveEntity(element);
			}
			
		}
		public virtual  void SaveMembers(List<Member> elements) {
			foreach(Member element in elements) {
				this.dataStorage.SaveEntity(element);
			}
		}
		public virtual List<Match> LoadMatches() {
			List<Match> matches = new List<Match>();
			foreach(Match row in  this.dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Match"))) {
				matches.Add(row);
			}
			return matches;
		}
		
		public virtual List<Team> LoadTeams() {
			List<Match> matches = this.LoadMatches().ToList();
			List<Team> teams = new List<Team>();
			foreach(Team team in this.dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Team"))) {
				team.Matches = matches.Where(o => (o.teamId == team.id) || (o.opponentId == team.id)).ToList();
				teams.Add(team);
			}
			return teams;
		}
		
		public virtual List<Member> LoadMembers() {
			List<Member> members = new List<Member>();
			foreach(Member row in  this.dataStorage.LoadEntities(Type.GetType("BundesligaVerwaltung.Model.Member"))) {
				members.Add(row);	
			}
			return members;
		}
		#endregion
	}
}
