using UnityEngine;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;

public class DAL : MonoBehaviour
{

    public static string[] getData_ssr(string whereClause)
    {
        DataRow[] rowset = getSortedResults(whereClause);
        int len = rowset.Length;
        string[] sss = new string[len];
        int i = 0;
        foreach (DataRow row in rowset)
        {   
            sss[i] = row[0].ToString();
            i++;
        }
        return sss;
    }

    public static DataRow[] getSortedResults(string whereClause)
    {
        DataRow[] res = DataFeed.data_table.Select(whereClause, DataFeed.Attributes[0]);
        return res;
    }

    public static DataRow[] getFixedRandomRows(string whereClause, int noOfRows)
    {
        DataRow[] res = DataFeed.data_table.Select(whereClause);
        List<DataRow> tempList = res.ToList();
        DataRow[] resultSet = new DataRow[noOfRows];
        if (noOfRows < res.Length)
        {
            for (int i = 0; i < noOfRows; i++)
            {
                int index = UnityEngine.Random.Range(0, tempList.Count);
                resultSet[i] = tempList[index];
                tempList.RemoveAt(index);
            }
        }else
        {
            resultSet = res;
        }
        return resultSet;
    }

    public static DataRow getSingleDataRow(string whereClause)
    {
        DataRow[] res = DataFeed.data_table.Select(whereClause);
        if (res != null && res.Length>0)
        {
            int index = UnityEngine.Random.Range(0, res.Length);
            return res[index];
        }
        else
            return null;
    }

    public static void MakTheRow(DataRow row)
    {
        row[3] = true;
    }

    public static void ResetMarkedRows(string whereClause)
    {
        DataRow[] rowset = DataFeed.data_table.Select(whereClause);
        for (int i = 0; i < rowset.Length; i++)
        {
            rowset[i][3] = false;
        }
    }


}
