public record struct RightKill(uint IndexHoda) : IKill
{
    public uint DeletedFigure => IndexHoda >> 5;
}
