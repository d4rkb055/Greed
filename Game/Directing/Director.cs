using System;
using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;


namespace Unit04.Game.Directing
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

        


        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        private void AddNewObjects(Cast cast)
        {
            List<Actor> fallingRocks = cast.GetActors("fallingObjects");
            if (fallingRocks.Count < MAX_FALLING_ROCKS)
            {
                Rock rock = new Rock();
                Random random = new Random();
                int x = random.Next(0, 60);
                x *= 15;
                rock.SetPosition(new Point(x, 0));
                cast.AddActor("fallingObjects", rock);
            }
            List<Actor> fallingGems = cast.GetActors("fallingObjects");
            if (fallingGems.Count < MAX_FALLING_GEMS)
            {
                Gem gem = new Gem();
                Random random = new Random();
                int x = random.Next(0, 60);
                x *= 15;
                gem.SetPosition(new Point(x, 0));
                cast.AddActor("fallingObjects", gem);
            }

        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
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
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            AddNewObjects(cast);
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> fallingObjects = cast.GetActors("fallingObjects");

            
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Actor actor in fallingObjects)
            {
                actor.MoveNext(maxX, maxY);

                // Collision test
                if (robot.GetPosition().GetX() >= actor.GetPosition().GetX()-7 && 
                    robot.GetPosition().GetX() <= actor.GetPosition().GetX()+7 &&
                    robot.GetPosition().GetY() >= actor.GetPosition().GetY()-7 &&
                    robot.GetPosition().GetY() <= actor.GetPosition().GetY()+7)
                {
                    // Artifact artifact = (Artifact) actor;
                    cast.RemoveActor("fallingObjects", actor);
                    
                    FallingObject fallingObject = (FallingObject) actor;
                    

                    //  This will work once you implement a ScoreBoard class
                    // ScoreBoard.UpdateScore(fallingObject.GetScoreValue);
                    
                }
                // If statement to delete falling objects as they hit bottom row goes here?
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