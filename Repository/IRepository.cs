/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 02.09.2019
 * Time: 21:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using BundesligaVerwaltung.Model;
using System.Collections.Generic;
using BundesligaVerwaltung.Model.Members;

namespace BundesligaVerwaltung.Repository
{
	 public abstract class IRepository
	{
		#region properties
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
			public IRepository()
			{
			}
		#endregion
		
		#region workers
		public abstract List<Team> LoadTeams();
		public abstract List<Match> LoadMatches();
		public abstract List<Member> LoadMembers();
		public abstract void SaveTeams(List<Team> members);
		public abstract void SaveMatches(List<Match> members);
		public abstract void SaveMembers(List<Member> members);
		#endregion
	}
}
