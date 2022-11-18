using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace RobbyVisualizer
{
    public class SimulationSprite : DrawableGameComponent{
        
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture;
        private Texture2D _can;
        private Texture2D _robby;
        private SpriteFont _generationStr;
        private SpriteFont _pointsStr;
        private SpriteFont _movesStr;
        private double _genNum;
        private double _points;
        private int _numMoves;
        private int _moves;
        private int _gridXRobby;
        private int _gridYRobby;
        private int _robbyX;
        private int _robbyY;
        private int _count;
        private int _limit;
        private string[] _filePaths;
        private int[] _possibleMoves;
        private string _txt;
        private int _fileIndex;
        private Random _rnd;
        private RobbyTheRobot.IRobbyTheRobot _robbyObj; 
        private RobbyTheRobot.ContentsOfGrid[,] _grid;

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
            string movesStr = "Moves: " + _moves + "/"+_numMoves;
            string pointsStr = "Points: " + _points;
            _spriteBatch.DrawString(_generationStr,genStr,new Vector2(10,510), Color.White);
            _spriteBatch.DrawString(_movesStr,movesStr,new Vector2(10,530), Color.White);
            _spriteBatch.DrawString(_pointsStr,pointsStr,new Vector2(10,550), Color.White);
            _spriteBatch.Draw(_robby,new Vector2(_robbyX,_robbyY), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Initialize(){
            _rnd = new Random();
            _robbyX = 10;
            _robbyY = 10;
            _fileIndex = 0;
            _points = 0;
            _genNum = 0;
            _numMoves = 200;
            _moves = 0;
            _count = 0;
            _limit = 4;
            _gridXRobby = _rnd.Next(0,9);
            _gridYRobby = _rnd.Next(0,9);
            _gridYRobby = 0;
            _robbyObj= RobbyTheRobot.Robby.CreateRobbyTheRobot(1,1,1);
                                  
            using(var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    _filePaths = Directory.GetFiles(fbd.SelectedPath);

                }
            }
            
            readFiles();
            
            
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
                    _points += RobbyTheRobot.RobbyHelper.ScoreForAllele(_possibleMoves, _grid,_rnd, ref _gridXRobby, ref _gridYRobby);
                    _count = 0;
                    _robbyX = (_gridXRobby)*50;
                    _robbyY = (_gridYRobby)*50;
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
                _gridXRobby = _rnd.Next(0,9);
                _gridYRobby = _rnd.Next(0,9);
                _moves = 0;
                _points = 0;

                _fileIndex++;
                if (_fileIndex < 6)
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
            _grid = _robbyObj.GenerateRandomTestGrid();      
            this._txt = File.ReadAllText(_filePaths[_fileIndex]);
            string[] txtArr = _txt.Split(',');
            int[] moves = new int[243];
            //Getting the generation number;
            string pattern = @"Candidate(\d*)\.";
            Match match = Regex.Match(_filePaths[_fileIndex], pattern);
            string value = match.Groups[1].Value;
            _genNum = Int32.Parse(value);
            for(var i = 0; i < moves.Length; i++){
                moves[i] = (int)Char.GetNumericValue(txtArr[2][i]);

            }
            this._numMoves = Int32.Parse(txtArr[1]);
            this._possibleMoves  = moves;

        }
    }
}