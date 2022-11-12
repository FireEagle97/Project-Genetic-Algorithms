using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace RobbyVisualizer{
    public class RobbySprite : DrawableGameComponent{
        
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture;
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
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent(){
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileTexture = _game.Content.Load<Texture2D>("tile");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}