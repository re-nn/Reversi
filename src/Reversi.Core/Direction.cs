using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Core;

public class Direction {
    public static readonly Direction Up = new(0, -1);
    public static readonly Direction Down = new(0, 1);
    public static readonly Direction Left = new(-1, 0);
    public static readonly Direction Right = new(1, 0);
    public static readonly Direction UpLeft = new(-1, -1);
    public static readonly Direction UpRight = new(1, -1);
    public static readonly Direction DownLeft = new(-1, 1);
    public static readonly Direction DownRight = new(1, 1);
    public static readonly Direction[] Directions = new[] {
        Up,
        Left,
        Right,
        Down,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
    };

    public int X { get; }

    public int Y { get; }

    public Direction(int x, int y) {
        this.X = x;
        this.Y = y;
    }

    public Position Add(Position position) {
        return position with {
            X = position.X + this.X,
            Y = position.Y + this.Y,
        };
    }
}
