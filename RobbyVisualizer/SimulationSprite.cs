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
        private SpriteFont _generationStr;
        private SpriteFont _pointsStr;
        private SpriteFont _movesStr;
        private int _genNum;
        private int _points;
        private int _moves;
        private int _numMoves;
        private int _robbyX;
        private int _robbyY;
        private int _count;
        private int _limit;
        private string[] _filePaths;
        private string _txt;
        private int _fileIndex;
        private int[] _possibleMoves;
        private RobbyTheRobot.IRobbyTheRobot _robbyObj; 
        private RobbyTheRobot.ContentsOfGrid[,] _grid;

        private Game _game;

        public SimulationSprite(Game game): base(game){
            _game = game;
            _robbyX = 10;
            _robbyY = 10;
            _fileIndex = 0;
            _points = 0;
            _moves = 0;
            _count = 0;
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
            var width = _grid.GetLength(0);
            var height = _grid.GetLength(1);
            for (var x = 0;  x < width; x++){
                for(var y = 0; y < height; y++){
                    if(_grid[x,y] == RobbyTheRobot.ContentsOfGrid.Can){
                        _spriteBatch.Draw(_can,new Vector2((x*50)+10, (y*50)+10), Color.White);
                    }
                }
            }
            string genStr = "Generation: " + _genNum;
            string movesStr = "Moves: " + _numMoves;
            string pointsStr = "Points: " + _points;
            _spriteBatch.DrawString(_generationStr,genStr,new Vector2(10,510), Color.White);
            _spriteBatch.DrawString(_movesStr,movesStr,new Vector2(10,530), Color.White);
            _spriteBatch.DrawString(_pointsStr,pointsStr,new Vector2(10,550), Color.White);
            _spriteBatch.Draw(_robby,new Vector2(_robbyX,_robbyY), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize(){

            _grid = _robbyObj.GenerateRandomTestGrid();
            readFiles();
            // _robbyObj= RobbyTheRobot.Robby.CreateRobbyTheRobot(1,1,1);
            
            base.Initialize();
        }
        protected override void LoadContent(){
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileTexture = _game.Content.Load<Texture2D>("tile");
            _generationStr = _game.Content.Load<SpriteFont>("generation");
            _movesStr = _game.Content.Load<SpriteFont>("moves");
            _pointsStr = _game.Content.Load<SpriteFont>("points");
            _can = _game.Content.Load<Texture2D>("can");
            _robby = _game.Content.Load<Texture2D>("robot");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
           
            if (_moves < _numMoves)
            {
                if (_count > _limit)
                {
                    _points += RobbyTheRobot.RobbyHelper.ScoreForAllele(_possibleMoves, _grid, ref _robbyX, ref _robbyY);

                    _count = 0;
                    _moves++;
                }
                else
                {
                    _count++;
                }
            }
            else
            {
                _robbyX = 10;
                _robbyY = 10;
                _moves = 0;
                _points = 0;

                _fileIndex++;
                if (_fileIndex < 4)
                {
                    readFiles();
                }
                else
                {
                    Game.Exit();
                }
            }
            
            base.Update(gameTime);
        }

        public void readFiles()
        {
                        
            using(var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _filePaths = Directory.GetFiles(fbd.SelectedPath);

                }
            }
            

            this._txt = File.ReadAllText(_filePaths[_fileIndex]);
            string[] txtArr = _txt.Split(',');

            this._genNum = Int32.Parse(txtArr[0]);
            this._numMoves = Int32.Parse(txtArr[1]);
            this._possibleMoves  = new int[txtArr.Length - 3];
            int geneCounter = 0;

            for (int i = 3; i < txtArr.Length; i++)
            {
                if (Int32.Parse(txtArr[i]) == 0)
                {
                    _possibleMoves[geneCounter] = 0;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 1)
                {
                    _possibleMoves[geneCounter] = 1;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 2)
                {
                    _possibleMoves[geneCounter] = 2;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 3)
                {
                    _possibleMoves[geneCounter] = 3;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 4)
                {
                    _possibleMoves[geneCounter] = 4;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 5)
                {
                    _possibleMoves[geneCounter] = 5;
                    geneCounter++;
                }
                else if (Int32.Parse(txtArr[i]) == 6)
                {
                    _possibleMoves[geneCounter] = 6;
                    geneCounter++;
                }
            }
        }
    }
}