// Если все тесты проходит, то консоль пустая.

public static class TestClass
{
    public static void TestKills3Count()
    {
        var doska = new Doska(0b00000000000000100000111111111111, 0, 0b11111111111100000011000000000000, 0);
        var testDoska = new TestDoska(doska);
        testDoska.TestWhiteKillsVariantsCount(new List<(int, int, int)>()
    {
    });
    }
    public static void TestKills3()
    {
        var doska = new Doska(0b00000000000000100000111111111111, 0, 0b11111111111100000011000000000000, 0);
        var testDoska = new TestDoska(doska);
        testDoska.TestWhiteKillsVariants(new List<(int, int, int)>()
    {
        (9, 16, 12), (9, 18, 13)
    });
    }
    public static void TestKills2Count()
    {
        var doska = new Doska(0b00000000000000000000111111111111, 0, 0b11111111111100000011000000000000, 0);
        var testDoska = new TestDoska(doska);
        testDoska.TestWhiteKillsVariantsCount(new List<(int, int, int)>()
    {
       
    });
    }
    public static void TestKills2()
    {
        var doska = new Doska(0b00000000000000000000111111111111, 0, 0b11111111111100000011000000000000, 0);
        var testDoska = new TestDoska(doska);
        testDoska.TestWhiteKillsVariants(new List<(int, int, int)>()
    {
        (8, 17, 12),
        (9, 16, 12),
        (9, 18, 13),
        (10, 17, 13)
    });
    }
    public static void TestKills1Count()
    {
        var doska = new Doska(0b0000000000000000000000000001111, 0, 0b11111111111100000000000011000000, 0);
        var testDoska = new TestDoska(doska);
        testDoska.TestWhiteKillsVariantsCount(new List<(int, int, int)>()
    {
    });
    }
    public static void TestKills1()
    {
        var doska = new Doska(0b0000000000000000000000000001111, 0, 0b11111111111100000000000011000000, 0);
        var testDoska = new TestDoska(doska);
        testDoska.TestWhiteKillsVariants(new List<(int, int, int)>()
    {
        (2, 11, 6), (3, 10, 6)
    });
    }
    public static void TestVariants1Count()
    {
        var doska = new Doska(0b00000000000000000000111111111111, 0, 0b11111111111100000000000000000000, 0);
        var testDoska = new TestDoska(doska);
        testDoska.TestWhiteVariantsCount(new List<(int, int)>()
    {
    });
    }
    public static void TestVariants1()
    {
        var doska = new Doska(0b00000000000000000000111111111111, 0, 0b11111111111100000000000000000000, 0);
        var testDoska = new TestDoska(doska);
        testDoska.TestWhiteVariants(new List<(int, int)>()
    {
        (8, 12), (9, 12), (9, 13), (10, 13), (10, 14), (11, 14), (11, 15)
    });
    }
}