using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using BundesligaVerwaltung.Model;
namespace BundesligaVerwaltung.Model
{
	public class Team:Entity
    {    	
		public string name;
		public List<Match> Matches = new List<Match>();

		public Team(List<object> row):base(row) {
			this.name = (string) row[1];
		}
		public Team(int id, string name):base(id) {
			this.name = name;
		}
		public string GetName() {
			return this.name;
		}
		public List<Match> GetDraws() {
			return  this.Matches.Where(x => (x.score == x.opponentScore)).ToList();
		}
		public List<Match> GetMatches() {
			return  this.Matches;
		}
		public List<Match> GetWins() {
			return  this.Matches.Where(x => (x.teamId == this.id && x.score > x.opponentScore) || (x.opponentId==this.id && x.score < x.opponentScore)).ToList();
		}
		public List<Match> GetLosses() {
			return  this.Matches.Where(x => (x.teamId == this.id && x.score > x.opponentScore) || (x.opponentId==this.id && x.score < x.opponentScore)).ToList();
		}
		public int GetGoals() {
			int goals = 0;
			foreach(Match match in this.Matches.Where(x => (x.teamId == this.id))) {
				goals += match.score;
			}
			//List<Match
			foreach(Match match in this.Matches.Where(x => (x.opponentId == this.id))) {
				goals += match.opponentScore;
			}
			return goals;
		}
		public int GetPoints() {
			//List<Match> matches =  this.Matches.Where(x => (x.teamId == this.id) || (x.opponentId == this.id)).ToList();
			return this.GetDraws().Count() + 3 * this.GetWins().Count();
		}
		public override List<string[]> GetKeys() {
			List<string[]> keys = base.GetKeys();
			keys.Add(new string[]{"name","STRING"});
			return keys;
		}
		
		public override List<object> GetValues() {
			List<object> values = base.GetValues();
			values.Add(this.name);
			return values;
		}
    }
}

