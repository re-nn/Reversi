using Reversi.Core;

namespace Reversi.Blazor.Services;

public class PlayerInteractor : IInteractor {
    public string Name { get; set; } = "Play";

    public Piece Turn { get; private set; }

    TaskCompletionSource<Position>? taskSource;

    public Task<Position> InputAsync(Board board, Piece turn) {
        var taskSource = new TaskCompletionSource<Position>();
        this.taskSource = taskSource;
        this.Turn = turn;
        return taskSource.Task;
    }

    public void SetInput(Position position) {
        if (this.taskSource is null) {
            throw new Exception("あなたの番じゃありません" + this.Name);
        }
        var taskSource = this.taskSource;
        this.taskSource = null;
        taskSource.SetResult(position);
    }
}
