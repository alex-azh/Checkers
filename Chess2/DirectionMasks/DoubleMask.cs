public record DoubleMask(uint Mask) : IHod
{
    public IEnumerable<(uint hod, uint kill)> Variants()
    {
        var chisla = Doska.Indexes(Mask).ToArray();
        yield return (chisla[2], chisla[0]);
        yield return (chisla[3], chisla[1]);
    }
}
