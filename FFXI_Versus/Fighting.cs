using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Spectre.Console;
using Spectre.Console.Rendering;
using SixLabors.ImageSharp;
using NAudio.Wave;
using System.Net;
using System.Reflection;
using System.Numerics;
using System.ComponentModel;
using FFXI_Versus.Mechanics;
using System.Security.Cryptography;

namespace FFXI_Versus
{
    class Fighting
    {
        public CombatManager combatManager = new CombatManager();

        private static Thread? MusicThread;

        private static bool ContinueMusic = true;

        public Fighting() { }

        public void Fight()
        {
            /*MusicThread = new Thread(() =>
            {
                while (ContinueMusic)
                {
                    TitleScreen();
                    CharacterSelect();
                }
            });

            MusicThread.Start();


            MusicThread.Join();*/

            TitleScreen();

            CharacterSelect();

        }


        private static void TitleScreen()
        {
            var menuTheme = "C:\\Users\\spect\\source\\repos\\FinalFantasyXI\\FFXI_Versus\\Resources\\Musics\\FFXI - Title Theme 2022 We Are Vanadiel.mp3";

            //Generic_functions.PlayAudio(menuTheme);

            List<string> title_images = new List<string> //Create list of title patterns          
            {
                //Red Mage
                "\n  \n" + "\n" + "______________¶¶¶¶¶¶¶¶__¶¶¶¶¶¶¶¶¶\r\n__________¶¶¶¶########¶¶11111111¶¶\r\n_________¶¶###########111111¶¶¶¶¶\r\n________¶¶##########111111111¶¶¶¶¶\r\n_______¶¶###########1111##¶¶¶¶####¶¶\r\n______¶¶###########11###########¶¶\r\n_____¶¶#######################¶¶\r\n__¶¶¶¶################1111¶¶¶¶\r\n¶¶¶##########¶¶¶¶¶¶1111111¶¶\r\n__¶¶¶¶¶¶¶¶¶¶¶¶sssss111111111¶¶\r\n______¶¶¶¶ss¶¶sssss11111111111¶¶\r\n____¶¶##¶¶ssssssss111####1111111¶¶\r\n__¶¶####¶¶########11######11##11¶¶\r\n_¶###¶¶###¶¶######1#######11###11¶¶\r\n__¶¶¶¶¶¶¶######¶¶¶##############¶¶\r\n_¶¶sssss¶¶####¶¶ss¶¶############¶¶\r\n_¶¶sssss¶¶####¶¶ssss¶¶###########¶¶\r\n_¶¶sssss¶¶####¶¶ssss¶¶###¶¶#######¶¶\r\n__¶¶##¶¶¶¶¶###¶¶¶¶¶¶¶¶####¶¶######¶¶\r\n__¶¶##¶¶¶¶¶¶_____________¶¶¶¶#####¶¶\r\n__¶¶###¶¶¶¶¶¶¶###########¶¶¶¶#####¶¶\r\n__¶¶####¶¶¶¶¶¶############¶¶¶¶####¶¶\r\n__¶¶########¶¶###########¶¶¶¶¶¶####¶¶\r\n____¶¶¶¶¶¶¶¶############¶¶¶__¶¶¶##¶¶\r\n___________¶¶¶¶¶¶¶¶¶¶¶¶¶¶______¶¶¶¶",
                //Chocobo
                "\n  \n" + "\n" + "__________________________¶¶\r\n______________¶¶________¶¶nn¶¶__¶¶\r\n____________¶¶nn¶¶________¶¶nn¶¶nn¶¶\r\n__________¶¶nn¶¶¶¶¶¶¶¶¶¶¶¶nnnn¶¶nn¶¶________________¶¶\r\n_________¶¶nn¶¶nnnnnnnnnnnnn¶¶nnnn¶¶______________¶¶nn¶¶\r\n________¶¶nnnnnnxxxxxxxxxxnnnnnnnn¶¶____________¶¶xxnn¶¶\r\n________¶¶xxnn¶¶¶¶¶¶xxxxxxxxnnnnn¶¶___________¶¶¶¶xxnn¶¶¶¶\r\n______¶¶¢¢nn¶¶¯¯¯¯¯¶¶¶xxxxxxnnaa¶¶__________¶¶xx¶¶xx¶¶xxnn¶¶\r\n______¶¶¢¢¢n¶¶####¯¯¶¶xxnnnnnnaa¶¶__________¶¶xx¶¶xn¶¶xnnn¶¶¶¶\r\n______¶¶¢¢¢¢¶¶_###__¶¶xnnnnnaa¶¶____________¶¶nnnnnn¶¶xxnn¶¶nn¶¶\r\n____¶¶¢¢x¢¢¢¢¢¶¶¶¶¶¶nnnnnnaaa¶¶_____________¶¶nnxxxxnnnn¶¶nnnn¶¶\r\n__¶¶¢¢x¢¢¢¢¢¢¢¢¢¢¢¢§§nnnaaaa¶¶_____________¶¶nnxxxxxxxnn¶¶nnnn¶¶\r\n¶¶¢¢xx¢¢¢¢¢¢¶¶¶¶§§§§nnnnnnaa¶¶____________¶¶nnxxxxxxnnnnnnaa¶¶\r\n¶¶¢¢x¢¢¢¶¶¶¶§§§§¶¶nnnnxxxxnnnn¶¶¶¶________¶¶nnxxxxxxnnnnaa¶¶¶¶\r\n¶¶¢¢¢¢¶¶____¶¶¶¶¶¶nnxxxxxxxxnnnnnn¶¶¶¶¶¶¶¶nnnnxx¶¶¶¶nnaaaaaaa¶¶\r\n__¶¶¶¶¶______¶¶nnnnxxxxxxxxnn¶¶nnnnnnnnnnnnn¶¶¶¶xxnn¶¶¶¶aaaa¶¶\r\n____________¶¶nn¶¶nnxxxxxxxx¶¶nnnnxxxxxxxxnnxxxxnn¶¶nnnna¶¶¶\r\n______________¶¶nnxxxxxxxx¶¶nnnnxxxxxxxxxxxxnn¶¶¶¶nnnnaa¶¶\r\n____________¶¶nnnnxxxxxxxx¶¶nnnnxxxxxxxxxxxxxxxxxxnnnn¶¶\r\n____________¶¶nnnnxxxxxxxxxx¶¶nnnnxxxxxxxxxxxxxxnnnn¶¶a¶¶\r\n___________¶¶nnnnnnnnnxxxxxx¶¶nnnnnnnnnnxxxxxxnnnnn¶¶a¶¶\r\n________¶¶¶¶¶a¶¶aannnnnnnnnnnn¶¶aaaannnnnnnnnnnnaaaaaa¶¶\r\n______¶¶xxnn¶¶¶¶aaaa¶¶aaaaaaaaaa¶¶aaaaaaaaaaaaaaaaaa¶¶¢¢¶¶\r\n________¶¶xxn¶¶¶¶¶aaaa¶¶¶¶aaaaaaaa¶¶¶¶aaaaaaaaaa¶¶¶¶ƒƒƒƒ¢¢¶¶\r\n__________¶¶¢¢ƒƒ¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶____¶¶¶¶¶¶¶¶¶¶__¶¶¢¢¢¢¶¶\r\n____________¶¶¢¢ƒƒ¶¶¶¶¶¶ƒƒ¢¢ƒƒ¢¢¶¶________________¶¶ƒƒ¶¶¶¶¶¶¶¶\r\n_____________¶¶ƒ¢¢¢¢ƒƒ¢¢ƒƒ¶¶¶¶¶¶________________¶¶ƒƒ¢¢¶¶¢¢nnxx¶¶\r\n______________¶¶ƒƒ¢¢ƒƒ¶¶¶¶____________________¶¶¶¶¢¢ƒƒ¢¢ƒƒ¶¶¶¶\r\n______________¶¶¢¢ƒƒ¶¶__________________¶¶¶¶¶¶¢¢ƒƒ¢¢¢¢¶¶¶¶\r\n______________¶¶nn¢¢¶¶________________¶¶xx¶¶xxnn¢¢¶¶¶¶\r\n______________¶¶xx¶¶________________¶¶xx¶¶xxnn¶¶¶¶\r\n________________¶¶____________________¶¶¶¶¶¶¶¶",
                //Fighter
                "\n  \n" + "\n" + "_____59955995599999999555599\r\n____9955999999999999999999\r\n____9999999999999999999999999995\r\n_____5999999999999999999999\r\n____99999999999999999999999999\r\n____9955995599¶¶¶¶99999999999999\r\n_____9oo99¶¶¶¶¶xxx9999999999\r\n__________xx¶¶xxxx9999xxx99999\r\n__________xx¶¶xxxxxx99xxxx999\r\n_________xxxxxxxxxxxxx999966\r\n________¶¶xxxxxxxxxx¶¶¶¶999nn99\r\n____99999¶¶¶xxxxxx¶¶¶¶9999999999\r\n_____99999¶¶¶¶¶¶¶¶¶9¶¶9999999999\r\n_____¶¶¶9999¶¶¶¶999999¶¶xxxxxx\r\n____xxxx¶¶9999999999xxxx¶¶99xxx\r\n____9999xx99¶¶¶¶99xxxxxxx99999xx\r\n___xxxxx99¶¶¶999¶¶xxxx9999999999\r\n____xxx___99999999999¶¶99999999\r\n_________9999999999999999xxxx9\r\n__oo999xxx9999_____999xxxx9999o\r\n__99999999xx99_______9999999999\r\n___999999999__________99999999\r\n____999999____________999999\r\n______999___________999999",
                //Black Mage
                "\n  \n" + "\n" + "___________________________¶¶¶¶¶¶\r\n_______________________¶¶¶¶¶ccee¶¶\r\n_____________________¶¶¶cccceeaa¶¶\r\n_________________¶¶¶¶¶cccceeaaa¶¶\r\n_____________¶¶¶¶¶cccccceeaaaa¶¶\r\n__¶¶¶¶¶¶¶¶¶¶¶¶cccccceeaaaaaaa¶¶\r\n¶¶¶ccccccccccccceeaaaaaaaaaa¶¶\r\n__¶¶¶aaaaaaaeeceeeeeaaaaaag¶¶\r\n____¶¶¶¶¶¶aaaaaaaaeeeeeeag¶¶\r\n________¶¶¶¶¶¶¶¶aaaaaaeeegg¶¶\r\n_________#######¶¶¶¶¶aaaaggg¶¶\r\n________###|¯|########¶¶aaaaggg¶¶\r\n______¶¶###|_|###|¯|#####¶¶¶aagggg¶¶\r\n____¶¶££#########|_|########¶¶¶¶ggg¶¶\r\n__¶¶££¶¶#################£££££¶¶¶¶¶\r\n__¶¶££££££#########££££££££¶¶£¶¶\r\n__¶¶¶¶££££££££££££¶¶¶¶¶¶£££££¶¶\r\n_¶§xxx¶¶££££££¶¶¶¶££££££££££££¶¶\r\n_¶§xxx¶¶££££¶¶¶¶¶¶££££££££££££¶¶\r\n__¶¶¶¶££££££§xxx¶¶££££££££££££¶¶\r\n__¶¶££¶¶££££§xxx¶¶££££££££££££¶¶\r\n__¶¶££¶¶££££¶¶¶¶¶¶££££££££££¶¶\r\n__¶¶£££¶¶£££££££££¶¶££££££££¶¶\r\n__¶¶££££¶¶¶£££££££¶¶££££££££¶¶\r\n_¶¶£££££££¶¶¶¶¶¶££££¶¶££££¶¶££¶¶\r\n¶¶££££££££££££££££££££¶¶¶¶££££££¶¶\r\n_¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶¶____¶¶¶¶¶¶¶",
            };

            //Generics.PlayVisualsAtGameStart();

            Random random_titles = new Random(); //Generate a new Random object

            int rIndex = random_titles.Next(0, title_images.Count); //Create a random index for the List of titles

            string randomTitle = title_images[rIndex]; //Give the index to the List of titles

            Generics.Format_display(randomTitle); //Display the random title
            
            Generics.SpaceWriteLine("\n Welcome to FFXI Versus" + "\n" + "\n" + "This is a turn-based fighting mini-game" + "\n"+ "\n" + "Have fun ! \n ");
        }

        
        private Fighter CharacterSelect()
        {    
            var fighters = CharacterManager.InitializeDatabase();
            var fighterList = fighters.Find(_ => true).ToList();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please choose a fighter:")
                    .PageSize(10)
                    .AddChoices(fighterList.Select(fighter => fighter.CharName))
           );
           
            Fighter selectedHero = fighterList.FirstOrDefault(fighter => fighter.CharName == selection);

            Generics.SpaceWriteLine($"You chose {selectedHero?.CharName} to fight at your side !");
            selectedHero?.JoinFight();

            OpponentChoice(selectedHero);

            return selectedHero;

        }

        private void OpponentChoice(Fighter player)
        {
            var MatchupType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("\n Please select your the type of Matchup : choose [yellow]Random opponent[/] or go to [red]Opponent selection[/]")
                .AddChoices(new[]
                {
                    "[yellow]Random Matchup[/]", "[red]Opponent List[/]"
                }
                ));

            if (MatchupType == "[yellow]Random Matchup[/]")
            {
                ChooseRandomOpponent(player);
            }
            else if (MatchupType == "[red]Opponent List[/]")
            {
                DisplayOpponentList(player);
            }

        }

        private void ChooseRandomOpponent(Fighter player)
        {
                // Code pour choisir un adversaire au hasard dans la liste des combattants
                var fighters = CharacterManager.InitializeDatabase();
                var fighterList = fighters.Find(_ => true).ToList();

                Random random = new Random();
                int randomIndex = random.Next(0, fighterList.Count);

                Fighter randomOpponent = fighterList[randomIndex];

                Console.WriteLine($"Your random opponent is: {randomOpponent.CharName}");
               
                Entrance(player, randomOpponent);
        }

        private void DisplayOpponentList(Fighter player)
        {
                // display list of available opponents
                var fighters = CharacterManager.InitializeDatabase();
                var fighterList = fighters.Find(_ => true).ToList();

            var opponentSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please chose your opponent: ")
                .PageSize(10)
                .AddChoices(fighterList.Select(fighter => fighter.CharName))
                );

            Fighter selectedOpponent = fighterList.FirstOrDefault(fighter => fighter.CharName.Equals(opponentSelection));

                selectedOpponent?.JoinFight();
             
                Entrance(player, selectedOpponent);
        }

        private Fighter Entrance(Fighter player, Fighter opponent)
        {
            //Generics.PlayVisualsAtEntrance();

            Generics.SpaceWriteLine($"{player.CharName} has entered the battlefield !");

            Generics.SpaceWriteLine($"{opponent.CharName} has enterered the battlefield !");

            Generics.SpaceWriteLine($"{player.CharName} and {opponent.CharName} will fight each other will all their might !");

            Fighter playerReady = player.ActivateFighterPassivesAtBattleStart(player);

            Fighter opponentReady = opponent.ActivateFighterPassivesAtBattleStart(opponent);

            var combatCount = 0;

            Combat(playerReady, opponentReady, ref combatCount);

            return playerReady;           
        }

        private void Combat(Fighter player, Fighter cpu, ref int combatCount  )
        {
            var continueBattle = true;

            var playerInitiative = player.Spd + RandomNumberGenerator.GetInt32(1, 50);
            var cpuInitiative = cpu.Spd + RandomNumberGenerator.GetInt32(1, 50);

            while (continueBattle)
            {
                var combatMenu = new SelectionPrompt<string>()
                    .Title("Choose an action:")
                    .PageSize(4)
                    .AddChoices(new[]
                    {
                    "Attack", "Weaponskill", "Defend", "Spell"
                    });

                Generics.SpaceWriteLine($"Tp Jauge : {player.TpJauge}");

                var playerAction = AnsiConsole.Prompt(combatMenu);

                var playerTurn = playerInitiative > cpuInitiative;
               
                if (playerAction != null && playerTurn)
                {                    
                    switch (playerAction)
                    {
                        case "Attack":
                            combatManager.NormalAttack(player, cpu);
                            break;
                        case "WeaponSkill":
                            combatManager.WeaponSkill(player, cpu);
                            break;
                        case "Defend":
                            combatManager.Defend(player, cpu, 0);
                            break;
                        case "Spell":
                            combatManager.CastSpell(player, cpu);
                            break;
                    }
                }
                
                if (player.Hp <= 0 || cpu.Hp <= 0)
                {
                    continueBattle = false;
                }

                if (!continueBattle) 
                {
                    if (player.Hp <= 0)
                    {
                        Generics.SpaceWriteLine($"{player.CharName} falls to the ground.. {cpu.CharName} is victorious ! You've been defeated.");
                    }

                    if (cpu.Hp <= 0)
                    {
                        Generics.SpaceWriteLine($"{cpu.CharName} falls to the ground.. {player.CharName} is victorious ! You've won !");

                        combatCount++;

                        if (combatCount >= 7)
                        {
                            GetVictoryStreakCelebration();
                        }
                        else
                            GetReadyForNextBattle(player);
                    }                    
                }
            }                                       
        }        

        private void GetReadyForNextBattle(Fighter player)
        {
            Generics.SpaceWriteLine("A new challenger has come ! Get ready for the next battle !");
           
            ChooseRandomOpponent(player);
        }

        private void GetVictoryStreakCelebration()
        {
            Generics.SpaceWriteLine("You defeated all your opponents ! You are a true legend of the arena ! \n\n Do you want to continue ? ");

            var combatMenu = new SelectionPrompt<string>()
                    .Title("What will you do ?")
                    .PageSize(2)
                    .AddChoices(new[]
                    {
                    "Continue", "Stop Game"
                    });

            var playerAction = AnsiConsole.Prompt(combatMenu);
           
            switch (playerAction)
            {
                case "Continue":
                    CharacterSelect();
                    break;
                case "Stop Game":
                    Generics.GameExit();
                    break;                                 
            }
        }

        
    } 
}
