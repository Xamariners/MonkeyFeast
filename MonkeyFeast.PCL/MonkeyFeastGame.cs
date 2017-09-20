using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
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
        }

	    protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch (GraphicsDevice);
		    _background = Content.Load<Texture2D>("background");

            _font = Content.Load<SpriteFont> ("Font");
            fire = Content.Load<SoundEffect>("fire");
		}

	    protected override void UnloadContent()
	    {
	        Content.Unload();
	    }
    }
}