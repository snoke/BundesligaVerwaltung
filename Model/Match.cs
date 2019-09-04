/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 01.09.2019
 * Time: 00:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace BundesligaVerwaltung.Model
{
	public class Match : Entity
	{
		#region properties
		private int teamId;
		private int opponentId;
		private int score;
		private int opponentScore;
		#endregion

		#region accessors
		public int TeamId
		{
			get { return teamId; }
			set { teamId = value; }
		}
		public int OpponentId
		{
			get { return opponentId; }
			set { opponentId = value; }
		}
		public int Score
		{
			get { return score; }
			set { score = value; }
		}
		public int OpponentScore
		{
			get { return opponentScore; }
			set { opponentScore = value; }
		}
		#endregion

		#region constructors
		public Match(int id, int teamId, int opponentId, int score, int opponentScore) : base(id)
		{
			TeamId = teamId;
			OpponentId = opponentId;
			Score = score;
			OpponentScore = opponentScore;
		}

		public Match(List<object> row) : base(row)
		{
			TeamId = Int32.Parse(row[1].ToString());
			OpponentId = Int32.Parse(row[2].ToString());
			Score = Int32.Parse(row[3].ToString());
			OpponentScore = Int32.Parse(row[4].ToString());
		}
		#endregion

		#region workers
		public override List<string[]> GetKeys()
		{
			List<string[]> keys = base.GetKeys();
			keys.Add(new[] { "teamid", "INTEGER" });
			keys.Add(new[] { "opponentscore", "INTEGER" });
			keys.Add(new[] { "score", "INTEGER" });
			keys.Add(new[] { "opponentid", "INTEGER" });
			return keys;
		}

		public override List<object> GetValues()
		{
			List<object> values = base.GetValues();
			values.Add(TeamId);
			values.Add(OpponentScore);
			values.Add(Score);
			values.Add(OpponentId);
			return values;
		}
		#endregion
	}
}
