using System.Collections.Generic;
using Aig.Client.Integration.Runtime.Analytics;

public class LevelInfo
{
    public const string LEVEL_NUMBER = "level_number";
    public const string LEVEL_NAME = "level_name";
    public const string LEVEL_COUNT = "level_count";
    public const string LEVEL_DIFF = "level_diff";
    public const string LEVEL_LOOP = "level_loop";
    public const string LEVEL_RANDOM = "level_random";
    public const string LEVEL_TYPE = "level_type";

    public int LevelNumber = 0;
    public string LevelName = string.Empty;
    public int LevelCount = 0;
    public string LevelDiff = string.Empty;
    public int LevelLoop = 0;
    public bool LevelRandom = false;
    public string LevelType = string.Empty;

    public override string ToString()
    {
        return $"LevelNumber: {LevelNumber}, LevelName: {LevelName}, LevelCount: {LevelCount}, " +
               $"LevelDiff: {LevelDiff}, LevelLoop: {LevelLoop}, LevelRandom: {LevelRandom}, LevelType: {LevelType}";
    }

    public static void ParseAndAddLevelInfo(LevelInfo levelInfo, Dictionary<string, object> toDictionary)
    {
        if (levelInfo == null)
            return;

        toDictionary.Add(LEVEL_NUMBER, levelInfo.LevelNumber);
        toDictionary.Add(LEVEL_NAME, levelInfo.LevelName);
        toDictionary.Add(LEVEL_COUNT, levelInfo.LevelCount);
        toDictionary.Add(LEVEL_DIFF, levelInfo.LevelDiff);
        toDictionary.Add(LEVEL_LOOP, levelInfo.LevelLoop);
        toDictionary.Add(LEVEL_RANDOM, levelInfo.LevelRandom);
        toDictionary.Add(LEVEL_TYPE, levelInfo.LevelType);
    }
}