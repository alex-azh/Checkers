public interface IHod
{
    IEnumerable<(uint hod, uint kill)> Variants();
}
