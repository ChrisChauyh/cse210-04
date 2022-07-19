using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unit04.Game.Casting;
using Unit04.Game.Directing;
using Unit04.Game.Services;


namespace Unit04
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 900;
        private static int CELL_SIZE = 25;
        private static int FONT_SIZE = 25;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Gems";

        private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_ARTIFACTS = 40;


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create the banner
            Actor banner = new Actor();
            banner.SetText("Hello");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the robot
            Actor robot = new Actor();
            robot.SetText("#");
            robot.SetFontSize(FONT_SIZE);
            robot.SetColor(WHITE);
            robot.SetPosition(new Point(MAX_X / 2,  MAX_Y- CELL_SIZE));
            cast.AddActor("Player", robot);

            // create the artifacts
            Random random = new Random();
            for (int i = 0; i < DEFAULT_ARTIFACTS; i++)
            {

                int x = random.Next(1, COLS);
                int y = random.Next(1, ROWS);
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                //create rocks
                Artifact rocks = new Artifact();
                rocks.SetText("O");
                rocks.SetFontSize(FONT_SIZE);
                rocks.SetColor(color);
                rocks.SetPosition(position);
                cast.AddActor("Rock", rocks);
                int x1 = random.Next(1, COLS);
                int y1 = random.Next(1, ROWS);
                Point position1 = new Point(x1, y1);
                position1 = position1.Scale(CELL_SIZE);

                int r1 = random.Next(0, 256);
                int g1 = random.Next(0, 256);
                int b1 = random.Next(0, 256);
                Color color1 = new Color(r1, g1, b1);
                //create gems
                Artifact gems = new Artifact();
                gems.SetText("*");
                gems.SetFontSize(FONT_SIZE);
                gems.SetColor(color1);
                gems.SetPosition(position1);
                cast.AddActor("Gems", gems);


            }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
    }
}