﻿// x, sopernik, all
public static class Masks
{
    public static Dictionary<uint, uint> Forward = new()
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
    public static Dictionary<uint, uint> ForwardKill = new()
    {
        { 0b00_00000_00000_00000_00000_00000_00001, 0b00_00000_00000_00000_00000_10000_00000 }, //0 9
        { 0b00_00000_00000_00000_00000_00000_00010, 0b00_00000_00000_00000_00001_01000_00000 }, //1 8 10
        { 0b00_00000_00000_00000_00000_00000_00100, 0b00_00000_00000_00000_00010_10000_00000 }, //2 9 11
        { 0b00_00000_00000_00000_00000_00000_01000, 0b00_00000_00000_00000_00001_00000_00000 }, //3 10
        { 0b00_00000_00000_00000_00000_00000_10000, 0b00_00000_00000_00000_01000_00000_00000 }, //4 13
        { 0b00_00000_00000_00000_00000_00001_00000, 0b00_00000_00000_00000_10100_00000_00000 }, //5 12 14
        { 0b00_00000_00000_00000_00000_00010_00000, 0b00_00000_00000_00001_01000_00000_00000 }, //6 13 15
        { 0b00_00000_00000_00000_00000_00100_00000, 0b00_00000_00000_00000_10000_00000_00000 }, //7 14
        { 0b00_00000_00000_00000_00000_01000_00000, 0b00_00000_00000_00100_00000_00000_00000 }, //8 17
        { 0b00_00000_00000_00000_00000_10000_00000, 0b00_00000_00000_01010_00000_00000_00000 }, //9 16 18
        { 0b00_00000_00000_00000_00001_00000_00000, 0b00_00000_00000_10100_00000_00000_00000 }, //10 17 19
        { 0b00_00000_00000_00000_00010_00000_00000, 0b00_00000_00000_01000_00000_00000_00000 }, //11 18
        { 0b00_00000_00000_00000_00100_00000_00000, 0b00_00000_00010_00000_00000_00000_00000 }, //12 21
        { 0b00_00000_00000_00000_01000_00000_00000, 0b00_00000_00101_00000_00000_00000_00000 }, //13 20 22
        { 0b00_00000_00000_00000_10000_00000_00000, 0b00_00000_01010_00000_00000_00000_00000 }, //14 21 23
        { 0b00_00000_00000_00001_00000_00000_00000, 0b00_00000_00100_00000_00000_00000_00000 }, //15 22
        { 0b00_00000_00000_00010_00000_00000_00000, 0b00_00001_00000_00000_00000_00000_00000 }, //16 25
        { 0b00_00000_00000_00100_00000_00000_00000, 0b00_00010_10000_00000_00000_00000_00000 }, //17 24 26
        { 0b00_00000_00000_01000_00000_00000_00000, 0b00_00101_00000_00000_00000_00000_00000 }, //18 25 27
        { 0b00_00000_00000_10000_00000_00000_00000, 0b00_00010_00000_00000_00000_00000_00000 }, //19 26
        { 0b00_00000_00001_00000_00000_00000_00000, 0b00_10000_00000_00000_00000_00000_00000 }, //20 29
        { 0b00_00000_00010_00000_00000_00000_00000, 0b01_01000_00000_00000_00000_00000_00000 }, //21 28 30
        { 0b00_00000_00100_00000_00000_00000_00000, 0b11_00000_00000_00000_00000_00000_00000 }, //22 29 31
        { 0b00_00000_01000_00000_00000_00000_00000, 0b01_00000_00000_00000_00000_00000_00000 } //23 30
    };
    public static Dictionary<uint, IHod> ForwardKillStruct = new()
    {
        { 0b00_00000_00000_00000_00000_00000_00001, new SingleMask(0b00_00000_00000_00000_00000_10000_10000) }, //0 9
        { 0b00_00000_00000_00000_00000_00000_00010, new DoubleMask(0b00_00000_00000_00000_00001_01001_10000) }, //1 8 10
        { 0b00_00000_00000_00000_00000_00000_00100, new DoubleMask(0b00_00000_00000_00000_00010_10011_00000) }, //2 9 11
        { 0b00_00000_00000_00000_00000_00000_01000, new SingleMask(0b00_00000_00000_00000_00001_00010_00000) }, //3 10
        { 0b00_00000_00000_00000_00000_00000_10000, new SingleMask(0b00_00000_00000_00000_01000_10000_00000) }, //4 13
        { 0b00_00000_00000_00000_00000_00001_00000, new DoubleMask(0b00_00000_00000_00000_10101_10000_00000) }, //5 12 14
        { 0b00_00000_00000_00000_00000_00010_00000, new DoubleMask(0b00_00000_00000_00001_01011_00000_00000) }, //6 13 15
        { 0b00_00000_00000_00000_00000_00100_00000, new SingleMask(0b00_00000_00000_00000_10010_00000_00000) }, //7 14
        { 0b00_00000_00000_00000_00000_01000_00000, new SingleMask(0b00_00000_00000_00100_00100_00000_00000) }, //8 17
        { 0b00_00000_00000_00000_00000_10000_00000, new DoubleMask(0b00_00000_00000_01010_01100_00000_00000) }, //9 16 18
        { 0b00_00000_00000_00000_00001_00000_00000, new DoubleMask(0b00_00000_00000_10100_11000_00000_00000) }, //10 17 19
        { 0b00_00000_00000_00000_00010_00000_00000, new SingleMask(0b00_00000_00000_01000_10000_00000_00000) }, //11 18
        { 0b00_00000_00000_00000_00100_00000_00000, new SingleMask(0b00_00000_00010_00010_00000_00000_00000) }, //12 21
        { 0b00_00000_00000_00000_01000_00000_00000, new DoubleMask(0b00_00000_00101_01100_00000_00000_00000) }, //13 20 22
        { 0b00_00000_00000_00000_10000_00000_00000, new DoubleMask(0b00_00000_01010_11000_00000_00000_00000) }, //14 21 23
        { 0b00_00000_00000_00001_00000_00000_00000, new SingleMask(0b00_00000_00100_10000_00000_00000_00000) }, //15 22
        { 0b00_00000_00000_00010_00000_00000_00000, new SingleMask(0b00_00001_00001_00000_00000_00000_00000) }, //16 25
        { 0b00_00000_00000_00100_00000_00000_00000, new DoubleMask(0b00_00010_10011_00000_00000_00000_00000) }, //17 24 26
        { 0b00_00000_00000_01000_00000_00000_00000, new DoubleMask(0b00_00101_00110_00000_00000_00000_00000) }, //18 25 27
        { 0b00_00000_00000_10000_00000_00000_00000, new SingleMask(0b00_00010_00100_00000_00000_00000_00000) }, //19 26
        { 0b00_00000_00001_00000_00000_00000_00000, new SingleMask(0b00_10001_00000_00000_00000_00000_00000) }, //20 29
        { 0b00_00000_00010_00000_00000_00000_00000, new DoubleMask(0b01_01011_00000_00000_00000_00000_00000) }, //21 28 30
        { 0b00_00000_00100_00000_00000_00000_00000, new DoubleMask(0b11_00110_00000_00000_00000_00000_00000) }, //22 29 31
        { 0b00_00000_01000_00000_00000_00000_00000, new SingleMask(0b01_00100_00000_00000_00000_00000_00000) } //23 30
    };

}