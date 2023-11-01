namespace Chess2.Tests;

public class TestClassJulia
{
    public uint AllBlack = 0b11_11111_11111_00000_00000_00110_00000;
    public uint AllWhite = 0b00_00000_00000_00000_00011_11111_11111;
    public static void TestKills3Count()
    {
        var doska = new Doska(0b00000000000000100000111111111111, 0, 0b11111111111100000011000000000000, 0);
        var testDoska = new TestDoska(doska);
        _ = testDoska.TestKillsJulia(new List<(int, int, int)>()
        {
        }).Count();
    }
    public static void TestKills3()
    {
        var doska = new Doska(0b00000000000000100000111111111111, 0, 0b11111111111100000011000000000000, 0);
        var testDoska = new TestDoska(doska);
        foreach (var p in testDoska.TestKillsJulia(new List<(int, int, int)>()
        {
        (9, 16, 12), (9, 18, 13)
        }))
        {
            Console.WriteLine(p.ToString());
        }
    }
    public static void TestKills2Count()
    {
        var doska = new Doska(0b00000000000000000000111111111111, 0, 0b11111111111100000011000000000000, 0);
        var testDoska = new TestDoska(doska);
        _ = testDoska.TestKillsJulia(new List<(int, int, int)>()
        {

        }).Count();
    }
    public static void TestKills2()
    {
        var doska = new Doska(0b00000000000000000000111111111111, 0, 0b11111111111100000011000000000000, 0);
        var testDoska = new TestDoska(doska);
        foreach (var p in testDoska.TestKillsJulia(new List<(int, int, int)>()
    {
        (8, 17, 12),
        (9, 16, 12),
        (9, 18, 13),
        (10, 17, 13)
    }))
        {
            Console.WriteLine(p.ToString());
        }
    }
    public static void TestKills1Count()
    {
        var doska = new Doska(0b0000000000000000000000000001111, 0, 0b11111111111100000000000011000000, 0);
        var testDoska = new TestDoska(doska);
        _ = testDoska.TestKillsJulia(new List<(int, int, int)>()
        {
        }).Count();
    }
    public static void TestKills1()
    {
        var doska = new Doska(0b00_00000_00000_00000_00000_00000_01111, 0, 0b11111111111100000000000011000000, 0);
        var testDoska = new TestDoska(doska);
        foreach (var p in testDoska.TestKillsJulia(new List<(int, int, int)>()
    {
        (2, 11, 6), (3, 10, 6)
    }))
        {
            Console.WriteLine(p.ToString());
        }
    }
    public static void TestKills4()
    {
        var doska = new Doska(
            0b00_00000_00000_00100_00000_00000_00000, 
            0,
            0b00_00000_00001_00000_00000_00000_00000, 
            0b00_00000_00000_00000_00000_00000_00000);
        var testDoska = new TestDoska(doska);
        var actual = new List<(int, int, int)>()
        {
            (17, 24, 20)
        };
        foreach (var p in testDoska.TestKillsJulia(actual))
        {
            Console.WriteLine(p.ToString());
        }
    }
}
