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
        
        SpriteBatch _spriteBatch;

        private Texture2D _background;

        private SpriteSheet _monkey;

	    private PlayPen _playPen;
	  
        SpriteFont _font;

        SoundEffect fire;

        private Rectangle _screen;
		
		public MonkeyFeastGame ()
		{
            _graphics = new GraphicsDeviceManager(this)
		    {
		        IsFullScreen = true,
		        SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight,
		    };
            
            Content.RootDirectory = "Content";

            Window.AllowUserResizing = true;

		    _playPen = new PlayPen
		    {
		        Rectangle = new Rectangle(290, 100, 550, 340),
		        MonkeyPosition = 3
		    };
		}

	    protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch (GraphicsDevice);
		    _background = Content.Load<Texture2D>("background");

            _font = Content.Load<SpriteFont> ("Font");
            fire = Content.Load<SoundEffect>("fire");

            _monkey = new SpriteSheet(Content, "monkeys", 1, 4, _playPen.MonkeyLocation());
		    _monkey.Size = _playPen.CellWidth / _monkey.Width;

		    _playPen.Monkey = _monkey;
		}

	    protected override void UnloadContent()
	    {
	        Content.Unload();
	    }
    }
}