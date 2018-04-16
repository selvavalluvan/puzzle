using System;
using System.Collections.Generic;

namespace ChessLib
{
    public class BishopMove
    {
        public static readonly int[,] Directions = new[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

        public IEnumerable<Position> ValidMovesFor(Position pos)
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
}