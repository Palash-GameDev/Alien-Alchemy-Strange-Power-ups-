using System;
public static class Levels 
{
    public static string level1 = "GamePlay-1-Fly";
    public static string level2 = "GamePlay-2-Gravity";
    public static string level3 = "GamePlay-3-Anti-Gravity";
    public static string level4 = "GamePlay-4-parashoot";
}

public enum LevelStatus
{
    Locked,
    Unlocked,
    Completed
}