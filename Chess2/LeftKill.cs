public record struct LeftKill(uint IndexHoda) : IKill
{
    public uint DeletedFigure => IndexHoda >> 4;
}
