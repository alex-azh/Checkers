﻿namespace Chess2;

public static class Masks
{
    public readonly static Dictionary<uint, uint> Steps = new()
{
    { 0b00_00000_00000_00000_00000_00000_00001, 0b00_00000_00000_00000_00000_00000_10000 }, //0 4
    { 0b00_00000_00000_00000_00000_00000_00010, 0b00_00000_00000_00000_00000_00001_10000 }, //1 4 5
    { 0b00_00000_00000_00000_00000_00000_00100, 0b00_0000_000000_00000_00000_00011_00000 }, //2 5 6
    { 0b00_00000_00000_00000_00000_00000_01000, 0b00_0000_000000_00000_00000_00110_00000 }, //3 6 7
    { 0b00_00000_00000_00000_00000_00000_10000, 0b00_0000_000000_00000_00000_11000_00000 }, //4 8 9
    { 0b00_00000_00000_00000_00000_00001_00000, 0b00_0000_000000_00000_00001_10000_00000 }, //5 9 10
    { 0b00_00000_00000_00000_00000_00010_00000, 0b00_0000_000000_00000_00011_00000_00000 }, //6 10 11
    { 0b00_00000_00000_00000_00000_00100_00000, 0b00_0000_000000_00000_00010_00000_00000 }, //7 11
    { 0b00_00000_00000_00000_00000_01000_00000, 0b00_0000_000000_00000_00100_00000_00000 }, //8 12
    { 0b00_00000_00000_00000_00000_10000_00000, 0b00_0000_000000_00000_01100_00000_00000 }, //9 12 13
    { 0b00_00000_00000_00000_00001_00000_00000, 0b00_0000_000000_00000_11000_00000_00000 }, //10 13 14
    { 0b00_00000_00000_00000_00010_00000_00000, 0b00_0000_000000_00001_10000_00000_00000 }, //11 14 15
    { 0b00_00000_00000_00000_00100_00000_00000, 0b00_0000_000000_00110_00000_00000_00000 }, //12 16 17
    { 0b00_00000_00000_00000_01000_00000_00000, 0b00_0000_000000_01100_00000_00000_00000 }, //13 17 18
    { 0b00_00000_00000_00000_10000_00000_00000, 0b00_0000_000000_11000_00000_00000_00000 }, //14 18 19
    { 0b00_00000_00000_00001_00000_00000_00000, 0b00_0000_000000_10000_00000_00000_00000 }, //15 19
    { 0b00_00000_00000_00010_00000_00000_00000, 0b00_0000_000001_00000_00000_00000_00000 }, //16 20
    { 0b00_00000_00000_00100_00000_00000_00000, 0b00_0000_000011_00000_00000_00000_00000 }, //17 20 21
    { 0b00_00000_00000_01000_00000_00000_00000, 0b00_0000_000110_00000_00000_00000_00000 }, //18 21 22
    { 0b00_00000_00000_10000_00000_00000_00000, 0b00_0000_001100_00000_00000_00000_00000 }, //19 22 23
    { 0b00_00000_00001_00000_00000_00000_00000, 0b00_0000_110000_00000_00000_00000_00000 }, //20 24 25
    { 0b00_00000_00010_00000_00000_00000_00000, 0b00_0001_100000_00000_00000_00000_00000 }, //21 25 26
    { 0b00_00000_00100_00000_00000_00000_00000, 0b00_0011_000000_00000_00000_00000_00000 }, //22 26 27
    { 0b00_00000_01000_00000_00000_00000_00000, 0b00_0010_000000_00000_00000_00000_00000 }, //23 27
    { 0b00_00000_10000_00000_00000_00000_00000, 0b00_0100_000000_00000_00000_00000_00000 }, //24 28
    { 0b00_00001_00000_00000_00000_00000_00000, 0b00_1100_000000_00000_00000_00000_00000 }, //25 28 29
    { 0b00_00010_00000_00000_00000_00000_00000, 0b01_1000_000000_00000_00000_00000_00000 }, //26 29 30
    { 0b00_00100_00000_00000_00000_00000_00000, 0b11_0000_000000_00000_00000_00000_00000 } //27 30 31
};
    public readonly static Dictionary<uint, uint> Damki = new()
    {
    };
    public readonly static Dictionary<uint, uint> StepsByDirection = new()
{
    { UintHelper.CreateNumber(0, 4), UintHelper.CreateNumber(9)},
    { UintHelper.CreateNumber(1, 4), UintHelper.CreateNumber(8)},
    { UintHelper.CreateNumber(1, 5), UintHelper.CreateNumber(10)},
    { UintHelper.CreateNumber(2, 5), UintHelper.CreateNumber(9)},
    { UintHelper.CreateNumber(2, 6), UintHelper.CreateNumber(11)},
    { UintHelper.CreateNumber(3, 6), UintHelper.CreateNumber(10)},
    { UintHelper.CreateNumber(4, 9), UintHelper.CreateNumber(13)},
    { UintHelper.CreateNumber(5, 9), UintHelper.CreateNumber(12)},
    { UintHelper.CreateNumber(5, 10), UintHelper.CreateNumber(14)},
    { UintHelper.CreateNumber(6, 10), UintHelper.CreateNumber(13)},
    { UintHelper.CreateNumber(6, 11), UintHelper.CreateNumber(15)},
    { UintHelper.CreateNumber(7, 11), UintHelper.CreateNumber(14)},
    { UintHelper.CreateNumber(8, 12), UintHelper.CreateNumber(17)},
    { UintHelper.CreateNumber(9, 12), UintHelper.CreateNumber(16)},
    { UintHelper.CreateNumber(9, 13), UintHelper.CreateNumber(18)},
    { UintHelper.CreateNumber(10, 13), UintHelper.CreateNumber(17)},
    { UintHelper.CreateNumber(10, 14), UintHelper.CreateNumber(19)},
    { UintHelper.CreateNumber(11, 14), UintHelper.CreateNumber(18)},
    { UintHelper.CreateNumber(12, 17), UintHelper.CreateNumber(21)},
    { UintHelper.CreateNumber(13, 17), UintHelper.CreateNumber(20)},
    { UintHelper.CreateNumber(13, 18), UintHelper.CreateNumber(22)},
    { UintHelper.CreateNumber(14, 18), UintHelper.CreateNumber(21)},
    { UintHelper.CreateNumber(14, 19), UintHelper.CreateNumber(23)},
    { UintHelper.CreateNumber(15, 19), UintHelper.CreateNumber(22)},
    { UintHelper.CreateNumber(16, 20), UintHelper.CreateNumber(25)},
    { UintHelper.CreateNumber(17, 20), UintHelper.CreateNumber(24)},
    { UintHelper.CreateNumber(17, 21), UintHelper.CreateNumber(26)},
    { UintHelper.CreateNumber(18, 21), UintHelper.CreateNumber(25)},
    { UintHelper.CreateNumber(18, 22), UintHelper.CreateNumber(27)},
    { UintHelper.CreateNumber(19, 22), UintHelper.CreateNumber(26)},
    { UintHelper.CreateNumber(20, 25), UintHelper.CreateNumber(29)},
    { UintHelper.CreateNumber(21, 25), UintHelper.CreateNumber(28)},
    { UintHelper.CreateNumber(21 ,26), UintHelper.CreateNumber(30)},
    { UintHelper.CreateNumber(22, 26), UintHelper.CreateNumber(29)},
    { UintHelper.CreateNumber(22, 27), UintHelper.CreateNumber(31)},
    { UintHelper.CreateNumber(23, 27), UintHelper.CreateNumber(30)},
};
}