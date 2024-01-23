namespace CheckersGame;

public static class UintHelper
{
    public static uint CreateNumber(params int[] numbers)
    {
        uint result = 0;
        foreach (int index in numbers)
        {
            result |= 1u << index;
        }
        return result;
    }
}