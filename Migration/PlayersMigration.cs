﻿using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.Repository.DataStorage;

namespace BundesligaVerwaltung.Migration
{
    internal class PlayersMigration
    {
        private DataStorage _dataStorage;
        public DataStorage DataStorage { get => _dataStorage; set => _dataStorage = value; }

        private List<Team> _teams;
        public List<Team> Teams { get => _teams; set => _teams = value; }

        private List<Role> _roles;
        public List<Role> Roles { get => _roles; set => _roles = value; }

        public PlayersMigration(DataStorage dataStorage, List<Team> teams, List<Role> roles)
        {
            DataStorage = dataStorage;
            Teams = teams;
            Roles = roles;
        }
        private void membersUp(Team team, string[] members, Role role)
        {

            foreach (string member in members)
            {
                DataStorage.SaveEntity(new Member(null, member, team, role));
            }
        }
        public void up()
        {
            List<Team> teams = Teams;
            List<Role> roles = Roles;

            // Die Informationen entstammen offiziellen Webseite des DFB und sind frei zugänglich!
            // https://www.dfb.de/bundesliga
            //FC Bayern
            membersUp(teams.Single(x => x.id == 1), new string[] {
           "Manuel Neuer",
            }, roles.Single(x => x.id == 1));
            //Borussia Dortmund
            membersUp(teams.Single(x => x.id == 2), new string[] {
                "Roman Bürki",
"Marwin Hitz",
"Eric Oelschlägel",
"Luca Unbehaun",
"Achraf Hakimi",
"Manuel Akanji",
"Lukasz Piszczek",
"Mats Hummels",
"Nico Schulz",
"Raphaël Guerreiro",
"Leonardo Balerdi",
"Mateu Morey",
"Felix Passlack",
"Marcel Schmelzer",
"Jeremy Toljan",
"Dan-Axel Zagadou",
"Jadon Sancho",
"Marco Reus",
"Julian Weigl",
"Julian Brandt",
"Axel Witsel",
"Thorgan Hazard",
"Mario Götze",
"Mahmoud Dahoud",
"Thomas Delaney",
"Dženis Burnic",
"Patrick Osterhage",
"Tobias Raschl",
"Andre Schürrle",
"Paco Alcácer",
"Jacob Bruun Larsen",
"Marius Wolf",
            }, roles.Single(x => x.id == 1));


            //RB Leipzig
            membersUp(teams.Single(x => x.id == 3), new string[] {
           "Péter Gulácsi",
            }, roles.Single(x => x.id == 1));

            //Bayer Leverkusen
            membersUp(teams.Single(x => x.id == 4), new string[] {
     "Lukáš Hrádecký",
        }, roles.Single(x => x.id == 1));

            //Mönchengladbach
            membersUp(teams.Single(x => x.id == 5), new string[] {
               "Yann Sommer",
            }, roles.Single(x => x.id == 1));
            //Hoffenheim
            membersUp(teams.Single(x => x.id == 6), new string[] {
               "Oliver Baumann",
            }, roles.Single(x => x.id == 1));
            //VFL Wolfsburg
            membersUp(teams.Single(x => x.id == 7), new string[] {
 "Koen Casteels",
"Lino Kasten",
"Niklas Klinger",
"Phillip Menzel",
"Pavao Pervan",
"William",
"Robin Knoche",
"Jérôme Roussillon",
"John Brooks",
"Yannick Gerhardt",
"Kevin Mbabu",
"Jeffrey Bruma",
"Paulo Otávio",
"Marcel Tisserand",
"Josip Brekalo",
"Maximilian Arnold",
"Victor João",
"Josuha Guilavogui",
"Felix Klaus",
"Xaver Schlager",
"Admir Mehmedi",
"Renato Steffen",
"Ismail Azzaoui",
"Ignacio Camacho",
"Yunus Malli",
"Paul-Georges Ntep",
"Elvis Rexhbecaj",
"Wout Weghorst",
"Lukas Nmecha",
"Daniel Ginczek",
"Marvin Stefaniak",
            }, roles.Single(x => x.id == 1));


            //Hertha BSC
            membersUp(teams.Single(x => x.id == 8), new string[] {
               "Rune Jarstein",
            }, roles.Single(x => x.id == 1));


            //Schalke
            membersUp(teams.Single(x => x.id == 9), new string[] {
               "Alexander Nübel",
            }, roles.Single(x => x.id == 1));


            //Eintracht
            membersUp(teams.Single(x => x.id == 10), new string[] {
              "Kevin Trapp","Felix Wiedwald","Frederik Rönnow","Danny Da Costa","Martin Hinteregger","David Abraham","Erik Durm","Almamy Touré","Evan NDicka","Timothy Chandler","Marco Russ","Simon Falette","Daichi Kamada","Filip Kostic","Dominik Kohr","Mijat Gacinovic","Makoto Hasebe","Gelson Fernandes","Sebastian Rode","Lucas Torró","Jonathan De Guzmán","Djibril Sow","Marijan Cavar","Sahverdi Cetin","Patrick Finger","Nicolai Müller","Marc Stendera","Nils Stendera","Gonçalo Paciência","Dejan Joveljic","Bas Dost",
            }, roles.Single(x => x.id == 1));
            //Werder Bremen
            membersUp(teams.Single(x => x.id == 11), new string[] {
               "Jiri Pavlenka",
            }, roles.Single(x => x.id == 1));

            //Mainz
            membersUp(teams.Single(x => x.id == 12), new string[] {
           "Florian Müller",
"Finn Dahmen",
"Omer Hanin",
"Robin Zentner",
"Aaaron Martin",
"Moussa Niakhaté",
"Daniel Brosinski",
"Alexander Hack",
"Jeremiah St. Juste",
"Ronaël Pierre-Gabriel",
"Stefan Bell",
"Ahmet Gürleyen",
"Niklas Kolle",
"Jonathan Meier",
"Philipp Mwene",
"Jean-Paul Boëtius",
"Ridle Baku",
"Edimilson Fernandes",
"Danny Latza",
"Pierre Kunde",
"Alexandru Maxim",
"Levin Öztunali",
"Erkan Eyibil",
"Leandro Martins",
"Karim Onisiwo",
"Robin Quaison",
"Jonathan Michael Burkardt",
"Taiwo Awoniyi",
"Ádám Szalai",
"Cyrill Akono",
"Dong Won Ji",
"Jean-Philippe Mateta",
"Aaron Seydel",
            }, roles.Single(x => x.id == 1));

            //Augsburg
            membersUp(teams.Single(x => x.id == 13), new string[] {
               "Tomas Koubek",
            }, roles.Single(x => x.id == 1));

            //Freiburg
            membersUp(teams.Single(x => x.id == 14), new string[] {
               "Alexander Schwolow",
            }, roles.Single(x => x.id == 1));
            //Köln
            membersUp(teams.Single(x => x.id == 15), new string[] {
               "Timo Horn",
            }, roles.Single(x => x.id == 1));
            //Fortuna Düsseldorf
            membersUp(teams.Single(x => x.id == 16), new string[] {
                "Zack Steffen",
                "Florian Kastenmeier",
                "Michael Rensing",
                "Tim Wiesner",
                "Raphael Wolf",
                "Kaan Ayhan",
                "Niko Gießelmann",
                "André Hoffmann",
                "Matthias Zimmermann",
                "Adam Bodzek",
                "Markus Suttner",
                "Jean Zimmer",
                "Robin Bormuth",
                "Diego Contento",
                "Kasim Nuhu",
                "Michel Stöcker",
                "Alfredo Morales",
                "Erik Thommy",
                "Lewis Baker",
                "Oliver Fink",
                "Thomas Pledl",
                "Marcel Sobottka",
                "Aymen Barkok",
                "Shinta Karl Appelkamp",
                "Johannes Bühler",
                "Davor Lovren",
                "Kevin Stöger",
                "Rouwen Hennings",
                "Kenan Karaman",
                "Dawid Kownacki",
                "Bernard Tekpetey",
                "Nana Opoku Ampomah",
                "Kelvin Ofori",
                "Emmanuel Iyoha",
            }, roles.Single(x => x.id == 1));
            //Union Berlin
            membersUp(teams.Single(x => x.id == 17), new string[] {
                "Rafal Gikiewicz",
"Jakob Busk",
"Moritz Nicolas",
"Christopher Lenz",
"Christopher Trimmel",
"Keven Schlotterbeck",
"Marvin Friedrich",
"Neven Subotic",
"Florian Hübner",
"Lennard Maloney",
"Nicolai Rapp",
"Ken Reichel",
"Robert Andrich",
"Grischa Prömel",
"Christian Gentner",
"Joshua Mees",
"Felix Kroos",
"Michael Parensen",
"Manuel Schmiedebach",
"Lars Dietz",
"Florian Flecker",
"Akaki Gogia",
"Julius Kade",
"Cihan Kahraman",
"Maurice Opfermann",
"Julian Ryerson",
"Sebastian Andersson",
"Marius Bülter",
"Anthony Ujah",
"Sheraldo Becker",
"Marcus Ingvartsen",
"Sebastian Polter",
"Suleiman Abdullahi",
"Laurenz Dehl",
            }, roles.Single(x => x.id == 1));
            //Paderborn
            membersUp(teams.Single(x => x.id == 18), new string[] {
                "Jannik Huth",
"Leon Brüggemeier",
"Michael Ratajczak",
"Leopold Zingerle",
"Uwe Hünemeier",
"Jamilu Collins",
"Christian Strohdiek",
"Mohamed Dräger",
"Laurent Jans",
"Luca Kilian",
"Justin Reineke",
"Jan-Luca Rumpf",
"Sebastian Schonlau",
"Christopher Antwi-Adjej",
"Cauly",
"Sebastian Vasiliadis",
"Klaus Gjasula",
"Gerrit Holtmann",
"Kai Pröger",
"Abdelhamid Sabiri",
"Johannes Dörfler",
"Marcel Hilßner",
"Rifet Kapic",
"Marlon Ritter",
"Streli Mamba",
"Sven Michel",
"Ben Zolinski",
"Babacar Guèye",
"Khiry Shelton",
"Felix Drinkuth",
"Sergio Gucciardo",
            }, roles.Single(x => x.id == 1));

        }
    }
}