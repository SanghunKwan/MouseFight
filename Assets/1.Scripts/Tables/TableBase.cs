using System;
using System.Collections.Generic;
using DefineEnums;
using UnityEngine;

public abstract class TableBase
{
    protected Dictionary<int, Dictionary<AllIndexType, string>> _tableData = new Dictionary<int, Dictionary<AllIndexType, string>>();


    //fileDetail을 나누어서 string[]으로 저장.
    public abstract void LoadText(in string fileDetail);



    public string ToStr(int index, AllIndexType type)
    {
        if (_tableData.ContainsKey(index))
        {
            if (_tableData[index].ContainsKey(type))
            {
                return _tableData[index][type];
            }
            else
                Debug.LogError(type + "이 잘못입력되었습니다");
        }
        Debug.LogError("index가 잘못 입력되었습니다");

        return string.Empty;
    }
    public int ToInt(int index, AllIndexType type)
    {
        string strValue = ToStr(index, type);

        if (strValue != string.Empty)
        {
            if (int.TryParse(strValue, out int result))
            {
                return result;
            }
            else
                Debug.LogError("반환값의 형식이 잘못 입력되었습니다.");
        }

        return 0;
    }
    public float ToFloat(int index, AllIndexType type)
    {
        string strValue = ToStr(index, type);

        if (strValue != string.Empty)
        {
            if (float.TryParse(strValue, out float result))
            {
                return result;
            }
            else
                Debug.LogError("반환값의 형식이 잘못 입력되었습니다.");
        }

        return 0;
    }
}
