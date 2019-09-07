using System.Collections.Generic;
namespace BundesligaVerwaltung.Model
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
        public Role(List<object> args) : base((int)args[0])
        {
            Name = (string)args[1];
        }
        #endregion

        #region workers
        #endregion
    }
}

