using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Core;

public record struct Position {
    public int X { get; set; }

    public int Y { get; set; }

    public Position(int x, int y) {
        this.X = x;
        this.Y = y;
    }

    public Position Add(Direction direction) {
        return this with {
            X = this.X + direction.X,
            Y = this.Y + direction.Y,
        };
    }

    public Position Subtract(Direction direction) {
        return this with {
            X = this.X - direction.X,
            Y = this.Y - direction.Y,
        };
    }
}