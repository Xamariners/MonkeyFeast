﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.TextureAtlases;

namespace MonkeyFeast
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public partial class MonkeyFeastGame : Game
	{
		protected override void Draw (GameTime gameTime)
		{
            _graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			
			_spriteBatch.Begin ();
		    _spriteBatch.Draw(_background, destinationRectangle: _screen);

            //var v = font.MeasureString ("Game Over");
            //spriteBatch.DrawString (font, "Game Over",
            //	new Vector2 (GraphicsDevice.Viewport.Width - v.X - 30, 30),
            //	Color.MonoGameOrange);
            _spriteBatch.End ();

			base.Draw (gameTime);
		}
    }
}