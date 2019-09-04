using System.Collections.Generic;
using System.Linq;
namespace BundesligaVerwaltung.Model
{
	public class Team : Entity
	{
		#region properties
		private string name;
		private List<Match> matches = new List<Match>();
		#endregion

		#region accessors
		public string Name { get => name; set => name = value; }
		public List<Match> Matches { get => matches; set => matches = value; }
		#endregion

		#region constructors
		public Team(List<object> row) : base(row)
		{
			Name = (string)row[1];
		}
		public Team(int id, string name) : base(id)
		{
			Name = name;
		}
		#endregion

		#region workers
		public string GetName()
		{
			return Name;
		}
		public List<Match> GetDraws()
		{
			return matches.Where(x => (x.Score == x.OpponentScore)).ToList();
		}
		public List<Match> GetMatches()
		{
			return matches;
		}
		public List<Match> GetWins()
		{
			return matches.Where(x => (x.TeamId == id && x.Score > x.OpponentScore) || (x.OpponentId == id && x.Score < x.OpponentScore)).ToList();
		}
		public List<Match> GetLosses()
		{
			return matches.Where(x => (x.TeamId == id && x.Score < x.OpponentScore) || (x.OpponentId == id && x.Score > x.OpponentScore)).ToList();
		}
		public int GetGoals()
		{
			int goals = 0;
			foreach (Match match in matches.Where(x => (x.TeamId == id)))
			{
				goals += match.Score;
			}
			//List<Match
			foreach (Match match in matches.Where(x => (x.OpponentId == id)))
			{
				goals += match.OpponentScore;
			}
			return goals;
		}
		public int GetPoints()
		{
			//List<Match> matches =  this.Matches.Where(x => (x.teamId == this.id) || (x.opponentId == this.id)).ToList();
			return GetDraws().Count() + 3 * GetWins().Count();
		}
		public override List<string[]> GetKeys()
		{
			List<string[]> keys = base.GetKeys();
			keys.Add(new string[] { "name", "STRING" });
			return keys;
		}

		public override List<object> GetValues()
		{
			List<object> values = base.GetValues();
			values.Add(Name);
			return values;
		}
		#endregion
	}
}

