using UnityEngine;

namespace DefineEnums
{
    #region [ĳ����]
    public enum AniState
    {
        Idle,
        Run,
        Attack
    }
    public enum BattleState
    {
        NoneTarget,
        DelayWaiting,
        Attacking,

    }
    #endregion [ĳ����]

    #region [ī�޶�]
    public enum CameraPositionType
    {
        None,
        Scene,
        UnitFollow
    }
    #endregion [ī�޶�]


    #region [���̺�]
    public enum TableType
    {
        BaseData,
        UnitData
    }

    public enum AllIndexType
    {
        INDEX,
        HP,
        ARMOR,
        ATTACKCOOLTIME,
        DAMAGE,
        RANGE,

        Max
    }
    #endregion [���̺�]
}
