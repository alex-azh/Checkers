﻿// x, sopernik, all
public static class DirectionFuncs
{
    public static readonly Func<uint, uint, uint, (uint step, IKill kill)> Right = (x, sopernik, all) =>
    {
        var offset = x << 4;
        return (offset & ~all, new RightKill(((offset << 4 & sopernik) << 5) & ~all));
    };

    public static readonly Func<uint, uint, uint, (uint step, LeftKill kill)> Left = (x, sopernik, all) =>
    {
        var offset = x << 3;
        return (offset & ~all, new(((offset & sopernik) << 4) & ~all));
    };
    // TODO: написать BackLeft BackRight методы для ходьбы (дамка ходит на 1 клетку)
}
