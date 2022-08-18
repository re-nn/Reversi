using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Core;

public delegate void StateChangedEventHandler(Board board, Piece turn);

public class ReversiGame {
    public event StateChangedEventHandler? StateChanged;
    public event EventHandler? GameOver;

    public int GameCount { get; private set; } = 0;

    public Board Board { get; } = new();

    public IInteractor Player1 { get; }

    public IInteractor Player2 { get; }

    public Piece Turn { get; private set; }

    public bool IsGameOvered { get; private set; }

    private IInteractor CurrentPlayer => this.Turn switch {
        Piece.Black => this.Player1,
        Piece.White => this.Player2,
        _ => throw new Exception(),
    };

    public ReversiGame(IInteractor player1, IInteractor player2) {
        this.Player1 = player1;
        this.Player2 = player2;
        this.Reset();
    }

    public void Reset() {
        this.Board.InitAndReadyCells();
        this.IsGameOvered = false;
        this.Turn = Piece.Black;
        this.StateChanged?.Invoke(this.Board, this.Turn);
    }

    public async Task RestartAsync() {
        this.Reset();
        await this.StartAsync();
    }

    public async Task StartAsync() {
        this.StateChanged?.Invoke(this.Board,this.Turn);
        int passedCount = 0;

        while (true) {
            while (true) {
                if (this.Board.CanPlaceAnywhere(this.Turn) is false) {
                    passedCount++;
                    break;
                }
                else {
                    passedCount = 0;
                }

                var position = await this.CurrentPlayer.InputAsync(this.Board, this.Turn);
                if (this.Board.Place(position, this.Turn)) {
                    break;
                }
            }

            try {
                if (passedCount >= 2) {
                    this.IsGameOvered = true;
                    this.GameOver?.Invoke(this, EventArgs.Empty);
                    break;
                }
                this.ToggleTurn();
            }
            finally {
                this.StateChanged?.Invoke(this.Board, Piece.Black);
            }
        }
    }

    void ToggleTurn() {
        this.Turn = new Cell(this.Turn).Toggle().Piece;
    }
}

