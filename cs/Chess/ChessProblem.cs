namespace Chess
{
    public class ChessProblem
    {
        // Определяет мат, шах или пат белым.
        public static ChessStatus CalculateChessStatus(string[] lines)
        {
            var initBoard = new BoardParser().ParseBoard(lines);

            var isCheck = IsCheckForWhite(initBoard);
            var hasMoves = false;
            foreach (var locFrom in initBoard.GetPieces(PieceColor.White))
            {
                foreach (var locTo in initBoard.GetPiece(locFrom).GetMoves(locFrom, initBoard))
                {
                    var checkedBoard = new BoardParser().ParseBoard(lines);

                    MakeStep(checkedBoard, checkedBoard.GetPiece(locFrom), locFrom, locTo);

                    if (!IsCheckForWhite(checkedBoard))
                        hasMoves = true;
                }
            }
            if (isCheck)
                if (hasMoves)
                    return ChessStatus.Check;
                else return ChessStatus.Mate;
            else if (hasMoves) return ChessStatus.Ok;
            else return ChessStatus.Stalemate;
        }

        private static void MakeStep(Board board, Piece piece, Location locFrom, Location locTo)
        {
            board.Set(locTo, piece); 
            board.Set(locFrom, null);
        }

        // check — это шах
        private static bool IsCheckForWhite(Board board)
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