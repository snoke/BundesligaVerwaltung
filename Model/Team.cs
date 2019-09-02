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
	public class Team
    {    	
		public const int MAXIMUM_PARTICIPANTS = 18;
		
		public int id;
		public string name;
		public List<Match> Matches = new List<Match>();
		public Team(XElement element) {
			Int32.TryParse(element.Element("id").Value, out this.id );
			this.name = element.Element("name").Value;
		}
		public Team(int id, string name) {
			this.id = id;
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
    }
}

