using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Core;

public delegate void StateChangedEventHandler(Board board , Piece turn);

public class ReversiGame {
    public event StateChangedEventHandler? StateChanged;
    public event EventHandler? GameOver;

    public int GameCount { get; private set; } = 0;

    public Board Board { get; } = new();

    public IInteractor Player1 { get; }

    public IInteractor Player2 { get; }

    public ReversiGame(IInteractor player1, IInteractor player2) {
        this.Player1 = player1;
        this.Player2 = player2;
    }

    public async Task StartAsync() {
        this.Board.InitAndReadyCells();
        this.StateChanged?.Invoke(this.Board, Piece.Black);

        while (true) {
            while (true) {
                var position = await this.Player1.InputAsync(this.Board, Piece.Black);
                if (this.Board.Place(position, Piece.Black)) {
                    break;
                }
            }
            this.StateChanged?.Invoke(this.Board, Piece.Black);

            while (true) {
                var position = await this.Player2.InputAsync(this.Board, Piece.White);
                if (this.Board.Place(position, Piece.White)) {
                    break;
                }
            }
            this.StateChanged?.Invoke(this.Board, Piece.White);

            if (this.Board.IsGameOver()) {
                this.GameOver?.Invoke(this, EventArgs.Empty);
                break;
            }
        }
    }
}

