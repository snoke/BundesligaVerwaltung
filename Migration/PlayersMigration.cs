﻿using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model.Entities;

using myEntityRepository.DataStorage;
using myEntityRepository;
namespace BundesligaVerwaltung.Migration
{
    internal class PlayersMigration
    {
        private EntityRepository _repository;
        private EntityRepository Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }
        private List<Team> _teams;
        private List<Team> Teams
        {
            get { return _teams; }
            set { _teams = value; }
        }


        private List<Role> _roles;
        public List<Role> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public PlayersMigration(EntityRepository repository, List<Team> teams, List<Role> roles)
        {
            Repository = repository;
            Teams = teams;
            Roles = roles;
        }
        private void membersUp(Team team, string[] members, Role role)
        {

            foreach (string member in members)
            {
                Repository.Save(new Member(null, member, team, role));
            }
        }
        public void up()
        {
            List<Team> teams = Teams;
            List<Role> roles = Roles;

            // Die Informationen entstammen offiziellen Webseite des DFB und sind frei zugänglich!
            // https://www.dfb.de/bundesliga
            //FC Bayern
            membersUp(teams.Single(x => x.Name == "FC Bayern München"), new string[] {
           "Manuel Neuer","Christian Früchtl","Ron - Thorben Hoffmann","Sven Ulreich","Benjamin Pavard","David Alaba","Joshua Kimmich","Niklas Süle","Lucas Hernández","Javi Martínez","Jérôme Boateng","Lars Lukas Mai","Kingsley Coman","Serge Gnabry","Thiago","Corentin Tolisso","Ivan Perisic","Alphonso Davies","Philippe Coutinho","Leon Goretzka","Mickaël Cuisance","Ryan Johansson","Sarpreet Singh","Paul Will","Robert Lewandowski","Thomas Müller","Fiete Arp","Kwasi Okyere Wriedt",
            }, roles.Single(x => x.Name == "Spieler"));
            //Borussia Dortmund
            membersUp(teams.Single(x => x.Name == "Borussia Dortmund"), new string[] {
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
            }, roles.Single(x => x.Name == "Spieler"));


            //RB Leipzig
            membersUp(teams.Single(x => x.Name == "RB Leipzig"), new string[] {
           "Péter Gulácsi","Yvon Mvogo","Tim Schreiber","Philipp Tschauner","Lukas Klostermann","Ibrahima Konaté","Nordi Mukiele","Willi Orban","Marcel Halstenberg","Marcelo Saracchi","Ethan Ampadu","Luan Cândido","Frederik Jäkel","Anton Rücker","Malik Talabidi","Dayot Upamecano","Marcel Sabitzer","Diego Demme","Konrad Laimer","Christopher Nkunku","Amadou Haidara","Kevin Kampl","Tyler Adams","Oliver Bias","Mads Bidstrup","Stefan Ilsanker","Tom Krauß","Max Winter","Hannes Wolf","Timo Werner","Yussuf Poulsen","Emil Forsberg","Matheus Cunha","Ademola Lookman","Jean - Kévin Augustin","Jacob Ruhner","Patrik Schick",
            }, roles.Single(x => x.Name == "Spieler"));

            //Bayer Leverkusen
            membersUp(teams.Single(x => x.Name == "Bayer 04 Leverkusen"), new string[] {
     "Lukáš Hrádecký","Ramazan Özcan","Niklas Lomb","Sven Bender","Wendell","Jonathan Tah","Aleksandar Dragovic","Panagiotis Retsos","Mitchell Weiser","Kai Havertz","Charles Aránguiz","Karim Bellarabi","Kerem Demirbay","Nadiem Amiri","Julian Baumgartlinger","Lars Bender","Moussa Diaby","Daley Sinkgraven","Adrian Stanilewicz","Kevin Volland","Leon Bailey","Lucas Alario","Paulinho","Joel Pohjanpalo",
        }, roles.Single(x => x.Name == "Spieler"));

            //Mönchengladbach
            membersUp(teams.Single(x => x.Name == "Borussia Mönchengladbach"), new string[] {
               "Yann Sommer","Max Grün","Tobias Sippel","Stefan Lainer","Nico Elvedi","Matthias Ginter","Oscar Wendt","Tony Jantschke","Ramy Bensebaini","Louis Beyer","Mamadou Doucouré","Andreas Poulsen","László Bénes","Fabian Johnson","Florian Neuhaus","Denis Zakaria","Christoph Kramer","Tobias Strobl","Jonas Hofmann","Aaron Herzog","Torben Müsel","Breel Embolo","Alassane Pléa","Marcus Thuram","Raffael","Ibrahima Traoré","Keanan Bennetts","Patrick Herrmann","Lars Stindl","Julio Villalba",
            }, roles.Single(x => x.Name == "Spieler"));
            //Hoffenheim
            membersUp(teams.Single(x => x.Name == "TSG 1899 Hoffenheim"), new string[] {
               "Oliver Baumann","Philipp Pentke","Alexander Stolz","Pavel Kadeřábek","Stefan Posch","Kevin Vogt","Ermin Bicakcic","Kostas Stafylidis","Kevin Akpoguma","Joshua Brenet","Benjamin Hübner","Justin Hoogma","Havard Nordtveit","Lucas Ribeiro","Dennis Geiger","Sebastian Rudy","Florian Grillitsch","Vincenzo Grifo","Lukas Rupp","Diadie Samassekou","Steven Zuber","Christoph Baumgartner","Leonardo Bittencourt","Bruno Nazario","Philipp Ochs","Robert Žulj","Ihlas Bebou","Robert Skov","Ishak Belfodil","Jürgen Locadia","Sargis Adamyan","Andrej Kramaric","Felipe Pires",
            }, roles.Single(x => x.Name == "Spieler"));
            //VFL Wolfsburg
            membersUp(teams.Single(x => x.Name == "VfL Wolfsburg"), new string[] {
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
            }, roles.Single(x => x.Name == "Spieler"));


            //Hertha BSC
            membersUp(teams.Single(x => x.Name == "Hertha BSC"), new string[] {
               "Rune Jarstein","Nils Körber","Thomas Kraft","Dennis Smarsch","Maximilian Mittelstädt","Karim Rekik","Niklas Stark","Lukas Klünter","Marvin Plattenhardt","Jordan Torunarigha","Florian Baak","Dedryck Boyata","Peter Pekarík","Marko Grujic","Vladimír Darida","Dodi Lukebakio","Ondrej Duda","Mathew Leckie","Alexander Esswein","Salomon Kalou","Javairô Dilrosun","Eduard Löwen","Per Ciljan Skjelbred","Julian Albrecht","Maurice Covic","Sidney Friede","Arne Maier","Vedad Ibisevic","Davie Selke","Daishawn Redan","Palko Dárdai","Dennis Jastrzembski","Muhammed Kiprit","Pascal Köpke",
            }, roles.Single(x => x.Name == "Spieler"));


            //Schalke
            membersUp(teams.Single(x => x.Name == "FC Schalke 04"), new string[] {
               "Alexander Nübel","Michael Langer","Markus Schubert","Jonjoe Kenny","Bastian Oczipka","Benjamin Stambouli","Matija Nastasic","Salif Sané","Jonas Carls","Pablo Insúa","Ozan Kabak","Juan Miranda","Daniel Caligiuri","Amine Harit","Omar Mascarell","Weston McKennie","Münir Levent Mercan","Suat Serdar","Alessandro Schöpf","Nabil Bentaleb","Nassim Boujellab","Marcel Langer","Guido Burgstaller","Ahmed Kutucu","Benito Raman","Steven Skrzybski","Fabian Reese","Rabbi Matondo","Mark Uth",
            }, roles.Single(x => x.Name == "Spieler"));


            //Eintracht
            membersUp(teams.Single(x => x.Name == "Eintracht Frankfurt"), new string[] {
              "Kevin Trapp","Felix Wiedwald","Frederik Rönnow","Danny Da Costa","Martin Hinteregger","David Abraham","Erik Durm","Almamy Touré","Evan NDicka","Timothy Chandler","Marco Russ","Simon Falette","Daichi Kamada","Filip Kostic","Dominik Kohr","Mijat Gacinovic","Makoto Hasebe","Gelson Fernandes","Sebastian Rode","Lucas Torró","Jonathan De Guzmán","Djibril Sow","Marijan Cavar","Sahverdi Cetin","Patrick Finger","Nicolai Müller","Marc Stendera","Nils Stendera","Gonçalo Paciência","Dejan Joveljic","Bas Dost",
            }, roles.Single(x => x.Name == "Spieler"));
            //Werder Bremen
            membersUp(teams.Single(x => x.Name == "SV Werder Bremen"), new string[] {
               "Jiri Pavlenka","Stefanos Kapino","Luca Plogmann","Michael Zetterer","Niklas Moisander","Marco Friedl","Theodor Gebre Selassie","Ömer Toprak","Michael Lang","Ludwig Augustinsson","Sebastian Langkamp","Miloš Veljkovic","Jannes Vollert","Niklas Wiemann","Davy Klaassen","Nuri Sahin","Maximilian Eggestein","Milot Rashica","Christian Groß","Kevin Möhwald","Philipp Bargfrede","Fin Bartels","Ilia Gruev","Ole Käuper","David Lennart Philipp","Niklas Schmidt","Simon Straudi","Yuya Osako","Claudio Pizarro","Niclas Füllkrug","Josh Sargent","Johannes Eggestein","Martin Harnik","Benjamin Goller","Luc Ihorst",
            }, roles.Single(x => x.Name == "Spieler"));

            //Mainz
            membersUp(teams.Single(x => x.Name == "1.FSV Mainz 05"), new string[] {
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
            }, roles.Single(x => x.Name == "Spieler"));

            //Augsburg
            membersUp(teams.Single(x => x.Name == "FC Augsburg"), new string[] {
               "Tomas Koubek","Fabian Giefer","Benjamin Leneis","Andreas Luthe","Mads Pedersen","Marek Suchý","Stephan Lichtsteiner","Tin Jedvaj","Philipp Max","Reece Oxford","Tim Rieder","Felix Ohis Uduokhai","Simon Asta","Raphael Framberger","Jeffrey Gouweleeuw","Iago","Kilian Jakob","Jozo Stanic","Ruben Vargas","Daniel Baier","Michael Gregoritsch","Rani Khedira","Andre Hahn","Carlos Gruezo","Marco Richter","Fredrik Jensen","Georg Teigl","Felix Götze","Jan Morávek","Stefano Russo","Noah Joel Sarenren Bazee","Felix Schwarzholz","Florian Niederlechner","Alfred Finnbogason","Julian Schieber","Seong-Hoon Cheon","Sergio Córdova","Maurice Malone",
            }, roles.Single(x => x.Name == "Spieler"));

            //Freiburg
            membersUp(teams.Single(x => x.Name == "SC Freiburg"), new string[] {
               "Alexander Schwolow","Mark Flekken","Niclas Thiede","Philipp Lienhart","Christian Günter","Robin Koch","Nico Schlotterbeck","Dominique Heintz","Manuel Gulde","Gian-Luca Itter","Lukas Kübler","Jonathan Schmid","Brandon Borrello","Nicolas Höfler","Mike Frantz","Jerôme Gondorf","Chang-Hoon Kwon","Roland Sallai","Amir Abrashi","Janik Haberer","Woo-yeong Jeong","Patrick Kammerbauer","Florian Kath","Yoric Ravet","Fabian Rüdlin","Marco Terrazzino","Luca Waldschmidt","Lucas Höler","Nils Petersen","Tim Kleindienst",
            }, roles.Single(x => x.Name == "Spieler"));
            //Köln
            membersUp(teams.Single(x => x.Name == "1.FC Köln"), new string[] {
               "Timo Horn","Thomas Kessler","Julian Krahl","Brady Scott","Rafael Czichos","Kingsley Ehizibue","Jorge Meré","Sebastiaan Bornauw","Matthias Bader","Ismail Jakobs","Noah Katterbach","Benno Erik Schmitz","Lasse Sobiech","Florian Kainz","Dominick Drexler","Kingsley Schindler","Jonas Hector","Birger Verstraete","Ellyes Skhiri","Marco Höger","Louis Schaub","Darko Churlinov","Christian Clemens","Niklas Hauptmann","Vincent Koziello","Nikolas Nartey","Marcel Risse","Anthony Modeste","Simon Terodde","Jhon Córdoba","Noah Joel Sarenren Bazee","Felix Schwarzholz","Florian Niederlechner","Alfred Finnbogason","Julian Schieber","Seong-Hoon Cheon","Sergio Córdova",
            }, roles.Single(x => x.Name == "Spieler"));
            //Fortuna Düsseldorf

            membersUp(teams.Single(x => x.Name == "Fortuna Düsseldorf"), new string[] {
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
            }, roles.Single(x => x.Name == "Spieler"));
            //Union Berlin
            membersUp(teams.Single(x => x.Name == "1.FC Union Berlin"), new string[] {
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
            }, roles.Single(x => x.Name == "Spieler"));
            //Paderborn
            membersUp(teams.Single(x => x.Name == "SC Paderborn 07"), new string[] {
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
            }, roles.Single(x => x.Name == "Spieler"));

        }
    }
}
