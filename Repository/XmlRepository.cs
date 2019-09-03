/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 18:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using BundesligaVerwaltung.Model;
using BundesligaVerwaltung.Model.Members;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Data.SQLite;

namespace BundesligaVerwaltung.Repository
{
	public class XmlRepository:IRepository
	{
		#region properties
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
			public XmlRepository()
			{
			}
		#endregion
		
		#region workers
		public override List<Member> LoadMembers() {
			XDocument document = XDocument.Load("Members.xml");
			List<Member> members = new List<Member>();
			foreach(XElement element in document.Root.Elements("Member")) {
							members.Add(new Member(XElementToList(element)));
			}
			return members;
		}
		private List<object> XElementToList(XElement element) {
			List<object> list = new List<object>();
			foreach(XElement parameter in element.Descendants()) {
				list.Add(parameter.Value);
			}
			return list;
		}
		public override List<Match> LoadMatches() {
			XDocument document = XDocument.Load("Matches.xml");
			List<Match> matches = new List<Match>();
			
			foreach(XElement element in document.Root.Elements("Match")) {
				matches.Add(new Match(XElementToList(element)));
			}
			
			return matches;
		}
		public override List<Team> LoadTeams() {
			XDocument document = XDocument.Load("Teams.xml");
			List<Team> teams = new List<Team>();
			List<Match> matches = this.LoadMatches();
			foreach(XElement element in document.Root.Elements("Team")) {
				Team team = new Team(XElementToList(element));
				team.Matches = matches.Where(o => o.teamId == team.id  || o.opponentId == team.id).ToList();
				teams.Add(team);
			}
			return teams;
		}
		
		public override void SaveMatches(List<Match> matches) {
			XElement root = new XElement("root");
			for(int i =0;i<matches.Count;i++) {
				XElement e = new XElement("Match");
				e.Add(new XElement("id",matches[i].id.ToString()));
				e.Add(new XElement("teamid",matches[i].teamId.ToString()));
				e.Add(new XElement("opponentid",matches[i].opponentId.ToString()));
				e.Add(new XElement("score",matches[i].score.ToString()));
				e.Add(new XElement("opponentscore",matches[i].opponentScore.ToString()));
				root.Add(e);
			}
			
			XDocument xdoc = new XDocument();
			xdoc.Add(root);
			xdoc.Save("Matches.xml");
		}
		public override void SaveTeams(List<Team> teams) {
			XElement root = new XElement("root");
			for(int i =0;i<teams.Count;i++) {
				XElement e = new XElement("Team");
				e.Add(new XElement("id",teams[i].id.ToString()));
				e.Add(new XElement("name",teams[i].name));
				root.Add(e);
			}
			
			XDocument xdoc = new XDocument();
			xdoc.Add(root);
			xdoc.Save("Teams.xml");
		}
		public override void SaveMembers(List<Member> members) {
			XElement root = new XElement("root");
			for(int i =0;i<members.Count;i++) {
				XElement e = new XElement("Member");
				e.Add(new XElement("id",members[i].id.ToString()));
				e.Add(new XElement("name",members[i].name));
				e.Add(new XElement("teamid",members[i].teamid.ToString()));
				e.Add(new XElement("role",members[i].GetType().Name));
				root.Add(e);
			}
			
			XDocument xdoc = new XDocument();
			xdoc.Add(root);
			xdoc.Save("Members.xml");
		}
		#endregion
	}
}
