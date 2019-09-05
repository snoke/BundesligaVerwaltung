/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 20:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.Generic;

namespace BundesligaVerwaltung.Model.Members
{
	public class Player : Member
	{
		#region properties
		#endregion

		#region accessors
		#endregion

		#region constructors
		public Player(List<object> row) : base(row)
		{
		}
        public Player(Member member) : base(member.id, member.Name, member.Teamid, "Player")
        {
        }
        public Player(int id, string name, int teamid) : base(id, name, teamid, "Player")
		{
		}
		#endregion

		#region workers
		#endregion
	}
}
