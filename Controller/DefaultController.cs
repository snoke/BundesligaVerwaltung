using System;
using System.Text;
using BundesligaVerwaltung.View;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.Model;
using BundesligaVerwaltung.Model.Members;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Data.SQLite;
namespace BundesligaVerwaltung.Controller
{
    class DefaultController
    {
    	private IRepository Repository;
    	private Terminal Terminal;
    	
    	private List<Team> _teams;
    	private List<Member> _members;
    	private List<Match> _matches;
    	
    	private List<Team> Teams
	    {
	        get {
    			//lazy loading
    			if (this._teams==null) {
    				this._teams = this.Repository.LoadTeams();
    			} else {}
    			return this._teams;
	        } 
    		set {
    			this._teams = value;
	        }
	    }
    	private List<Match> Matches
	    {
	        get {
    			//lazy loading
    			if (this._matches==null) {
    				this._matches = this.Repository.LoadMatches();
    			} else {}
    			return this._matches;
	        } 
    		set {
    			this._matches = value;
	        }
	    }
    	private List<Member> Members
	    {
	        get {
    			//lazy loading
    			if (this._members==null) {
    				this._members = this.Repository.LoadMembers();
    			} else {}
    			return this._members;
	        } 
    		set {
    			this._members = value;
	        }
	    }
    	    
    	
    	public DefaultController() {
    		//this.Repository = new XmlRepository();
    		this.Repository = new SqliteRepository();
    		this.Terminal = new Terminal();
    	}
    	
        private void __MainMenu()
        {
	        string[] menuElements = new string[this.Teams.Count+2];
	        for(int i = 0;i<this.Teams.Count;i++) {
	        	menuElements[i] = this.Teams[i].name;
	        }
	        menuElements[this.Teams.Count] = "Verein hinzufügen";
	        menuElements[this.Teams.Count+1] = "Tabelle anzeigen";
	        int choice = new SelectMenu.SelectMenu(menuElements).setTitle("Bitte wählen").select();
	        if (choice == this.Teams.Count) {
	        	//Add
	        	if (this.Teams.Count()<Team.MAXIMUM_PARTICIPANTS) {
		        	this.Teams.Add(
		        		new Team(
		        			this.Teams.Count,
		        			this.Terminal.AskForString("Bitte geben Sie einen Vereinsnamen ein"))
		        	);
	    			this.Repository.SaveTeams(this.Teams);
	        	} else {
	        		this.Terminal.Message("Die Liga ist bereits voll!");
	        	}
	        	this.MainMenu();
	        } else if (choice == this.Teams.Count+1) {
	        	//Tabelle anzeigen
	        } else {
	        	this.TeamMenu(this.Teams[choice]);
	        }
        }
        
        private void TeamMenu(Team team) {
        	List<Member> members = new List<Member>();
        	foreach(Member member in this.Members) {
        		if (member.teamid==team.id) {
        			members.Add(member);
        		}
        	}
        	string[] menuElements = new string[members.Count+3];
        	
	        for(int i = 0;i<members.Count;i++) {
	        	menuElements[i] = members[i].name;
	        }
        	menuElements[members.Count] = "Neues Mitglied hinzufügen";
        	menuElements[members.Count+1] = "Spielergebnis hinzufügen";
        	menuElements[members.Count+2] = "Zurück";
        	int choice = new SelectMenu.SelectMenu(menuElements).setTitle(team.name + "\n" + "".PadLeft(20,(char)'-') + "\nBitte wählen").select();
	        
	        if (choice == members.Count) {
	        	switch (new SelectMenu.SelectMenu("Spieler","Trainer","Physio","Zurück").setTitle("Bitte eine Rolle wählen").select()) {
	        		case 0:
	        			this.Members.Add(new Player(
	        				this.Members.Count,
	        				this.Terminal.AskForString("Bitte geben Sie einen Namen ein"),
	        				team.id
	        			));
	        			this.Repository.SaveMembers(this.Members);
	        			this.MainMenu();
	        			break;
	        		case 1:
	        			this.Members.Add(new Physio(
	        				this.Members.Count,
	        				this.Terminal.AskForString("Bitte geben Sie einen Namen ein"),
	        				team.id
	        			));
	        			this.Repository.SaveMembers(this.Members);
	        			this.MainMenu();
	        			break;
	        		case 2:
	        			this.Members.Add(new Trainer(
	        				this.Members.Count,
	        				this.Terminal.AskForString("Bitte geben Sie einen Namen ein"),
	        				team.id
	        			));
	        			this.Repository.SaveMembers(this.Members);
	        			this.MainMenu();
	        			break;
	        		case 3:
	        			this.TeamMenu(team);
	        			break;
	        	}
        	} else if (choice == members.Count+1) {
        		if (team.GetMatches().Count()<this.Teams.Min(x => x.GetMatches().Count()+1)) {
        			List<Team> opponentTeams = this.Teams.Where(x=> x.id != team.id).ToList();
    		    	string[] opponentElements = new string[opponentTeams.Count+1];
			        for(int i = 0;i<opponentTeams.Count;i++) {
			        		opponentElements[i] = opponentTeams[i].name;
			        }
			        
			        opponentElements[opponentTeams.Count]= "Zurück";
			        int selection = new SelectMenu.SelectMenu(opponentElements).setTitle(team.name + "\n" + "".PadLeft(20,(char)'-') + "\nBitte Gastmannschaft wählen").select();
			        if (selection==opponentTeams.Count) {
			        	this.TeamMenu(team);
			        } else {
			    		this.Matches.Add(new Match(
			        		this.Matches.Count(),
			        		team.id,
				        	opponentTeams[selection].id,
				        	this.Terminal.AskForInteger("Bitte (Heim)Tore eingeben"),
				        	this.Terminal.AskForInteger("Bitte Gegentore eingeben")
						));
			    		this.Repository.SaveMatches(this.Matches);
			    		this.Teams = this.Repository.LoadTeams();
			    		this.MainMenu();
			        }
        		} else {
        			this.Terminal.Message("Es darf für jeden Spieltag nur 1 Spiel eingetragen werden!");
        			this.TeamMenu(team);
        		}
		        
	        } else if (choice == members.Count+2) {
		     	this.MainMenu();
	        } else {
	        	this.MemberMenu(members[choice]);
	        }
        }
        
        public void MemberMenu(Member member) {
        	switch (new SelectMenu.SelectMenu("Verein wechseln","Zurück").setTitle("Bitte wählen").select()) {
        		case 1:
        			this.TeamMenu(this.Teams[member.teamid]);
        			break;
        		case 0:
	    			string[] menuElements = new string[this.Teams.Count+1];
			        for(int i = 0;i<this.Teams.Count;i++) {
			        	menuElements[i] = this.Teams[i].name;
			        }
			        menuElements[this.Teams.Count] = "Zurück";
			        int teamChoice = new SelectMenu.SelectMenu(menuElements).setTitle("Bitte wählen").select();
			        if (teamChoice==this.Teams.Count) {
			        	this.MemberMenu(member);
			        } else {
			        	this.Members[member.id].teamid = teamChoice;
		        		this.Repository.SaveMembers(this.Members);
	        			this.MainMenu();
			        }
        			break;
        	}
        }
        public void _MainMenu() {
        	string[] options = {"MatchController - Verwaltung von Spielplan","MemberController - Verwaltung von Personal","TeamController - Verwaltung von Mannschaften"};
        	switch(this.Terminal.Menu(options)) {
        		case 0:
        			break;
        		case 1:
        			break;
        		case 2:
        			break;
        	}
        }
        public void Tabelle() {
        	List<Team> teams = this.Teams.OrderByDescending(x => x.GetWins().Count()).ToList();
        //	string output="Spieltag:" + teams.Min(x => x.GetMatches().Count()+1) + "\n";
        	string output="";
        	string[] cells = {"Mannschaft".PadLeft(24),"Spiele","Siege","Unentschieden","Niederlagen","Tore"};
    		for(int i = 0;i<cells.Length;i++) {
    			string message = cells[i];
    			output = output + (message + " | ");
    		}
        	output = output + ("\n");
    		for(int i = 0;i<cells.Length;i++) {
        		string message = "".PadRight(cells[i].Length,(char)'-');
    			output = output + (message + "---");
    		}
        	List<string> elements= new List<string>();
        	foreach(Team team in teams) {
        		string line = "";
        		string name = team.GetName();
        		string[] values= {
        			name,
        			team.GetMatches().Count().ToString(),
        			team.GetWins().Count().ToString(),
        			team.GetDraws().Count().ToString(),
        			team.GetLosses().Count().ToString(),
        			team.GetGoals().ToString(),
        		};
        		for(int i = 0;i<values.Length;i++) {
        			int length = cells[i].Length;
        			if (values[i].Length>length) {
        				values[i] = values[i].Substring(0,length);
        			}
        			line = line + (values[i].PadLeft(length)+ " | ");
        			//output = output + (values[i].PadLeft(length)+ " | ");
        		}
        			elements.Add(line);
        			//output = output + ("\n");
        	}
			elements.Add("Tabelle aktualisieren");
			elements.Add("Team hinzufügen");
			elements.Add("Programm beenden");
			int choice = new SelectMenu.SelectMenu((string[]) elements.ToArray()).setTitle(output).select();
			if (choice == elements.Count()-3) {
				this.Teams = this.Repository.LoadTeams();
				this.Tabelle();
			} else if (choice == elements.Count()-1) {
			} else if (choice == elements.Count()-2) {
				//do nothing, but close program
			} else {
				this.TeamMenu(teams[choice]);
			}
        }
        public void MainMenu() {
        	this.Tabelle();
        }
        private void Migrate() {
			SqliteRepository repo = new SqliteRepository();
			List<Match> list = repo.LoadMatches();
			
        	
        }
        public void Run()
        {
        	//this.Migrate();
        	//this._MainMenu();
        	//this.MainMenu();
        	this.MainMenu();
        }
    }
}
