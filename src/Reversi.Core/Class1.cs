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

    public Cell Toggle() {
        return new(this.Piece switch {
            Piece.White => Piece.Black,
            Piece.Black => Piece.White,
            _ => Piece.Empty,
        });
    }
}

public class GameField {
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

    public GameField() {

    }

    public void ClearCells() {
        this.cells = new Cell[Length * Length];
    }

    public void InitAndReadyCells() {
        int center = Length / 2;
        this[center - 1, center - 1] = new(Piece.Black);
        this[center - 1, center] = new(Piece.White);
        this[center, center - 1] = new(Piece.Black);
        this[center, center] = new(Piece.White);
    }
}