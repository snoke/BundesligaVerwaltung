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
namespace BundesligaVerwaltung.Model
{
	public abstract class Member
	{
		#region properties
		public int id;
		public string name;
		public int teamid;
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
		
		public Member(int id, string name,int teamid){
			this.id = id;
			this.name = name;
			this.teamid = teamid;
		}
		public Member(XElement element)
		{
			Int32.TryParse(element.Element("id").Value, out this.id );
			this.name = element.Element("name").Value;
			Int32.TryParse(element.Element("teamid").Value, out this.teamid );
		}
		#endregion
		
		#region workers
		#endregion
	}
}
