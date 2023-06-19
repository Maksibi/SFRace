using System;

public static class StringTIme
{
    public static string SecondToTimeString(float second)
    {
        return TimeSpan.FromSeconds(second).ToString(@"mm\:ss\.fff");
    }
}
