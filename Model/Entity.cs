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
	public abstract class Entity
	{
		#region properties
		private int? _id;
		#endregion

		#region accessors
		public int? id
		{
			get { return _id; }
			set { _id = value; }
		}
		#endregion

		#region constructors
		public Entity(int? id)
        {
            this.id = id;
        }
		#endregion

		#region workers

		#endregion
	}
}
