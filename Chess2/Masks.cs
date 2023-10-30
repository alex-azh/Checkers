// x, sopernik, all
public static class Masks
{
    public static Dictionary<uint, Func<uint, uint, uint, (uint step, IKill kill)>[]> P = new()
    {
        // 0
        { 0b00000000000000000000000000000001,  new Func<uint, uint, uint, (uint step, uint kill)>[1]
            {
                DirectionFuncs.Right
            }
        }, 
        // 1
        { 0b00000000000000000000000000000010,  new Func<uint, uint, uint, (uint step, uint kill)>[2]
            {
                DirectionFuncs.Left, DirectionFuncs.Right
            }
        }, 
        // 2
         { 0b00000000000000000000000000000100,  new Func<uint, uint, uint, (uint step, uint kill)>[2]
            {
                DirectionFuncs.Left, DirectionFuncs.Right
            }
        }, 
         // 3
         { 0b00000000000000000000000000001000,  new Func<uint, uint, uint, (uint step, uint kill)>[2]
            {
                DirectionFuncs.Left, DirectionFuncs.Right
            }
        }, 
         // 4
         { 0b00000000000000000000000000010000,  new Func<uint, uint, uint, (uint step, uint kill)>[2]
            {
                DirectionFuncs.Left, DirectionFuncs.Right
            }
        }, 
         // 5
         { 0b00000000000000000000000000100000,  new Func<uint, uint, uint, (uint step, uint kill)>[2]
            {
                DirectionFuncs.Left, DirectionFuncs.Right
            }
        }, 
         // 6
         { 0b00000000000000000000000001000000,  new Func<uint, uint, uint, (uint step, uint kill)>[2]
            {
                DirectionFuncs.Left, DirectionFuncs.Right
            }
        }, 
         // 7
         { 0b00000000000000000000000010000000,  new Func<uint, uint, uint, (uint step, uint kill)>[1]
            {
                DirectionFuncs.Left
            }
        }, 
         // 8
         { 0b00000000000000000000000100000000,  new Func<uint, uint, uint, (uint step, uint kill)>[1]
            {
                DirectionFuncs.Right
            }
        }
        , 
         // 9
         { 0b00000000000000000000001000000000,  new Func<uint, uint, uint, (uint step, uint kill)>[2]
            {
                DirectionFuncs.Left, DirectionFuncs.Right
            }
        }
    };
}
