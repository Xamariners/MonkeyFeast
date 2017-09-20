using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace MonkeyFeast
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public partial class MonkeyFeastGame : Game
	{
		protected override void Update (GameTime gameTime)
		{
            var keyboardState = Keyboard.GetState ();
			var gamePadState = GamePad.GetState (PlayerIndex.One);
			var touchState = TouchPanel.GetState ();

		    _screen = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

           
            if (keyboardState.IsKeyDown (Keys.Left) || gamePadState.ThumbSticks.Right.X < 0f)
            {
                ;
            }
			if (keyboardState.IsKeyDown (Keys.Right) || gamePadState.ThumbSticks.Right.X > 0f)
			{
			    ;
			}
            if (keyboardState.IsKeyDown (Keys.Space) || gamePadState.IsButtonDown (Buttons.A))
            {
               fire.Play();
            }
			foreach (var touch in touchState) {
				if (touch.State != TouchLocationState.Released) {
                    if (touch.Position.X < (TouchPanel.DisplayWidth / 2))
                    {
                        ;
                    }
					if (touch.Position.X > (TouchPanel.DisplayWidth / 2))
					{
					    ;
					}
				}
			}

            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.DoubleTap)
                {
                    fire.Play();
                }
            }

			base.Update (gameTime);
		}
    }
}