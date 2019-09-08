namespace BundesligaVerwaltung.Model.Entities
{
    public class Team : Entity
    {
        #region properties
        private string name;
        private League league;
        #endregion

        #region accessors
        public string Name { get => name; set => name = value; }
        public League League { get => league; set => league = value; }
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

