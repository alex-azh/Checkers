public record BackwardDoubleMask(uint Mask) : IHod
{
    public IEnumerable<(uint hod, uint kill)> Variants()
    {
        var chisla = UintHelper.Indexes(Mask).ToArray();
        yield return (chisla[0], chisla[2]);
        yield return (chisla[1], chisla[3]);
    }
}
