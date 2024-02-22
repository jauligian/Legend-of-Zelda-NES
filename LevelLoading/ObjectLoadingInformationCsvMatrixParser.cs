using CSE3902.Shared;
using System.Collections.Generic;

namespace CSE3902.LevelLoading;

public static class ObjectLoadingInformationCsvMatrixParser
{
    public static ObjectsLoadingInformationPlusMetaData GenerateObjectsLoadingInformationPlusMetaData(
        string[][] csvMatrix)
    {
        List<ObjectLoadingInformation> objLoadingInfo = new();
        for (int rowIndex = 0; rowIndex < csvMatrix.Length; rowIndex++)
        {
            string[] row = csvMatrix[rowIndex];
            for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
            {
                if (!row[columnIndex].Equals(""))
                {
                    objLoadingInfo.Add(new ObjectLoadingInformation(row[columnIndex],
                        (columnIndex, rowIndex)));
                }
            }
        }

        return new ObjectsLoadingInformationPlusMetaData(objLoadingInfo, csvMatrix[0]?.Length ?? 0,
            csvMatrix.Length);
    }
}