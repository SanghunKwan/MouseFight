using UnityEngine;
using DefineEnums;
using System.Collections.Generic;

public class TableManager : MonoBehaviour
{
    const string _tableFolder = "Tables/";

    Dictionary<TableType, TableBase> _tableDict = new Dictionary<TableType, TableBase>();

    public IReadOnlyDictionary<TableType, TableBase> TableDict => _tableDict;


    public void InitManager()
    {
        _tableDict = new Dictionary<TableType, TableBase>();

        LoadTable<BaseData>(TableType.BaseData);
        LoadTable<UnitData>(TableType.UnitData);
    }

    void LoadTable<T>(TableType tableType) where T : TableBase, new()
    {
        TextAsset table = Resources.Load<TextAsset>(_tableFolder + tableType.ToString());

        T tableInstance = new T();
        tableInstance.LoadText(table.text);

        _tableDict.Add(tableType, tableInstance);
    }
}
