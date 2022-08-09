using Reversi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.ConsoleApp;

public class ConsoleView {
    readonly IInteractor player1;
    readonly IInteractor player2;

    public ConsoleView(IInteractor player1, IInteractor player2) {
        this.player2 = player2;
        this.player1 = player1;
    }

    public void Draw(Board board, Position currentPosition, Piece turn) {
        Console.Clear();
        Console.WriteLine($"Position ({currentPosition.X}, {currentPosition.Y})");

        for (int y = 0; y < 8; y++) {
            for (int x = 0; x < 8; x++) {
                // カーソルの位置であれば
                if (currentPosition.X == x && currentPosition.Y == y) {
                    if (board.CanPlace(currentPosition, turn)) {
                        Console.Write(turn switch {
                            Piece.Black => "◆",
                            Piece.White => "◇",
                            _ => "?",
                        });
                    }
                    else {
                        Console.Write(board[x, y]?.Piece switch {
                            Piece.Black => "◆",
                            Piece.White => "◇",
                            Piece.Empty => "＋",
                            _ => "?"
                        });
                    }
                }
                else {
                    var p = board[x, y]?.Piece switch {
                        Piece.Black => "●",
                        Piece.White => "〇",
                        Piece.Empty => "・",
                        _ => "E"
                    };
                    Console.Write(p);
                }
            }
            Console.WriteLine();
        }

        var currentTurnDisplay = turn switch {
            Piece.Black => $"[●] {this.player1.Name}\n 〇  {this.player2.Name}",
            Piece.White => $" ●  {this.player1.Name}\n[〇] {this.player2.Name}",
            _ => "Error turn is invalid",
        };
        Console.WriteLine("\n" + currentTurnDisplay);
    }

}
