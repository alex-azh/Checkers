public record struct RightKill(uint MaskaVstavki) : IKill
{
    public uint DeletedFigure => MaskaVstavki >> 5;
}
