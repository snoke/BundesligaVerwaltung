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
using BundesligaVerwaltung.Model.Members;
namespace BundesligaVerwaltung.Model
{
	public abstract class Member : Entity
	{
		#region properties
		private string name;
		private int teamid;
		private string role;
		#endregion

		#region accessors
		public string Name { get => name; set => name = value; }
		public int Teamid { get => teamid; set => teamid = value; }
		#endregion

		#region constructors
		public Member(int id, string name, int teamid,string role) : base(id)
		{
			Name = name;
			Teamid = teamid;
        }

		public Member(List<object> row) : base(row)
		{
            Name = (string)row[1];
            Teamid = Int32.Parse(row[2].ToString());
		}
		#endregion

		#region workers
		public  override List<string[]> GetKeys()
		{
			List<string[]> keys = base.GetKeys();
			keys.Add(new string[] { "name", "STRING" });
			keys.Add(new string[] { "teamid", "INTEGER" });
			keys.Add(new string[] { "role", "STRING" });
			return keys;
		}

		public override List<object> GetValues()
		{
			List<object> values = base.GetValues();
			values.Add(name);
			values.Add(teamid);
			values.Add(role);
			return values;
		}
		#endregion
	}
}
