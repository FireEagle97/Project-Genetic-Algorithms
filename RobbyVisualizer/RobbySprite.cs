using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;

namespace RobbyVisualizer{
    public class RobbySprite : DrawableGameComponent{
        
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture;
        private Texture2D _ball;
        private Texture2D _robby;
        private SpriteFont _generation;
        private SpriteFont _points;
        private SpriteFont _moves;
        private double[] _ballX;
        private double[] _ballY;
        private Random _rnd ;
        private double[] _randBallXs;
        private double[] _randBallYs;
        private int _tileSize;
        private int _numOfBalls; 
        private Game _game;

        public RobbySprite(Game game): base(game){
            _game = game;
        }

        public override void Draw(GameTime gameTime)
        {   _spriteBatch.Begin();
            
    
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    _spriteBatch.Draw(_tileTexture, new Rectangle(i *_tileSize , j * _tileSize, _tileSize, _tileSize), Color.White); 
                }
            }
            _spriteBatch.End();
            
            
           _spriteBatch.Begin();
            for (int j = 0; j < _numOfBalls; ++j){
                //make sure robby place is empty
                _spriteBatch.Draw(_ball,new Vector2((float)_randBallXs[j], (float)_randBallYs[j]), Color.White);
            }
            _spriteBatch.DrawString(_generation,"Generation",new Vector2(10,510), Color.White);
            //(0,1) == (22,-15)
            //(0,0) == (-27,-15)
            //y increments by 50
            //x increment by 50
            //right angle 450,450 for robby
            //right angle 422,435 for ball
         
            
            // _spriteBatch.Draw(_ball,new Vector2(22, 85), Color.White);
            _spriteBatch.Draw(_robby,new Vector2(0,0), Color.White);
            _spriteBatch.DrawString(_moves,"moves",new Vector2(10,530), Color.White);
            _spriteBatch.DrawString(_points,"points",new Vector2(10,550), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {   
            var _tileSize = 50;
            var _numOfBalls = _tileSize/2;
            Random _rnd = new Random();
            double[] _ballX = {-27,23,73,123,173,223,273,323,373,422};
            double[] _ballY = {-15,35,85,135,185,235,285,335,385,435};
            double[] _randBallXs = new double[_numOfBalls];
            double[] _randBallYs = new double[_numOfBalls];
            for(int j = 0; j< _numOfBalls; j++){
                _randBallXs[j] = _ballX[_rnd.Next(0,_ballX.Length)];
                _randBallYs[j] = _ballY[_rnd.Next(0,_ballY.Length)];
            }
            base.Initialize();
        }
        protected override void LoadContent(){
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileTexture = _game.Content.Load<Texture2D>("tile");
            _generation = _game.Content.Load<SpriteFont>("generation");
            _moves = _game.Content.Load<SpriteFont>("moves");
            _points = _game.Content.Load<SpriteFont>("points");
            _ball = _game.Content.Load<Texture2D>("green_ball1");
            _robby = _game.Content.Load<Texture2D>("robot_icon");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}