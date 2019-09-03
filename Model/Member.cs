/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 20:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
namespace BundesligaVerwaltung.Model
{
	public class Member:Entity
	{
		#region properties
		public string name;
		public int teamid;
		public string role;
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
		public Member(int id, string name,int teamid){
			this.id = id;
			this.name = name;
			this.teamid = teamid;
		}
		
		public Member(List<object> row)
		{
			this.id = (int) Int32.Parse( (string) row[0]);
			this.name = (string) row[1];
			this.teamid = (int) Int32.Parse( (string) row[2]);
			this.role = (string) row[3];
		}
		#endregion
		
		#region workers
		
		public override List<string[]> GetKeys() {
			List<string[]> keys =base.GetKeys();
			keys.Add(new string[]{"name","STRING"});
			keys.Add(new string[]{"teamid","INTEGER"});
			keys.Add(new string[]{"role","STRING"});
			return keys;
		}
		
		public override  List<object> GetValues() {
			List<object> values = base.GetValues();
			values.Add(this.name);
			values.Add(this.teamid);
			values.Add(this.role);
			return values;
		}
		#endregion
	}
}
