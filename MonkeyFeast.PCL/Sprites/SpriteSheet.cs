using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonkeyFeast.PCL.Sprites
{
    public class SpriteSheet
    {
        private int _currentCell;
        private readonly int _totalCells;

        private readonly int _width;
        private readonly int _height;
        private readonly Texture2D _texture;
      
        public int Rows { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Columns { get; private set; }
        public Vector2 Location { get; set; }
        public float Width => _width * Size;
        public float Height => _height * Size;
        public float Size { get; set; }

        public Action OnUpdate { get; set; }

        public SpriteSheet(ContentManager content, string textureName, int rows, int columns, Vector2 location, float size = 1)
        {
            Rows = rows;
            Columns = columns;

            _currentCell = 0;
            _totalCells = Rows * Columns;

            _texture = content.Load<Texture2D>(textureName);

            _width = _texture.Width / Columns;
            _height = _texture.Height / Rows;

            Location = location;

            Size = size;
        }

        public virtual void Update(GameTime gameTime, int? currentCell = null)
        {
            if (currentCell.HasValue)
                _currentCell = currentCell.Value;

            Row = _currentCell / Columns;
            Column = _currentCell % Columns;

            OnUpdate?.Invoke();

            _currentCell++;

            if (_currentCell == _totalCells)
                _currentCell = 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = new Rectangle(_width*Column,_height*Row, _width, _height);

            var destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y,
                (int)Width, (int)Height);
            
            spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}