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
	    private int _timeSinceLastBeerFrame;
        private const int MS_PER_FRAME = 200;
	    private int _beerMSPerFrame = 1000;

        protected override void Update (GameTime gameTime)
        {
            _screen = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (_timeSinceLastFrame > MS_PER_FRAME)
            {
                _timeSinceLastFrame -= MS_PER_FRAME;
              
                KeyUpdate(gameTime);

                TouchUpdate(gameTime);

                _timeSinceLastFrame = 0;
            }

            BeerUpdate(gameTime);

            base.Update(gameTime);
        }

	    private void BeerUpdate(GameTime gameTime)
	    {
	        if (_gameOver)
	            return;

	        _timeSinceLastBeerFrame += gameTime.ElapsedGameTime.Milliseconds;

	        if (_timeSinceLastBeerFrame > _beerMSPerFrame)
	        {
                BeerMoves(gameTime);
	            _timeSinceLastBeerFrame = 0;
	        }
	    }

	    private void KeyUpdate(GameTime gameTime)
	    {
	        var keyboardState = Keyboard.GetState();
	        var gamePadState = GamePad.GetState(PlayerIndex.One);

            if (keyboardState.IsKeyDown(Keys.Left) || gamePadState.ThumbSticks.Right.X < 0f)
	        {
	            MonkeyGoesLeft(gameTime);
	        }

	        if (keyboardState.IsKeyDown(Keys.Right) || gamePadState.ThumbSticks.Right.X > 0f)
	        {
	            MonkeyGoesRight(gameTime);
            }
        }

	    private void TouchUpdate(GameTime gameTime)
	    {
	        var touchState = TouchPanel.GetState();
	      
	        foreach (var touch in touchState)
	        {
	            if (touch.State != TouchLocationState.Released)
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
	        }

	        while (TouchPanel.IsGestureAvailable)
	        {
	            var gesture = TouchPanel.ReadGesture();
	            if (_gameOver && gesture.GestureType == GestureType.DoubleTap)
	            {
	                StartGame();
	            }
	        }
        }

	    private void StartGame()
	    {
	        _gameOver = false;
	        _score = 0;
	        _playPen.BeerColumn = new Random().Next(0, 5);
	        _playPen.MonkeyColumn = 3;
            _playPen.BeerRow = -1;
        }

        private void MonkeyGoesLeft(GameTime gameTime)
	    {
	        if (_gameOver)
	            return;

            if (_playPen.MonkeyColumn > 0)
	        {
	            _monkey.Location = new Vector2(_monkey.Location.X - _monkey.Width, _monkey.Location.Y);
	            _playPen.MonkeyColumn--;
	        }

	        _monkey.Update(gameTime);
        }

	    private void MonkeyGoesRight(GameTime gameTime)
	    {
	        if (_gameOver)
	            return;

            if (_playPen.MonkeyColumn < 4)
	        {
	            _monkey.Location = new Vector2(_monkey.Location.X + _monkey.Width, _monkey.Location.Y);
	            _playPen.MonkeyColumn++;
	        }

	        _monkey.Update(gameTime);
        }

	    private void BeerMoves(GameTime gameTime)
	    {
	        if (_playPen.BeerRow < 3)
	        {
                _playPen.BeerRow++;
	            _beerSound.Play();
            }

	        else if (_playPen.BeerRow < 4)
	        {
	            if (_playPen.MonkeyColumn == _playPen.BeerColumn)
	            {
                    // add points
	                _score++;
	                _playPen.BeerColumn = new Random().Next(0, 5);
	                _playPen.BeerRow = 0;
	            }
	            else
	            {
	                _playPen.BeerRow++;
	                _beerSound.Play();
                }
	        }

	        else if (_playPen.BeerRow >= 4)
	        {
	            this._gameOver = true;
	        }

	        _beer.Location = _playPen.BeerLocation();

            _beer.Update(gameTime);
        }
    }
}