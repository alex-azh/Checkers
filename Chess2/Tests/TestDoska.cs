using System.Numerics;

public record struct TestDoska(Doska D)
{
    public readonly IEnumerable<(int pos, int hod)> WhiteVariants()
    {
        foreach (var d in D.WhitePVariants())
        {
            int oldPos = BitOperations.Log2(D.Whites & ~d.Whites);
            int newPos = BitOperations.Log2(d.Whites & ~D.Whites);
            yield return (oldPos, newPos);
        }
    }
    public readonly IEnumerable<(int pos, int hod, int dead)> WhiteKillsVariants()
    {
        foreach (var d in D.WhitePKillVariants())
        {
            int oldPos = BitOperations.Log2(D.Whites & ~d.Whites);
            int newPos = BitOperations.Log2(d.Whites & ~D.Whites);
            int dead = BitOperations.Log2(D.Blacks & ~d.Blacks);
            yield return (oldPos, newPos, dead);
        }
    }

    public void TestWhiteVariants(IEnumerable<(int pos, int hod)> actual)
    {
        var values = actual.Except(WhiteVariants()).Any() ? actual.Except(WhiteVariants()) : WhiteVariants().Except(actual);
        foreach (var p in values)
        {
            Console.WriteLine(p);
        }
    }
    public void TestWhiteVariantsCount(IEnumerable<(int pos, int hod)> actual)
    {
        var values = actual.Except(WhiteVariants()).Any() ? actual.Except(WhiteVariants()) : WhiteVariants().Except(actual);
        _ = values.Count();
    }
    public void TestWhiteKillsVariantsCount(IEnumerable<(int, int, int)> actual)
    {
        var values = actual.Except(WhiteKillsVariants()).Any() ? actual.Except(WhiteKillsVariants()) : WhiteKillsVariants().Except(actual);
        _ = values.Count();
    }
    public void TestWhiteKillsVariants(IEnumerable<(int, int, int)> actual)
    {
        var values = actual.Except(WhiteKillsVariants()).Any() ? actual.Except(WhiteKillsVariants()) : WhiteKillsVariants().Except(actual);
        foreach (var p in values)
        {
            Console.WriteLine(p);
        }
    }
}
