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
	    private Vector2 _monkeyLocation;
	    private KeyboardState _previousKeyboardState;
	    private GamePadState _previousGamePadState;

	    private int _currentFrame;
	    private readonly int _totalFrames;
        private int _timeSinceLastFrame;
	    private const int MS_PER_FRAME = 200;


	    protected override void Update (GameTime gameTime)
        {
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceLastFrame > MS_PER_FRAME)
            {
                _timeSinceLastFrame -= MS_PER_FRAME;

                _screen = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

                KeyUpdate(gameTime);

                TouchUpdate(gameTime);

                _timeSinceLastFrame = 0;
            }

            base.Update(gameTime);
        }

	    private void KeyUpdate(GameTime gameTime)
	    {
	        var keyboardState = Keyboard.GetState();
	        var gamePadState = GamePad.GetState(PlayerIndex.One);

	        //if (!(_previousKeyboardState != keyboardState || _previousGamePadState != gamePadState))
	        //    return;

            if (keyboardState.IsKeyDown(Keys.Left) || gamePadState.ThumbSticks.Right.X < 0f)
	        {
	            MonkeyGoesLeft(gameTime);
	        }

	        if (keyboardState.IsKeyDown(Keys.Right) || gamePadState.ThumbSticks.Right.X > 0f)
	        {
	            MonkeyGoesRight(gameTime);
            }

	        if (keyboardState.IsKeyDown(Keys.Space) || gamePadState.IsButtonDown(Buttons.A))
	        {
	            fire.Play();
	        }
        }

	    private void TouchUpdate(GameTime gameTime)
	    {
	        var touchState = TouchPanel.GetState();
	        //var previousTouchState = new Vector2();

	        foreach (var touch in touchState)
	        {
	            if (touch.State != TouchLocationState.Released) //&& touch.Position != previousTouchState
                {
	                if (touch.Position.X < (TouchPanel.DisplayWidth / 2))
	                {
	                   MonkeyGoesLeft(gameTime);
	                }

	                if (touch.Position.X > (TouchPanel.DisplayWidth / 2))
	                {
	                    MonkeyGoesRight(gameTime);
                    }
	            }

	            //previousTouchState = touch.Position;
	        }

	        while (TouchPanel.IsGestureAvailable)
	        {
	            var gesture = TouchPanel.ReadGesture();
	            if (gesture.GestureType == GestureType.DoubleTap)
	            {
	                fire.Play();
	            }
	        }
        }

        private void MonkeyGoesLeft(GameTime gameTime)
	    {
	        if (_playPen.MonkeyPosition > 0)
	        {
	            _monkey.Location = new Vector2(_monkey.Location.X - _monkey.Width, _monkey.Location.Y);
	            _playPen.MonkeyPosition--;
	        }


	        _monkey.Update(gameTime);
        }

	    private void MonkeyGoesRight(GameTime gameTime)
	    {
	        if (_playPen.MonkeyPosition < 4)
	        {
	            _monkey.Location = new Vector2(_monkey.Location.X + _monkey.Width, _monkey.Location.Y);
	            _playPen.MonkeyPosition++;
	        }

	        _monkey.Update(gameTime);
        }
    }
}