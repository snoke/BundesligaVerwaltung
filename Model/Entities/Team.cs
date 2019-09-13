namespace BundesligaVerwaltung.Model.Entities
{
    public class Team : myEntityRepository.Model.Entity
    {
        #region properties
        private string name;
        private League league;
        #endregion

        #region accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public League League
        {
            get { return league; }
            set { league = value; }
        }
        #endregion

        #region constructors
        public Team(int? id, string name, League league) : base(id)
        {
            Name = name;
            League = league;
        }
        #endregion

        #region workers
        #endregion
    }
}

