using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;
using System;

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

        public int score { get; set; }= 20;
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;

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
            Actor robot = cast.GetFirstActor("Player");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);    

        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");

            List<Actor> Rocks = cast.GetActors("Rock");
            List<Actor> Gems = cast.GetActors("Gems");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
                            Actor robot = cast.GetFirstActor("Player");
                                                        robot.MoveNext(maxX, maxY);



            foreach (Actor actor in Rocks)
            {

                Point getlocation = actor.GetPosition();

                getlocation.Down(15);
                actor.SetPosition(getlocation);
                
                if (getlocation == robot.GetPosition())
                {
                    Artifact artifact = (Artifact) actor;
                    score++;
                    Console.WriteLine(score);
                }
            } 
            foreach (Actor actor in Gems)
            {
                Point getlocation = actor.GetPosition();
                getlocation.Down(10);
                actor.SetPosition(getlocation);
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    Artifact artifact = (Artifact) actor;
                    score--;
                }

            } 
                        banner.SetText("Score: " + score);
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