/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 20:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
namespace BundesligaVerwaltung.Model
{
	public  class Member : Entity
	{
		#region properties
		private string name;
		private int teamid;
		private string role;
		#endregion

		#region accessors
		public string Name { get => name; set => name = value; }
		public int Teamid { get => teamid; set => teamid = value; }
        public string Role { get => role; set => role = value; }
        #endregion

        #region constructors
        public Member(int? id, string name, int teamid,string role) : base(id)
		{
			Name = name;
			Teamid = teamid;
            Role = role;
        }

		public Member(List<object> row) : base((int)Int32.Parse(row[0].ToString()))
        {
            Name = (string)row[1];
            Teamid = Int32.Parse(row[2].ToString());
            Role = (string)row[3];
        }
		#endregion

		#region workers
		#endregion
	}
}
