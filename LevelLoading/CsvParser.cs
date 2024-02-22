using System.IO;
using System.Linq;

namespace CSE3902.LevelLoading;

public static class CsvParser
{
    public static string[][] GenerateCsvMatrix(string filePath)
    {
        using StreamReader reader = File.OpenText(filePath);
        string csvInfo = reader.ReadToEnd();
        string[] csvSplitByNewlines = csvInfo.Split("\r\n");
        string[][] csvMatrix = csvSplitByNewlines.Select(s => s.Split(',')).ToArray();

        return csvMatrix;
    }
}