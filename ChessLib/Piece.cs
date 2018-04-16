using System.Collections.Generic;

namespace ChessLib
{
	public abstract class Piece {
		
		public Position pos { get; set; }
		public int id { get; set; }
		
		public Piece(){}
		public abstract IEnumerable<Position> ValidMovesFor(Position pos);
	} 
}
