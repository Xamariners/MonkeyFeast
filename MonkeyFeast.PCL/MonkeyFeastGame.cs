using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using MonkeyFeast.PCL.Models;
using MonkeyFeast.PCL.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;

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

	    private int _score = 0;

        private Rectangle _screen;

	    private bool _gameOver = true;

	    public MonkeyFeastGame ()
		{
            _graphics = new GraphicsDeviceManager(this)
		    {
		        IsFullScreen = true,
		        SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight,
		    };
            
            Content.RootDirectory = "Content";

            Window.AllowUserResizing = true;

		    TouchPanel.EnabledGestures = GestureType.DoubleTap;

            _playPen = new PlayPen
		    {
		        Rectangle = new Rectangle(290, 100, 550, 340),
		        BeerColumn = new Random().Next(0, 5),
		        MonkeyColumn = 3,
		        BeerRow = -1
            };
		}

	    protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch (GraphicsDevice);
		    _background = Content.Load<Texture2D>("background");

            _font = Content.Load<SpriteFont> ("Font");
		    _beerSound = Content.Load<SoundEffect>("fire");

            _monkey = new SpriteSheet(Content, "monkeys", 1, 4, _playPen.MonkeyLocation());
		    _monkey.Size = _playPen.CellWidth / _monkey.Width;

		    _beer = new SpriteSheet(Content, "beers", 1, 4, _playPen.BeerLocation());
		    _beer.Size = _playPen.CellWidth / _beer.Width;
            
		    _playPen.Monkey = _monkey;
		    _playPen.Beer = _beer;
		}

	    protected override void UnloadContent()
	    {
	        Content.Unload();
	    }
	}
}