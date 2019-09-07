﻿using System.Collections.Generic;
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
        public Team(List<object> args) : base((int)args[0])
        {
            Name = (string)args[1];
        }
        #endregion

        #region workers
        #endregion
    }
}

