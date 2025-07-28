using DefineEnums;
using DefineStatus;
using UnityEngine;

public class Character : UnitBase
{
    static int _animatorParameterAniState = Animator.StringToHash("AniState");
    static int _animatorParameterAttack1 = Animator.StringToHash("Attack1");
    static int _animatorParameterAttack2 = Animator.StringToHash("Attack2");

    //공격력
    AniState _aniState;
    BattleState _battleState;
    CharacterStatus _characterStatus;

    UnitBase _target;


    private void Update()
    {
        if (_characterStatus._currentCoolTime < _characterStatus._attackCoolTime)
            _characterStatus._currentCoolTime += Time.deltaTime;

        switch (_battleState)
        {
            case BattleState.NoneTarget:
                break;

            case BattleState.DelayWaiting:
                if (_navAgent.remainingDistance <= _navAgent.stoppingDistance &&
                _characterStatus._currentCoolTime >= _characterStatus._attackCoolTime)
                    AttackTarget();
                break;

            case BattleState.Attacking:
                // 적 바라봄.
                transform.LookAt(_target.transform.position, Vector3.up);
                break;
        }
    }
    public override void InitUnit(int unitIndex)
    {
        base.InitUnit(unitIndex);
        _aniState = AniState.Idle;

        _baseStatus = new BaseStatus { _armor = 1, _currentHp = 5, _maxHp = 5 };
        _characterStatus = new CharacterStatus { _attackCoolTime = 2, _currentCoolTime = 2, _damage = 10, _range = 6 };
    }
    public void Move(in Vector3 vec)
    {
        if (_battleState == BattleState.Attacking) return;

        _navAgent.SetDestination(vec);
        _battleState = BattleState.NoneTarget;
        _aniState = AniState.Run;
        _aniController.SetInteger(_animatorParameterAniState, (int)_aniState);

        _navAgent.stoppingDistance = 0;
    }
    public void SetTarget(UnitBase target)
    {
        if (_battleState == BattleState.Attacking) return;

        _target = target;
        _navAgent.SetDestination(_target.transform.position);
        _navAgent.stoppingDistance = _characterStatus._range;
        _battleState = BattleState.DelayWaiting;
        _aniState = AniState.Run;
        _aniController.SetInteger(_animatorParameterAniState, (int)_aniState);
    }

    void AttackTarget()
    {
        _aniState = AniState.Attack;
        _battleState = BattleState.Attacking;
        _aniController.SetInteger(_animatorParameterAniState, (int)_aniState);

        _navAgent.isStopped = true;
        if (Random.Range(0, 2) == 0)
            _aniController.SetTrigger(_animatorParameterAttack1);
        else
            _aniController.SetTrigger(_animatorParameterAttack2);

    }
    void OnAttack()
    {
        IngameManager.Instance.DamageCalculate(_characterStatus._damage, _target);
    }
    void OnAttackEnd()
    {
        _aniState = AniState.Idle;
        _aniController.SetInteger(_animatorParameterAniState, (int)_aniState);
        _navAgent.isStopped = false;
        _battleState = BattleState.DelayWaiting;
    }
}
