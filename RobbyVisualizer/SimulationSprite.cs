using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;
using System.IO;
using System;

namespace RobbyVisualizer{
    public class SimulationSprite : DrawableGameComponent{
        
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture;
        private Texture2D _can;
        private Texture2D _robby;
        private SpriteFont _generation;
        private SpriteFont _points;
        private SpriteFont _moves;
        private RobbyTheRobot.IRobbyTheRobot _robbyObj; 
        private RobbyTheRobot.ContentsOfGrid[,] _tiles;
        private RobbyTheRobot.RobbyHelper _robbyMover;
        // private DialogResult _result;
        private string[] _files;
        private Game _game;

        public SimulationSprite(Game game): base(game){
            _game = game;     
           
        }

        public override void Draw(GameTime gameTime)
        {   _spriteBatch.Begin();
            var gridSize  =50;
    
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    _spriteBatch.Draw(_tileTexture, new Rectangle(i *gridSize , j * gridSize, gridSize, gridSize), Color.White); 
                }
            }
            _spriteBatch.End();
            
            
           _spriteBatch.Begin();
            var width = _tiles.GetLength(0);
            var height = _tiles.GetLength(1);
            for (var x = 0;  x < width; x++){
                for(var y = 0; y < height; y++){
                    if(_tiles[x,y] == RobbyTheRobot.ContentsOfGrid.Can){
                        _spriteBatch.Draw(_can,new Vector2((x*50)+10, (y*50)+10), Color.White);
                    }
                }
            }
            _spriteBatch.DrawString(_generation,"Generation",new Vector2(10,510), Color.White);
            _spriteBatch.Draw(_robby,new Vector2(10,10), Color.White);
            _spriteBatch.DrawString(_moves,"moves",new Vector2(10,530), Color.White);
            _spriteBatch.DrawString(_points,"points",new Vector2(10,550), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize(){
            _robbyObj= RobbyTheRobot.Robby.CreateRobbyTheRobot(1,1,1);
            _tiles = _robbyObj.GenerateRandomTestGrid();
            
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                _files = Directory.GetFiles(fbd.SelectedPath);
            }
            // using(var fbd = new FolderBrowserDialog())
            // {
            //     DialogResult result = fbd.ShowDialog();

            //     if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            //     {
            //         string[] files = Directory.GetFiles(fbd.SelectedPath);

            //         System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
            //     }
            // }
            

            base.Initialize();
        }
        protected override void LoadContent(){
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileTexture = _game.Content.Load<Texture2D>("tile");
            _generation = _game.Content.Load<SpriteFont>("generation");
            _moves = _game.Content.Load<SpriteFont>("moves");
            _points = _game.Content.Load<SpriteFont>("points");
            _can = _game.Content.Load<Texture2D>("can");
            _robby = _game.Content.Load<Texture2D>("robot");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            for(var i =0; i< _files.Length; i++){
                string[] lines = File.ReadAllLines(_files[i]);
                foreach (string r in lines){
                    Console.WriteLine("hello" + r);
                }
            }

            
            base.Update(gameTime);
        }
    }
}