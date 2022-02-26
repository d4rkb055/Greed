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
        protected static int MAX_X = 900;
        protected static int MAX_Y = 600;
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
            string mode = "easy";
            if(args.Count() > 0)
            {
                mode = args[0];
            }
            // create the cast
            Cast cast = new Cast();

            // create the greedyBoy
            Actor greedyBoy = new Actor();
            greedyBoy.SetText("#");
            greedyBoy.SetColor(WHITE);
            greedyBoy.SetPosition(new Point(MAX_X / 2, MAX_Y - 15));
            cast.AddActor("greedyBoy", greedyBoy);
            if(mode == "hero")
                {
                    greedyBoy.SetPosition(new Point(MAX_X / 2, MAX_Y / 2));
                    cast.AddActor("greedyBoy", greedyBoy);
                }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService, mode);
            director.StartGame(cast);
        }
    }
}