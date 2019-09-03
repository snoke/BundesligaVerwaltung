﻿/*
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
		public int id;
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
			public Entity(int id) {
				this.id = id;
			}
		
			public Entity(List<object> row) {
				this.id = Int32.Parse(row[0].ToString());
			}
		#endregion
		
		#region workers
		public virtual  List<string[]> GetKeys() {
			List<string[]> keys = new List<string[]>();
			keys.Add(new string[]{"id","INTEGER"});
			return keys;
		}
		public virtual List<object> GetValues() {
			List<object> values = new List<object>();
			values.Add(this.id);
			return values;
		}
		#endregion
	}
}
