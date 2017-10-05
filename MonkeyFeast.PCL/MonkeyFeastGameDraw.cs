using System;

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

		    _spriteBatch.Draw(_background, _screen, Color.White);
            _monkey.Draw(_spriteBatch);
		    _beer.Draw(_spriteBatch);

		    DrawScore();

		    DrawGameOver();

            _spriteBatch.End();

			base.Draw (gameTime);
		}

	    private void DrawScore()
	    {
	        _spriteBatch.DrawString(_font, _score.ToString(),
                new Vector2(_playPen.Area.Left + (20 * _scale), _playPen.Area.Top + (20 * _scale)),
	            Color.MonoGameOrange);
        }

	    private void DrawGameOver()
	    {
	        if (!this._gameOver)
	            return;

	        var gameOver = "GAME OVER";
	        _spriteBatch.DrawString(_font, gameOver,
	            new Vector2(_playPen.Area.Center.X - (100 * _scale), _playPen.Area.Center.Y),
                Color.MonoGameOrange);
	    }

        private void DrawRectangle(int width, int height, Color color)
	    {
	        var texture = new Texture2D(this.GraphicsDevice, width, height);
	        Color[] colorData = new Color[width * height];
	        for (int i = 0; i < (width * height); i++)
	            colorData[i] = color;

            texture.SetData<Color>(colorData);

	        _spriteBatch.Draw(texture, _playPen.Area, Color.OrangeRed);
        }
    }
}