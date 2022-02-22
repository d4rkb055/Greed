using System;
using System.Collections.Generic;
using Greed.Game.Casting;
using Greed.Game.Services;



namespace Greed.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        private int MAX_FALLING_ROCKS = 230;
        private int MAX_FALLING_GEMS = 230;
        
        private Score score;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
            this.score = new Score();
        }

        private void AddNewObjects(Cast cast)
        {
            List<Actor> fallingGems = cast.GetActors("fallingObjects");
            if (fallingGems.Count < MAX_FALLING_GEMS)
            {
                FallingObject gem = new FallingObject(1);
                Random random = new Random();
                int x = random.Next(0, 60);
                x *= 15;
                gem.SetPosition(new Point(x, 0));
                cast.AddActor("fallingObjects", gem);
            }
            List<Actor> fallingRocks = cast.GetActors("fallingObjects");
            if (fallingRocks.Count < MAX_FALLING_ROCKS)
            {
                FallingObject rock = new FallingObject(-1);
                Random random = new Random();
                int x = random.Next(0, 60);
                x *= 15;
                rock.SetPosition(new Point(x, 0));
                cast.AddActor("fallingObjects", rock);
            }
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            cast.AddActor("text", score);
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the greedyBoy.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor greedyBoy = cast.GetFirstActor("greedyBoy");
            Point velocity = keyboardService.GetDirection();
            greedyBoy.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the greedyBoy's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            AddNewObjects(cast);
            Actor greedyBoy = cast.GetFirstActor("greedyBoy");
            List<Actor> fallingObjects = cast.GetActors("fallingObjects");

            // // Create list of rock to be added to cast
            // List<FallingObject> Rocks = cast.GetActors("Rocks");
            // Random random = new Random();
            // Rocks.SetText("*");
            // Rocks.SetFontSize(20);
            // Rocks.SetVelocity(new Point(0, 5));
            // Rocks.SetPosition(new Point(15,0));
            // int x = random.Next(1, COLS);
            // int y = 0;
            // Point position = new Point (x, y);
            // position = position.Scale(CELL_SIZE);
            // int r = random.Next(0, 256);
            // int g = random.Next(0, 256);
            // int b = random.Next(0, 256);
            // Color color = new Color(r, g, b);
            // Rocks.SetColor(color);
            // cast.AddActor("fallingObjects", Rocks);

            // // Creat list of Gems to be added to cast
            // List<FallingObject> Gems = cast.GetActors("Gems");
            // Random random = new Random();
            // Gems.SetText("*");
            // Gems.SetFontSize(20);
            // Gems.SetVelocity(new Point(0, 5));
            // Gems.SetPosition(new Point(15,0));
            // int x = random.Next(1, COLS);
            // int y = 0;
            // Point position = new Point (x, y);
            // position = position.Scale(CELL_SIZE);
            // int r = random.Next(0, 256);
            // int g = random.Next(0, 256);
            // int b = random.Next(0, 256);
            // Color color = new Color(r, g, b);
            // Gems.SetColor(color);
            // cast.AddActor("fallingObjects", Gems);

            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            greedyBoy.MoveNext(maxX, maxY);

            foreach (Actor fallingObject in fallingObjects)
            {
                fallingObject.MoveNext(maxX, maxY);
                if (greedyBoy.GetPosition().GetX() >= fallingObject.GetPosition().GetX()-7 &&
                    greedyBoy.GetPosition().GetX() <= fallingObject.GetPosition().GetX()+7 &&
                    greedyBoy.GetPosition().GetY() >= fallingObject.GetPosition().GetY()-7 &&
                    greedyBoy.GetPosition().GetY() <= fallingObject.GetPosition().GetY()+7)
                    // if (greedyBoy.GetPosition() == fallingObject.GetPosition())
                {
                    cast.RemoveActor("fallingObjects",fallingObject);
                    
                    // Score score = new Score();
                    // score = (Score) cast.GetFirstActor("score");
                    if(fallingObject.GetText() == "*")
                    {
                        // cast.GetFirstActor("score")
                        score.HitObject(1);
                        score.SetText(score.GetScoreMessage());
                    }
                    else if (fallingObject.GetText() == "O")
                    {
                        score.HitObject(-1);
                        score.SetText(score.GetScoreMessage());
                    }

                    
                }

            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}