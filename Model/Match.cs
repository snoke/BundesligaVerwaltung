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
using System.Xml.Linq;

namespace BundesligaVerwaltung.Model
{
	public class Match
	{
		#region properties
		public int id;
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

		public Match(XElement element) {
			Int32.TryParse(element.Element("id").Value, out this.id );
			Int32.TryParse(element.Element("teamid").Value, out this.teamId );
			Int32.TryParse(element.Element("opponentid").Value, out this.opponentId );
			Int32.TryParse(element.Element("score").Value, out this.score );
			Int32.TryParse(element.Element("opponentscore").Value, out this.opponentScore );
			
		}
		#endregion
		
		#region workers
		#endregion
	}
}
