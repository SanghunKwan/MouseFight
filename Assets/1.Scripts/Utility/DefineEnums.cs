using UnityEngine;

namespace DefineEnums
{
    #region [캐릭터]
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
    #endregion [캐릭터]

    #region [카메라]
    public enum CameraPositionType
    {
        None,
        Scene,
        UnitFollow
    }
    #endregion [카메라]


    #region [테이블]
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
    #endregion [테이블]
}
