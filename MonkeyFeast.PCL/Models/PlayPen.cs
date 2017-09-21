using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonkeyFeast.PCL.Sprites;

namespace MonkeyFeast.PCL.Models
{
    public class PlayPen
    {
        private const int PLAYPEN_COLUMNS = 5;
        private const int PLAYPEN_ROWS = 5;

        public Rectangle Rectangle { get; set; }

        public SpriteSheet Beer { get; set; }
        public int BeerColumn { get; set; }
        public int  BeerRow { get; set; }

        public SpriteSheet Monkey { get; set; }
        public int MonkeyColumn { get; set; }
        

        public int[,] EnnemyPosition = new int[PLAYPEN_COLUMNS, PLAYPEN_ROWS];

        public int CellWidth => Rectangle.Width / PLAYPEN_COLUMNS;
        public int CellHeight => Rectangle.Height / PLAYPEN_ROWS;

        public Vector2 MonkeyLocation()
        { 
            var monkeyHeight = Monkey?.Height ?? 130;
            return new Vector2(Rectangle.X + (CellWidth * MonkeyColumn-1), Rectangle.Bottom - monkeyHeight);
        }

        public Vector2 BeerLocation()
        {
            return new Vector2(Rectangle.X + (CellWidth * BeerColumn-1), Rectangle.Top + (BeerRow*CellHeight));
        }
    }
}