using System.Collections.Generic;

namespace ChessLib
{
    public class QueenMove : Piece
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
}