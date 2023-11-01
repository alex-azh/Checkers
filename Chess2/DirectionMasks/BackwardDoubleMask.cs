public record BackwardDoubleMask(uint Mask) : IHod
{
    public IEnumerable<(uint hod, uint kill)> Variants()
    {
        var chisla = Doska.Indexes(Mask).ToArray();
        yield return (chisla[0], chisla[3]);
        yield return (chisla[1], chisla[4]);
    }
}
