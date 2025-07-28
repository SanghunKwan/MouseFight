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
}
