using Reversi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.ConsoleApp;

public class ConsoleInteractor : IInteractor {
    Position currentPosition {
        get => this.gameState.CurrentPosition;
        set => this.gameState.CurrentPosition = value;
    }

    readonly GameState gameState;

    public string Name { get; }

    public event StateChangedEventHandler? StateChanged;

    public ConsoleInteractor(GameState gameState, string name) {
        this.gameState = gameState;
        this.Name = name;
    }

    public async Task<Position> InputAsync(Board board, Piece turn) {
        while (true) {
            var key = await Task.Run(() => Console.ReadKey());
            switch (key.Key) {
                case ConsoleKey.UpArrow: {
                        var newPos = currentPosition with { Y = currentPosition.Y - 1 };
                        if (board.HasCell(newPos)) {
                            currentPosition = newPos;
                        }
                        break;
                    }
                case ConsoleKey.DownArrow: {
                        var newPos = currentPosition with { Y = currentPosition.Y + 1 };
                        if (board.HasCell(newPos)) {
                            currentPosition = newPos;
                        }
                        break;
                    }
                case ConsoleKey.LeftArrow: {
                        var newPos = currentPosition with { X = currentPosition.X - 1 };
                        if (board.HasCell(newPos)) {
                            currentPosition = newPos;
                        }
                        break;
                    }
                case ConsoleKey.RightArrow: {
                        var newPos = currentPosition with { X = currentPosition.X + 1 };
                        if (board.HasCell(newPos)) {
                            currentPosition = newPos;
                        }
                        break;
                    }
                case ConsoleKey.Enter: {
                        return this.currentPosition;
                    }
            }
            this.StateChanged?.Invoke(board, turn);
        }
    }
}
