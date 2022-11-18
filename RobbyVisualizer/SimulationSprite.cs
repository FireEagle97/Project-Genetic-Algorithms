using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
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
        private int _genNum;
        private int _numMoves;
        private int _robbyX;
        private int _robbyY;
        private string[] _filePaths;
        private string _txt;
        private int _fileIndex;
        private RobbyTheRobot.IRobbyTheRobot _robbyObj; 
        private RobbyTheRobot.ContentsOfGrid[,] _grid;
        // private RobbyTheRobot.RobbyHelper _robbyMover;
        // private DialogResult _result;
        // private string[] _files;
        private Game _game;

        public SimulationSprite(Game game): base(game){
            _game = game;     
           
        }

        public override void Draw(GameTime gameTime)
        {   _spriteBatch.Begin();
    
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    _spriteBatch.Draw(_tileTexture, new Rectangle(i *_robbyObj.GridSize/2 , j * _robbyObj.GridSize/2, _robbyObj.GridSize/2, _robbyObj.GridSize/2), Color.White); 
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
            int[] moves = {0,0,0,3,3};
            for(var i = 0; i < moves.Length; i++){
                // if(moves[i] == 0 && _robbyX >=10 && _robbyY >=10){
                //     _robbyY -= 50;
                // }
                if(moves[i] == 1 && _robbyX <=10 && _robbyY <=10){
                    _robbyY += 50;
                }
                // if(moves[i] == 2 && _robbyX >=10 && _robbyY >=10){
                //     _robbyX -= 50;
                // }
                if(moves[i] == 3 && _robbyX <=10 && _robbyY <=10){
                    _robbyX +=50;
                }
                

            }
            _spriteBatch.Draw(_robby,new Vector2(_robbyX,_robbyY), Color.White);
            // _spriteBatch.Draw(_robby,new Vector2(_robbyX,_robbyY), Color.White);
            _spriteBatch.DrawString(_moves,"moves",new Vector2(10,530), Color.White);
            _spriteBatch.DrawString(_points,"points",new Vector2(10,550), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize(){

            // readFiles();
            _robbyObj= RobbyTheRobot.Robby.CreateRobbyTheRobot(1,1,1);
            _robbyX = 10;
            _robbyY = 10;
            using(var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _filePaths = Directory.GetFiles(fbd.SelectedPath);

                }
            }
            
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
            //0 = North,
            // 1 = South,
            // 2 = East,
            // 3 = West,
            // 4 = Nothing,
            // 5 =
            // PickUp
            // 6 = Random
            // int[] moves = {6,1,6,6,2,4,4,3,4,1,4,0,2,0,1,6,0,3};
            // for(var i = 0; i < moves.Length; i++){
            //     // if(moves[i] == 0 && _robbyX >=10 && _robbyY >=10){
            //     //     _robbyY -= 50;
            //     // }
            //     if(moves[i] == 1 && _robbyX >=10 && _robbyY >=10){
            //         _robbyY += 50;
            //     }
            //     // if(moves[i] == 2 && _robbyX >=10 && _robbyY >=10){
            //     //     _robbyX -= 50;
            //     // }
            //     if(moves[i] == 3 && _robbyX >=10 && _robbyY >=10){
            //         _robbyX +=50;
            //     }

            // }
            
            // for(var i =0; i< _files.Length; i++){
            //     string[] lines = File.ReadAllLines(_files[i]);
            //     foreach (string r in lines){
            //         Console.WriteLine("hello" + r);
            //     }
            // }

            
            base.Update(gameTime);
        }

        public void readFiles()
        {
            _grid = _robbyObj.GenerateRandomTestGrid();

            this._txt = File.ReadAllText(_filePaths[_fileIndex]);
            string[] txtArr = _txt.Split(',');

            this._genNum = Int32.Parse(txtArr[0]);
            this._numMoves = Int32.Parse(txtArr[1]);
            RobbyTheRobot.PossibleMoves[] possibleMoves = new RobbyTheRobot.PossibleMoves[txtArr.Length - 3];
            int geneCounter = 0;

            for (int i = 3; i < txtArr.Length; i++)
            {
                if (Int32.Parse(txtArr[i]) == 0)
                {
                    possibleMoves[geneCounter] = (RobbyTheRobot.PossibleMoves)0;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 1)
                {
                    possibleMoves[geneCounter] = (RobbyTheRobot.PossibleMoves)1;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 2)
                {
                    possibleMoves[geneCounter] = (RobbyTheRobot.PossibleMoves)2;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 3)
                {
                    possibleMoves[geneCounter] = (RobbyTheRobot.PossibleMoves)3;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 4)
                {
                    possibleMoves[geneCounter] = (RobbyTheRobot.PossibleMoves)(4);
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 5)
                {
                    possibleMoves[geneCounter] = (RobbyTheRobot.PossibleMoves)(5);
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 6)
                {
                    possibleMoves[geneCounter] = (RobbyTheRobot.PossibleMoves)(6);
                    geneCounter++;
                }
            }

             this.chromo = new Chromosome(possibleMoves);
        }
    }
}