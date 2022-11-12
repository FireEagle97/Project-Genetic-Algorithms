using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace RobbyVisualizer{
    public class RobbySprite : DrawableGameComponent{
        
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture;
        private Texture2D _ball;
        private Texture2D _robby;
        private SpriteFont _generation;
        private SpriteFont _points;
        private SpriteFont _moves;


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
                    var tileSize = 50;
                    _spriteBatch.Draw(_tileTexture, new Rectangle(i *tileSize , j * tileSize, tileSize, tileSize), Color.White);
                    
                    
                }
            }
            _spriteBatch.End();
            _spriteBatch.Begin();
            
            _spriteBatch.DrawString(_generation,"Generation",new Vector2(10,510), Color.White);
            //(0,1) == (22,-15)
            //(0,0) == (-27,-15)
            //y increments by 50
            //x increment by 50
            _spriteBatch.Draw(_ball,new Vector2(22, 85), Color.White);
            _spriteBatch.Draw(_robby,new Vector2(50,0), Color.White);
            _spriteBatch.DrawString(_moves,"moves",new Vector2(10,530), Color.White);
            _spriteBatch.DrawString(_points,"points",new Vector2(10,550), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
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