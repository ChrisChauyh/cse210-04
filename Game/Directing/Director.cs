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
            Actor banner = (Actor)cast.GetFirstActor("banner");
            Actor robot = (Actor)cast.GetFirstActor("Player");
            List<Actor> Rocks = cast.GetActors("Rock");
            List<Actor> Gems = cast.GetActors("Gems");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);
            banner.SetText("Score: " + 0);


            foreach (Actor actor in Rocks)
            {
                Point getlocation = actor.GetPosition();
                getlocation.Down(20);
                actor.SetPosition(getlocation);
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    Artifact artifact = (Artifact) actor;
                    score++;
                    banner.SetText("Score: " + score);
                }
            } 
            foreach (Actor actor1 in Gems)
            {
                Point getlocation = actor1.GetPosition();
                getlocation.Down(20);
                actor1.SetPosition(getlocation);
                if (robot.GetPosition().Equals(actor1.GetPosition()))
                {
                    Artifact artifact = (Artifact) actor1;
                    score--;
                    banner.SetText("Score: " + score);
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