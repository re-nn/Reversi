using Reversi.ConsoleApp;
using Reversi.Core;

var gameState = new GameState();
var player1 = new ConsoleInteractor(gameState, "Player 1");
var player2 = new ConsoleInteractor(gameState, "Player 2");
var view = new ConsoleView(player1, player2);

var reversi = new ReversiGame(player1, player2);

player1.StateChanged += (board, turn) => {
    view.Draw(board, gameState.CurrentPosition, turn);
};
player2.StateChanged += (board, turn) => {
    view.Draw(board, gameState.CurrentPosition, turn);
};
reversi.StateChanged += (board, turn) => {
    view.Draw(board, gameState.CurrentPosition, turn);
};

await reversi.StartAsync();