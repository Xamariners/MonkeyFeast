using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using MonkeyFeast.PCL.Models;
using MonkeyFeast.PCL.Sprites;

using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;

namespace MonkeyFeast
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public partial class MonkeyFeastGame : Game
	{
	    private GraphicsDeviceManager _graphics;

	    private PlayPen _playPen;

        SpriteBatch _spriteBatch;

        private Texture2D _background;

        private SpriteSheet _monkey;

	    private SpriteSheet _beer;
	  
        SpriteFont _font;

        SoundEffect _beerSound;

	    private int _score;

        private Rectangle _screen;

	    private bool _gameOver;

	    private float _scale;

	    private Rectangle _bounds;

       
	    public MonkeyFeastGame ()
		{
            _graphics = new GraphicsDeviceManager(this)
		    {
		        IsFullScreen = true,
		        SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight,
		    };

            Window.AllowUserResizing = true;

		    TouchPanel.EnabledGestures = GestureType.DoubleTap;

		    _gameOver = true;
		    _score = 0;
		}

	    protected override void LoadContent ()
	    {
	        _screen = this.GraphicsDevice.Viewport.Bounds;

            Content.RootDirectory = "Content";
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch (GraphicsDevice);
            _background = Content.Load<Texture2D>("background");
            _bounds = _background.Bounds;

	        _scale = (float)_screen.Height / (float)_bounds.Height;

            _playPen = new PlayPen(_scale)
	        {
	            Area = new RectangleF(192 * _scale, 82 * _scale, 386 * _scale, 256 * _scale).ToRectangle(),
	            BeerColumn = new Random().Next(0, 5),
	            MonkeyColumn = 3,
	            BeerRow = -1
	        };

            _font = Content.Load<SpriteFont> ("Font");
		    _beerSound = Content.Load<SoundEffect>("fire");

            _monkey = new SpriteSheet(Content, "monkeys", 1, 4, new Vector2());
		    _monkey.Size = (_playPen.CellWidth / _monkey.Width);

		    _beer = new SpriteSheet(Content, "beers", 1, 4, _playPen.BeerLocation());
		    _beer.Size = (_playPen.CellWidth / _beer.Width);
            

		    _playPen.Monkey = _monkey;
		    _playPen.Beer = _beer;

	        _monkey.Location = _playPen.MonkeyLocation();
	    }

	    protected override void UnloadContent()
	    {
	        Content.Unload();
	    }
	}
}