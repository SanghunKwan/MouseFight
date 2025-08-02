using System;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;

public class UnitData : TableBase
{

    enum Index
    {
        INDEX,
        ATTACKCOOLTIME,
        DAMAGE,
        RANGE,

        Max
    }


    public override void LoadText(in string fileDetail)
    {
        string[] record = fileDetail.Split("\n");

        //i == 0 ¸ñÂ÷
        for (int i = 1; i < record.Length; i++)
        {
            string[] data = record[i].Split("|");

            Dictionary<AllIndexType, string> lineDic = new Dictionary<AllIndexType, string>();

            for (int j = 0; j < (int)Index.Max; j++)
            {
                lineDic.Add(Enum.Parse<AllIndexType>(((Index)j).ToString()), data[j]);
            }

            _tableData.Add(int.Parse(data[0]), lineDic);
        }
    }



}
