/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 01.09.2019
 * Time: 00:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BundesligaVerwaltung.Model
{
	public class Match:Entity
	{
		#region properties
		public int teamId;
		public int opponentId;
		public int score;
		public int opponentScore;
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
		public Match(int id, int teamId, int opponentId,int score,int opponentScore):base(id)
		{
			this.teamId = teamId;
			this.opponentId = opponentId;
			this.score = score;
			this.opponentScore = opponentScore;
		}
		
		public Match(List<object> row) :base(row)
		{
			this.teamId = Int32.Parse(row[1].ToString());
			this.opponentId = Int32.Parse(row[2].ToString());
			this.score = Int32.Parse(row[3].ToString());
			this.opponentScore = Int32.Parse(row[4].ToString());
		}
		#endregion
		
		#region workers
		public override List<string[]> GetKeys() {
			List<string[]> keys =base.GetKeys();
			keys.Add(new[]{"teamid","INTEGER"});
			keys.Add(new[]{"opponentscore","INTEGER"});
			keys.Add(new[]{"score","INTEGER"});
			keys.Add(new[]{"opponentid","INTEGER"});
			return keys;
		}
		
		public override List<object> GetValues() {
			List<object> values = base.GetValues();
			values.Add(this.teamId);
			values.Add(this.opponentScore);
			values.Add(this.score);
			values.Add(this.opponentId);
			return values;
		}
		#endregion
	}
}
