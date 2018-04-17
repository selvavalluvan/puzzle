using System;
using System.Collections.Generic;
using System.Linq;
using ChessLib;

namespace SampleProgram
{

    public class ComplexGame
    {
		public Board board;

        public void Play(int moves)
        {
			board.PrettyPrint();
            for(var move = 1; move <= moves; move++)
            {
                Piece piece = board.PickRandomPiece();
				Console.WriteLine("Cur position of {0}({1})\t= {2}", piece.ToString(), piece.id, piece.pos);
                board.Move(piece);
                Console.WriteLine("New position of {0}({1})\t= {2}", piece.ToString(), piece.id, piece.pos);
            }
			board.PrettyPrint();
        }


        public void Setup()
        {
			board = new Board();
			board.Add("knight", new Position(1, 3));
			board.Add("knight", new Position(1, 7));
			board.Add("queen", new Position(2, 2));
			board.Add("queen", new Position(3, 3));
			board.Add("queen", new Position(4, 4));
			board.Add("queen", new Position(5, 5));
			board.Add("queen", new Position(6, 6));
			board.Add("queen", new Position(7, 7));
			board.Add("queen", new Position(8, 8));
			board.Add("bishop", new Position(1, 1));
        }
    }
	
	
	/*
	* Piece Class. This class is the base class for all different pieces.
	*
	*/
	public abstract class Piece 
	{
		public Position pos { get; set; }
		public int id { get; set; }
		
		public Piece(){}
		public abstract IEnumerable<Position> ValidMovesFor(Position pos);
		public override string ToString()
		{
			return this.GetType().Name;
		}
	}
	
	public class Knight : Piece 
	{
		private KnightMove _knight;
		
		public Knight()
		{
			_knight = new KnightMove();
		}
		
        public override IEnumerable<Position> ValidMovesFor(Position pos) {
        	return _knight.ValidMovesFor(pos);
        }
	}
	
    public class Queen : Piece
    {
        public override IEnumerable<Position> ValidMovesFor(Position pos)
        {
            for(int i=1;i<=8;i++)
            {
                for (int j=1;j<=8;j++)
                {
                    if (pos.X == i && pos.Y == j)
                        continue;
                    var newX = i;
                    var newY = j;
                    yield return new Position(newX, newY);
                }
            }
        }
    }
	
    public class Bishop : Piece
    {
        public static readonly int[,] Directions = new[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

        public override IEnumerable<Position> ValidMovesFor(Position pos)
        {
            for (int i = 0; i <= Directions.GetUpperBound(0); i++)
            {
                int x1 = pos.X;
                int y1 = pos.Y;
                while (true) {
                    int newX = x1 + Directions[i, 0];
                    int newY = y1 + Directions[i, 1];

                    if (newX > 8 || newX < 1 || newY > 8 || newY < 1) {
                        break;
                    } else {
                        x1 = newX;
                        y1 = newY;
                        yield return new Position(newX, newY);
                    }
                }
            }
        }
    }
	
    static class PieceFactory
    {
        public static Piece Get(string piece)
        {
            switch (piece)
            {
                case "knight":
                    return new Knight();
                case "queen":
                    return new Queen();
                case "bishop":
                    return new Bishop();
				default:
					throw new System.ArgumentException("Invalid piece");
            }
        }
    }
	
	/* 
	* Board Class. This is the class for Board, that contains boundries and pieces are in be board
	*/
	public class Board
	{
		private readonly Random _rnd = new Random();
		
		// X axis Boundary
		private IEnumerable<int> _xBound = Enumerable.Range(1,8);
		// Y axis Boundary
		private IEnumerable<int> _yBound = Enumerable.Range(1,8);
		
		//Dictionary to store the pieces and its position.
		public IDictionary<Position, Piece> Pieces = new Dictionary<Position, Piece>();
		
		// Method to check the postion is in the bounds of the board.
        private bool _isPositionValid(Position pos)
        {
            return _xBound.Contains(pos.X) && _yBound.Contains(pos.Y);
        }
		
		// Method to check if the postion is occupied by any other piece.
        private bool _isPositionOccupied(Position pos)
        {
            return this.Pieces.ContainsKey(pos);
        }
		
		// Method to Add a piece to the board after validation.
		public void Add(string pieceName, Position pos)
		{
			if(_isPositionValid(pos) && !_isPositionOccupied(pos)) {
				var piece = PieceFactory.Get(pieceName);
				piece.pos = pos;
				piece.id = this.Pieces.Count+1;
				Console.WriteLine("Add piece {0}({1})\tin position {2}", piece.ToString(), piece.id, piece.pos);
				this.Pieces.Add(pos, piece);
			} else {
				throw new System.ArgumentException("Invalid position");
			}
		}
		
		// Method to Pick a piece randomly from the board.
        public Piece PickRandomPiece()
        {
            var randomKey = this.Pieces.Keys.ToArray()[_rnd.Next(this.Pieces.Count)];
            return this.Pieces[randomKey];
        }
		
		// Method to move the piece to a valid location.
		public void Move(Piece piece)
		{
			var possibleMoves = piece.ValidMovesFor(piece.pos).ToList();
			var index = _rnd.Next(possibleMoves.Count);
            Position pos = possibleMoves[index];
			while(_isPositionOccupied(pos)){
				possibleMoves.RemoveAt(index);
				if(!possibleMoves.Any()){
					Console.WriteLine("No more possible moves for {0}({1}), so leaving it in the same place.", piece.ToString(), piece.id);
					pos = piece.pos;
					break;
				}
				index = _rnd.Next(possibleMoves.Count);
				pos = possibleMoves[index];
			}
			this.Pieces.Remove(piece.pos);
			piece.pos = pos;
			this.Pieces.Add(pos, piece);
		}
		
		// Method to print the board in matrix.
		public void PrettyPrint()
		{
			Console.WriteLine("\t  1  \t  2 \t  3 \t  4 \t  5 \t  6 \t  7 \t  8 ");
            for(int i=1;i<=8;i++)
            {
				Console.Write("{0} ", i);
                for (int j=1;j<=8;j++)
                {
					Position pos = new Position(i, j);
					if (Pieces.TryGetValue(pos, out Piece piece)) {
						Console.Write("\t[{0}({1})]", piece.ToString()[0], piece.id);
					} else {
						Console.Write("\t[____]");
					}
                }
				Console.WriteLine("");
            }
		}
	}
}
