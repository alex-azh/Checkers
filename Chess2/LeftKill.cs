public record struct LeftKill(uint MaskaVstavki) : IKill
{
    public uint DeletedFigure => MaskaVstavki >> 4;
}
