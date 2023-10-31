// x, sopernik, all
public interface IKill
{
    uint MaskaVstavki { get; }
    uint DeletedFigure { get; }
    bool IsAvailable => MaskaVstavki != 0;
}
