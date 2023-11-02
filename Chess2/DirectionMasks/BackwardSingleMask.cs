public record BackwardSingleMask(uint Mask) : IHod
{
    public IEnumerable<(uint hod, uint kill)> Variants()
    {
        var chisla = UintHelper.Indexes(Mask).ToArray();
        yield return (chisla[0], chisla[1]);
    }
}
