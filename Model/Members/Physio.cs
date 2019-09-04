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
	public class Physio : Member
	{
		#region properties
		#endregion

		#region accessors
		#endregion

		#region constructors
		public Physio(int id, string name, int teamid) : base(id, name, teamid)
		{
		}
		public Physio(List<object> row) : base(row)
		{
		}
		#endregion

		#region workers
		#endregion
	}
}
