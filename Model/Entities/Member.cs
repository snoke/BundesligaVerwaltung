/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 20:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace BundesligaVerwaltung.Model.Entities
{
    public class Member : myEntityRepository.Model.Entity
    {
        #region properties
        private string name;
        private Team team;
        private Role role;
        #endregion

        #region accessors

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Team Team
        {
            get { return team; }
            set { team = value; }
        }
        public Role Role
        {
            get { return role; }
            set { role = value; }
        }
        #endregion

        #region constructors
        public Member(int? id, string name, Team team, Role role) : base(id)
        {
            Name = name;
            Team = team;
            Role = role;
        }
        #endregion

        #region workers
        #endregion
    }
}
