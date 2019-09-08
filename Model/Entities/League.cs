namespace BundesligaVerwaltung.Model.Entities
{
    public class League : Entity
    {
        #region properties
        private string name;
        private int maximumTeams;
        #endregion

        #region accessors
        public string Name { get => name; set => name = value; }
        public int MaximumTeams { get => maximumTeams; set => maximumTeams = value; }
        #endregion

        #region constructors
        public League(int? id, string name, int maximumTeams) : base(id)
        {
            Name = name;
            MaximumTeams = maximumTeams;
        }
        #endregion

        #region workers
        #endregion
    }
}

