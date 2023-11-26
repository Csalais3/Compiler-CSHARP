namespace Compiler.CodeAnalysis
{
    enum SyntaxKind
    {
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        InvalidToken,
        EndOfFileToken, 
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression,
    }
}