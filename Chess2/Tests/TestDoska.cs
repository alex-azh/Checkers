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
    public readonly IEnumerable<(int pos, int hod, int dead)> WhiteKillsJulia()
    {
        foreach (var d in D.WhitePKills())
        {
            int oldPos = BitOperations.Log2(D.Whites & ~d.Whites);
            int newPos = BitOperations.Log2(d.Whites & ~D.Whites);
            int dead = BitOperations.Log2(D.Blacks & ~d.Blacks);
            yield return (oldPos, newPos, dead);
        }
    }

    public IEnumerable<(int pos, int hod)> TestWhiteVariants(IEnumerable<(int pos, int hod)> actual)
        => actual.Except(WhiteVariants()).Any() ? actual.Except(WhiteVariants()) : WhiteVariants().Except(actual);

    public IEnumerable<(int, int, int)> TestWhiteKillsVariants(IEnumerable<(int, int, int)> actual)
        => actual.Except(WhiteKillsVariants()).Any() ? actual.Except(WhiteKillsVariants()) : WhiteKillsVariants().Except(actual);
    public IEnumerable<(int pos, int hod, int dead)> TestKillsJulia(IEnumerable<(int, int, int)> actual)
       => actual.Except(WhiteKillsJulia()).Any() ? actual.Except(WhiteKillsJulia()) : WhiteKillsJulia().Except(actual);
}
