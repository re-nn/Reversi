using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Core;

public interface IInteractor {
    string Name { get; }

    Task<Position> InputAsync(Board board, Piece tuen);
}
