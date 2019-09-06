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
		public Team(int? id, string name) : base(id)
		{
			Name = name;
        }
        public Team(params string[] args) : base(Int32.Parse(args[0]))
        {
            this.Name = args[1];
        }
        #endregion

        #region workers
        #endregion
    }
}

