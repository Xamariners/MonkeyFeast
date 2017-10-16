using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using MonkeyFeast.PCL.Models;
using MonoGame.Extended;

namespace MonkeyFeast
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public partial class MonkeyFeastGame : Game
	{  
        private int _timeSinceLastGameFrame;
	    private int _timeSinceLastBeerFrame;
	    private const int BEER_SPEED = 1000;


        private const int _gameMSPerFrame = 200;
	    private int _beerMSPerFrame = BEER_SPEED;

        protected override void Update (GameTime gameTime)
        {   
            _timeSinceLastGameFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (_timeSinceLastGameFrame > _gameMSPerFrame)
            {
                _timeSinceLastGameFrame -= _gameMSPerFrame;
              
                KeyUpdate(gameTime);

                TouchUpdate(gameTime);

                _timeSinceLastGameFrame = 0;
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
	            MonkeyGoesLeft(gameTime);

	        if (keyboardState.IsKeyDown(Keys.Right) || gamePadState.ThumbSticks.Right.X > 0f)
	            MonkeyGoesRight(gameTime);

	        if (_gameOver && keyboardState.IsKeyDown(Keys.Space))
                StartGame();
                
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
	                _monkey.Location = _playPen.MonkeyLocation();
                }
	        }
        }

	    private void StartGame(bool isGameOver = false)
	    {  
	        _score = 0;
	        _beerMSPerFrame = BEER_SPEED;

            _playPen = new PlayPen(_scale)
	        {
	            Area = new RectangleF(200 * _scale, 82 * _scale, 420 * _scale, 256 * _scale).ToRectangle(),
                BeerColumn = new Random().Next(0, 5),
	            MonkeyColumn = 2,
	            BeerRow = 0
	        };

	        if (!isGameOver && _monkey != null)
	        {
                // resets sprites position
	            _playPen.Beer = _beer;
	            _playPen.Monkey = _monkey;
                _monkey.Location = _playPen.MonkeyLocation();
	            _beer.Location = _playPen.BeerLocation();
	        }

	        _gameOver = isGameOver;
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

                    if(_beerMSPerFrame > 400)
	                    _beerMSPerFrame = _beerMSPerFrame - 100;
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