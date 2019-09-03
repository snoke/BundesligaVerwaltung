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
		public Match(int id, int teamId, int opponentId,int score,int opponentScore)
		{
			this.id = id;
			this.teamId = teamId;
			this.opponentId = opponentId;
			this.score = score;
			this.opponentScore = opponentScore;
		}
		
		public Match(List<object> row) {
			
			this.id = Int32.Parse(row[0].ToString());
		//	this.id = (int) Int32.Parse( (string) row[0]);
			this.teamId = Int32.Parse(row[1].ToString());
		//	this.teamId = (int) Int32.Parse( (string) row[1]);
			this.opponentId = Int32.Parse(row[2].ToString());
		//	this.opponentId = (int) Int32.Parse( (string) row[2]);
			this.score = Int32.Parse(row[3].ToString());
		//	this.score = (int) Int32.Parse( (string) row[3]);
			this.opponentScore = Int32.Parse(row[4].ToString());
		//	this.opponentScore = (int) Int32.Parse( (string) row[4]);
		}
		#endregion
		
		#region workers
		public override List<string[]> GetKeys() {
			List<string[]> keys =base.GetKeys();
			keys.Add(new string[]{"teamid","INTEGER"});
			keys.Add(new string[]{"opponentscore","INTEGER"});
			keys.Add(new string[]{"score","INTEGER"});
			keys.Add(new string[]{"opponentid","INTEGER"});
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
