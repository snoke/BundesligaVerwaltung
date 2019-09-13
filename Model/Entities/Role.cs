namespace BundesligaVerwaltung.Model.Entities
{
    public class Role : myEntityRepository.Model.Entity
    {
        #region properties
        private string name;
        #endregion

        #region accessors

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
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

