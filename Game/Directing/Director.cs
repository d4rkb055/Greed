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
        private int MAX_FALLING_BOXES = 200;

        private string mode;

        // DateTime lastSpawn =  

        
        
        private Score score;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService, string mode)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
            this.score = new Score();
            this.mode = mode;
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
                gem.SetText("*");

                FallingObject rock = new FallingObject(-1);
                x *= 15;
                rock.SetPosition(new Point(x, 0));
                rock.SetText("O");
                rock.SetVelocity(new Point(0, 15));
                if(mode == "medium")
                {
                    int x1 = random.Next(-5, 5);
                    gem.SetVelocity(new Point(x1, 10));
                }
                else if(mode == "hard")
                {
                    int x2 = random.Next(-10, 10);
                    int y1 = random.Next(1, 15);
                    gem.SetVelocity(new Point(x2, y1));
                }
                else if(mode == "hero")
                {
                    int x3 = random.Next(-15, 15);
                    int y3 = random.Next(1, 15);
                    gem.SetVelocity(new Point(x3, y3));
                }
                else
                {
                    mode = "normal";
                    gem.SetVelocity(new Point(0, 10));
                }
                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);
                gem.SetColor(color);
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
                rock.SetText("O");
                rock.SetVelocity(new Point(0, 15));
                int r = 150;
                int g = 150;
                int b = 150;
                Color color = new Color(r, g, b);
                rock.SetColor(color);
                cast.AddActor("fallingObjects", rock);
                if(mode == "hard")
                {
                    int x1 = random.Next(-10, 10);
                    int y1 = random.Next(1, 15);
                    rock.SetVelocity(new Point(x1, y1));
                }
                else if(mode == "hero")
                {
                    int x3 = random.Next(-15, 15);
                    int y3 = random.Next(1, 15);
                    rock.SetVelocity(new Point(x3, y3));
                }
            
            }
            List<Actor> fallingBoxes = cast.GetActors("fallingObjects");
            if (fallingBoxes.Count < MAX_FALLING_BOXES)
            {
                Random random = new Random();
                int p = random.Next(-100, 100);
                FallingObject box = new FallingObject(p);
                int x = random.Next(0, 60);
                x *= 15;
                box.SetPosition(new Point(x, 0));
                box.SetText("0");
                box.SetVelocity(new Point(0, 5));
                if(mode == "hero")
                {
                    int x3 = random.Next(-15, 15);
                    int y3 = random.Next(1, 15);
                    box.SetVelocity(new Point(x3, y3));
                }
                int r = 255;
                int g = 105;
                int b = 180;
                Color color = new Color(r, g, b);
                box.SetColor(color);
                cast.AddActor("fallingObjects", box);                

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
            if(mode == "hero")
            {
                Point velocity = keyboardService.GetHeroDirection();
                greedyBoy.SetVelocity(velocity); 
            }
            else
            {
                Point velocity = keyboardService.GetDirection();
                greedyBoy.SetVelocity(velocity); 
            }     
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
                    switch (fallingObject.GetText())
                    {
                        case "*":
                            // cast.GetFirstActor("score");
                            score.HitObject(1);
                            score.SetText(score.GetScoreMessage());
                            break;
                        case "O":
                            score.HitObject(-1);
                            score.SetText(score.GetScoreMessage());
                            break;
                        case "0":
                            Random random = new Random();
                            int p = random.Next(-100, 100);
                            score.HitObject(p);
                            score.SetText(score.GetScoreMessage());
                            break;
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