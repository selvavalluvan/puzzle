using System;
using System.Collections.Generic;
using System.Linq;
using ChessLib;

namespace SampleProgram
{

    public class ComplexGame
    {

        private Position _startPosition;
		public Board board;

        public void Play(int moves)
        {
            for(var move = 1; move <= moves; move++)
            {
                Piece piece = board.PickRandomPiece();
				Console.WriteLine("Piece to move = {0}:{1} ", piece.id, piece);
                board.Move(piece);
                Console.WriteLine("New position of {0}:{1} = {2} ", piece.id, piece, piece.pos);
            }
        }


        public void Setup()
        {
			board = new Board();
			board.Add("knight", new Position(1, 3));
			board.Add("knight", new Position(1, 7));
			board.Add("queen", new Position(7, 7));
			board.Add("queen", new Position(2, 2));
			board.Add("bishop", new Position(3, 3));
			board.Add("bishop", new Position(1, 1));
        }
    }
	
    static class PieceFactory
    {
        public static Piece Get(string piece)
        {
            switch (piece)
            {
                case "knight":
                    return new KnightMove();
                case "queen":
                    return new QueenMove();
                case "bishop":
                    return new BishopMove();
				default:
					return new QueenMove();
					
            }
        }
    }
	
	// This is the class for Board.
	public class Board
	{
		private readonly Random _rnd = new Random();
		
		private IEnumerable<int> _xBound = Enumerable.Range(1,8);
		private IEnumerable<int> _yBound = Enumerable.Range(1,8);
		
		public IDictionary<Position, Piece> Pieces = new Dictionary<Position, Piece>();
		
        private bool _isPositionValid(Position pos)
        {
            return _xBound.Contains(pos.X) && _yBound.Contains(pos.Y);
        }
		
        private bool _isPositionOccupied(Position pos)
        {
            return this.Pieces.ContainsKey(pos);
        }
		
		public void Add(string pieceName, Position pos)
		{
			if(_isPositionValid(pos) && !_isPositionOccupied(pos)) {
				var piece = PieceFactory.Get(pieceName);
				piece.pos = pos;
				piece.id = this.Pieces.Count+1;
				Console.WriteLine("{0}:{1}:{2}", piece.id, piece.ToString(), piece.pos);
				this.Pieces.Add(pos, piece);
			} else {
				throw new System.ArgumentException("Invalid position");
			}
		}
		
		public void Move(Piece piece)
		{
			var possibleMoves = piece.ValidMovesFor(piece.pos).ToList();
			var index = _rnd.Next(possibleMoves.Count);
            Position pos = possibleMoves[index];
			while(_isPositionOccupied(pos)){
				possibleMoves.RemoveAt(index);
				index = _rnd.Next(possibleMoves.Count);
				pos = possibleMoves[index];
			}
			this.Pieces.Remove(piece.pos);
			piece.pos = pos;
			this.Pieces.Add(pos, piece);
		}
		
        public Piece PickRandomPiece()
        {
            var randomKey = this.Pieces.Keys.ToArray()[_rnd.Next(this.Pieces.Count)];
            return this.Pieces[randomKey];
        }
		
	}

}
