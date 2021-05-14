namespace Chess
{
    public class ChessProblem
    {
        private static Board board;

        public static void LoadFrom(string[] lines)
        {
            board = new BoardParser().ParseBoard(lines);
        }

        // Определяет мат, шах или пат белым.
        public static ChessStatus CalculateChessStatus()
        {
            var isCheck = IsCheckForWhite();
            var hasMoves = false;
            foreach (var locFrom in board.GetPieces(PieceColor.White))
            {
                foreach (var locTo in board.GetPiece(locFrom).GetMoves(locFrom, board))
                {
                    var old = board.GetPiece(locTo);
                    MakeStep(board.GetPiece(locFrom), locFrom, locTo);
                    if (!IsCheckForWhite())
                        hasMoves = true;
                    board.Set(locFrom, board.GetPiece(locTo));
                    board.Set(locTo, old);
                }
            }
            if (isCheck)
                if (hasMoves)
                    return ChessStatus.Check;
                else return ChessStatus.Mate;
            else if (hasMoves) return ChessStatus.Ok;
            else return ChessStatus.Stalemate;
        }

        private static void MakeStep(Piece piece, Location locFrom, Location locTo)
        {
            board.Set(locTo, piece); 
            board.Set(locFrom, null);
        }

        // check — это шах
        private static bool IsCheckForWhite()
        {
            var isCheck = false;
            foreach (var loc in board.GetPieces(PieceColor.Black))
            {
                var piece = board.GetPiece(loc);
                var moves = piece.GetMoves(loc, board);
                foreach (var destination in moves)
                {
                    if (Piece.Is(board.GetPiece(destination),
                                 PieceColor.White, PieceType.King))
                        isCheck = true;
                }
            }
            if (isCheck) return true;
            return false;
        }
    }
}