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
	public class Trainer : Member
	{
		#region properties
		#endregion

		#region accessors
		#endregion

		#region constructors
		public Trainer(List<object> row) : base(row)
		{
		}
		public Trainer(int id, string name, int teamid) : base(id, name, teamid)
		{
		}
		#endregion

		#region workers
		#endregion
	}
}
