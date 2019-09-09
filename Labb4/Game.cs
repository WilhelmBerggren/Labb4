using System;
using System.Windows.Forms;

namespace Labb4
{
    public class Game
    {
        const int mapHeight = 5;
        const int mapWidth = 5;
        static Level level = new Level(mapHeight, mapWidth);
        static Player player = new Player(2, 2, 10);

        public void Start()
        {
            WinFormsDraw();
        }


        [STAThread]
        static void WinFormsDraw()
        {
            Application.Run(new Class1(level, player));
        }
    }
}