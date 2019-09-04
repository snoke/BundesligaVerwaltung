/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 02.09.2019
 * Time: 23:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace BundesligaVerwaltung.Model
{
	public class Entity
	{
		#region properties
		public int _id;
		#endregion

		#region accessors
		public int id
		{
			get { return _id; }
			set { _id = value; }
		}
		#endregion

		#region constructors
		public Entity(int id)
		{
			init(id);
		}
		public Entity(List<object> row)
		{
			init(Int32.Parse(row[0].ToString()));
		}
		#endregion

		#region workers
		private void init(int id)
		{
			this.id = id;
		}

		public virtual List<string[]> GetKeys()
		{
			List<string[]> keys = new List<string[]>();
			keys.Add(new string[] { "id", "INTEGER" });
			return keys;
		}
		public virtual List<object> GetValues()
		{
			List<object> values = new List<object>();
			values.Add(id);
			return values;
		}
		#endregion
	}
}
