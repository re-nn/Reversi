namespace Reversi.Core;

public enum Piece {
    Empty,
    Black,
    White,
}

public struct Cell {
    public Piece Piece { get; }

    public Cell(Piece color) {
        this.Piece = color;
    }

    public Cell() {
        this.Piece = Piece.Empty;
    }

    public Cell Toggle() => new(this.Piece switch {
        Piece.White => Piece.Black,
        Piece.Black => Piece.White,
        _ => Piece.Empty,
    });
}

public class Board {
    const int Length = 8;

    Cell[] cells = new Cell[Length * Length];

    public Cell? this[int x, int y] {
        get => (x, y) switch {
            ( >= 0 and < Length, >= 0 and < Length) => this.cells[y * Length + x],
            _ => null,
        };
        set {
            if ((x, y) is ( >= 0 and < Length, >= 0 and < Length)) {
                this.cells[y * Length + x] = value ?? default;
            }
        }
    }

    public Cell? this[Position position] {
        get => this[position.X, position.Y];
        set {
            this[position.X, position.Y] = value;
        }
    }

    public Board() {

    }

    public void ClearCells() {
        this.cells = new Cell[Length * Length];
    }

    public void InitAndReadyCells() {
        int center = Length / 2;
        this[center - 1, center - 1] = new(Piece.White);
        this[center - 1, center] = new(Piece.Black);
        this[center, center - 1] = new(Piece.Black);
        this[center, center] = new(Piece.White);
    }

    public bool HasCell(Position position) => this[position] is not null;

    public bool IsEmptyCell(Position position) => this[position] is { Piece: Piece.Empty };


    public bool CanPlace(Position position, Piece turn) {
        var toggledTurn = new Cell(turn).Toggle().Piece;

        bool Place() {
            foreach (var d in Direction.Directions) {
                var nextPos = position.Add(d);
                if (
                    this[position] is { Piece: not Piece.Empty }
                    || this[nextPos] is not Cell next
                ) {
                    continue;
                }

                if (next.Piece == toggledTurn) {
                    nextPos = nextPos.Add(d);
                    while (this[nextPos] is Cell c) {
                        if (c.Piece == turn) {
                            return true;
                        }
                        nextPos = nextPos.Add(d);
                    }
                }
            }

            return false;
        }

        return this[position] switch {
            ({ Piece: Piece.Empty }) => Place(),
            _ => false,
        };
    }

    public bool Place(Position position, Piece turn) {
        bool Place() {
            var toggledTurn = new Cell(turn).Toggle().Piece;
            foreach (var d in Direction.Directions) {
                var nextPos = position.Add(d);
                if (
                    this[position] is { Piece: not Piece.Empty }
                    || this[nextPos] is not Cell next
                ) {
                    continue;
                }

                if (next.Piece == toggledTurn) {
                    nextPos = nextPos.Add(d);
                    while (this[nextPos] is Cell c) {
                        if (c.Piece == turn) {
                            this[position] = new(turn);

                            // ひっくり返す
                            nextPos = nextPos.Subtract(d);
                            while (this[nextPos] is Cell b && b.Piece != turn) {
                                this[nextPos] = new Cell(turn);
                                nextPos = nextPos.Subtract(d);
                            }

                            return true;
                        }

                        nextPos = nextPos.Add(d);
                    }
                }
            }

            return false;
        }

        return this[position] switch {
            ({ Piece: Piece.Empty }) => Place(),
            _ => false,
        };
    }

    public bool IsGameOver() {
        return false;
    }
}