/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 20:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.Generic;
namespace BundesligaVerwaltung.Model
{
    public class Member : Entity
    {
        #region properties
        private string name;
        private Team team;
        private Role role;
        #endregion

        #region accessors
        public string Name { get => name; set => name = value; }
        public  Team Team { get => team; set => team = value; }
        public Role Role { get => role; set => role = value; }
        #endregion

        #region constructors
        public Member(int? id, string name, Team team, Role role) : base(id)
        {
            Name = name;
            Team = team;
            Role = role;
        }
        public Member(List<object> args) : base((int)args[0])
        {
            Name = (string)args[1];
            Team = (Team)args[2];
            Role = (Role)args[3];
        }
        #endregion

        #region workers
        #endregion
    }
}
