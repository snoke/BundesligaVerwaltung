using System.Collections.Generic;
using System.Linq;
using System;
namespace BundesligaVerwaltung.Model
{
	public class Team : Entity
	{
		#region properties
		private string name;
		#endregion

		#region accessors
		public string Name { get => name; set => name = value; }
		#endregion

		#region constructors
		public Team(List<object> row) : base((int)Int32.Parse(row[0].ToString()))
		{
			Name = (string)row[1];
		}
		public Team(int? id, string name) : base(id)
		{
			Name = name;
		}
		#endregion

		#region workers
		#endregion
	}
}

