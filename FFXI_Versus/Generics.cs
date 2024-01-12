using NAudio.Wave;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus
{
    public class Generics
    {

        public static void SpaceWriteLine(string message)
        {
            Console.WriteLine($"\n {message.PadLeft(message.Length + 5)} ");
        }

        public static void Format_display(string message)
        {
            Console.WriteLine(message.PadLeft(message.Length + 5));
        }

        public static void CharSpeaks(string sentence)
        { 
            int margin = 6;

            Console.WriteLine();

            foreach (var letter in sentence)
            {
                Console.Write(new string(' ', margin));
                Console.Write(letter);
                Thread.Sleep(40);

                margin = 0;
            }
            
            Console.WriteLine();
        }


        public static void PlayAudio(string filePath)
        {
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Wait for playing to finish
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
        }

        public static void PlayVisualsAtGameStart()
        {
            var gameStartVisual = new CanvasImage("C:\\Users\\spect\\source\\repos\\FinalFantasyXI\\FFXI_Versus\\Resources\\Images\\FFFXI_Versus_Start.jpg");

            AnsiConsole.Write(gameStartVisual);
        }

        public static void PlayVisualsAtEntrance()
        {
            var combatEntranceVisual = new CanvasImage("C:\\Users\\spect\\source\\repos\\FinalFantasyXI\\FFXI_Versus\\Resources\\Images\\FFXI_fight.jpg");

            AnsiConsole.Write(combatEntranceVisual);

        }

        internal static void GameExit()
        {
            SpaceWriteLine("Thank you for playing ! Hope you enjoyed !");
            
            Environment.Exit(0);
        }
        
    }
}
