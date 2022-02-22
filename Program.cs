using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Greed.Game.Casting;
using Greed.Game.Directing;
using Greed.Game.Services;


namespace Greed
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Greed";
        private static Color WHITE = new Color(255, 255, 255);
        // private static int DEFAULT_ARTIFACTS = 40;


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create score object
            // Score score = new Score();
            // cast.AddActor("Score", score);

            // create the greedyBoy
            Actor greedyBoy = new Actor();
            greedyBoy.SetText("#");
            greedyBoy.SetColor(WHITE);
            greedyBoy.SetPosition(new Point(MAX_X / 2, MAX_Y - 15));
            cast.AddActor("greedyBoy", greedyBoy);

            // Create rock object
            FallingObject rock = new FallingObject(-1);
            rock.SetText("O");
            rock.SetVelocity(new Point(0, 5));
            cast.AddActor("fallingObjects", rock);
            
            // Create Gem object 
            FallingObject gem = new FallingObject(1);
            Random random = new Random();
            gem.SetText("*");
            gem.SetFontSize(20);
            gem.SetVelocity(new Point(0, 5));
            gem.SetPosition(new Point(15,0));
            int x = random.Next(1, COLS);
            int y = 0;
            Point position = new Point (x, y);
            position = position.Scale(CELL_SIZE);
            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);
            Color color = new Color(r, g, b);
            gem.SetColor(color);
            cast.AddActor("fallingObjects", gem);

            
            // create the artifacts
            // Random random = new Random();
            // for (int i = 0; i < DEFAULT_ARTIFACTS; i++)
            // {
            //     string text = ((char)random.Next(42, 43)).ToString();

            //     int x = random.Next(1, COLS);
            //     int y = random.Next(1, ROWS);
            //     Point position = new Point(x, y);
            //     position = position.Scale(CELL_SIZE);

            //     int r = random.Next(0, 256);
            //     int g = random.Next(0, 256);
            //     int b = random.Next(0, 256);
            //     Color color = new Color(r, g, b);

            //     Score score = new Score();
            //     score.SetFontSize(FONT_SIZE);
            //     score.SetColor(color);
            //     score.SetPosition(position);
            //     cast.AddActor("score", score);
            // }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
    }
}