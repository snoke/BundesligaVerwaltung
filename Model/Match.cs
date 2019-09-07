/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 01.09.2019
 * Time: 00:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.Generic;

namespace BundesligaVerwaltung.Model
{
    public class Match : Entity
    {
        #region properties
        private Team team;
        private Team opponent;
        private int score;
        private int opponentScore;
        #endregion

        #region accessors
        public Team Team
        {
            get { return team; }
            set { team = value; }
        }
        public Team Opponent
        {
            get { return opponent; }
            set { opponent = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public int OpponentScore
        {
            get { return opponentScore; }
            set { opponentScore = value; }
        }
        #endregion

        #region constructors
        public Match(int? id, Team team, Team opponent, int score, int opponentScore) : base(id)
        {
            Team = team;
            Opponent = opponent;
            Score = score;
            OpponentScore = opponentScore;
        }
        public Match(List<object> args) : base((int)args[0])
        {
            OpponentScore = (int)args[4];
            Score = (int)args[3];
            Opponent = (Team)args[2];
            Team = (Team)args[1];
        }
        #endregion

        #region workers
        #endregion
    }
}
