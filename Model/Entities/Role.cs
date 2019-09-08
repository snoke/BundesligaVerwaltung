﻿namespace BundesligaVerwaltung.Model.Entities
{
    public class Role : Entity
    {
        #region properties
        private string name;
        #endregion

        #region accessors
        public string Name { get => name; set => name = value; }
        #endregion

        #region constructors
        public Role(int? id, string name) : base(id)
        {
            Name = name;
        }
        #endregion

        #region workers
        #endregion
    }
}
