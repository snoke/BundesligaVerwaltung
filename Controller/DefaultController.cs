using System;
using System.Text;
using BundesligaVerwaltung.View;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.Repository.DataStorage;
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
    	private EntityRepository _Repository;
    	private Terminal Terminal;
    	
    	private EntityRepository Repository
	    {
	        get {
    			return this._Repository;
	        } 
    		set {
    			this._Repository = value;
	        }
	    }
    	
    	
    	public DefaultController() {
    		//this.Repository = new EntityRepository(new XmlStrategy());
    		this.Repository = new EntityRepository(new SQLiteStrategy("db.sqlite"));
    		this.Terminal = new Terminal();
    	}
    	
        public void Tabelle() {
        	List<Team> teams = this.Repository.teams.OrderByDescending(x => x.GetWins().Count()).ToList();
        	string output="Spieltag:" + teams.Min(x => x.GetMatches().Count()+1) + "\n";
        //	string output="";
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
        }
        public void MainMenu() {
        	this.Tabelle();
        }
        public void Run()
        {
        	this.MainMenu();
        }
    }
}
