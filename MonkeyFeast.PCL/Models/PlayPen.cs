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
        private float _scale;

        private const int PLAYPEN_COLUMNS = 5;
        private const int PLAYPEN_ROWS = 5;

        public Rectangle Area { get; set; }

        public SpriteSheet Beer { get; set; }
        public int BeerColumn { get; set; }
        public int  BeerRow { get; set; }

        public SpriteSheet Monkey { get; set; }
        public int MonkeyColumn { get; set; }
 

        public int CellWidth => Area.Width / PLAYPEN_COLUMNS;
        public int CellHeight => Area.Height / PLAYPEN_ROWS;

        public PlayPen(float scale)
        {
            _scale = scale;
        }

        public Vector2 MonkeyLocation()
        { 
            var monkeyHeight = Monkey.Height * _scale;

            // we draw from top to bottom / left to right
            return new Vector2(Area.X + (CellWidth * MonkeyColumn - 1), Area.Bottom - monkeyHeight);
        }

        public Vector2 BeerLocation()
        {
            // we draw from top to bottom / left to right
            return new Vector2(Area.X + (CellWidth * BeerColumn - 1), Area.Top + (BeerRow * CellHeight));
        }
    }
}