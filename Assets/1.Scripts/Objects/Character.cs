using DefineEnums;
using DefineStatus;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : UnitBase
{

    const float _navAllowError = 0.1f;

    static int _animatorParameterAniState = Animator.StringToHash("AniState");
    static int _animatorParameterAttack1 = Animator.StringToHash("Attack1");
    static int _animatorParameterAttack2 = Animator.StringToHash("Attack2");

    //공격력
    AniState _aniState;
    BattleState _battleState;
    CharacterStatus _characterStatus;

    UnitBase _target;

    Action _queueAction;

    private void Update()
    {
        switch (_battleState)
        {
            case BattleState.NoneTarget:
                if (_characterStatus._currentCoolTime < _characterStatus._attackCoolTime)
                    _characterStatus._currentCoolTime += Time.deltaTime;
                break;

            case BattleState.DelayWaiting:
                _queueAction?.Invoke();

                if (_characterStatus._currentCoolTime < _characterStatus._attackCoolTime)
                    _characterStatus._currentCoolTime += Time.deltaTime;

                if (_navAgent.remainingDistance <= _navAgent.stoppingDistance &&
                _characterStatus._currentCoolTime >= _characterStatus._attackCoolTime)
                    AttackTarget();
                break;

            case BattleState.Attacking:
                // 적 바라봄.
                transform.LookAt(_target.transform.position, Vector3.up);
                break;
        }

        if (_aniState == AniState.Run && _navAgent.hasPath && _navAgent.remainingDistance <= _navAllowError)
        {
            Arrive();
        }
    }
    public override void InitUnit(int unitIndex)
    {
        base.InitUnit(unitIndex);
        _aniState = AniState.Idle;

        TableBase table = IngameManager.Instance.TableManager.TableDict[TableType.UnitData];

        float attackDelay = table.ToFloat(_index, AllIndexType.ATTACKCOOLTIME);
        float damage = table.ToFloat(_index, AllIndexType.DAMAGE);
        float range = table.ToFloat(_index, AllIndexType.RANGE);

        _characterStatus = new CharacterStatus { _attackCoolTime = attackDelay, _currentCoolTime = attackDelay, _damage = damage, _range = range };

    }
    public void Move(Vector3 vec)
    {
        if (_battleState != BattleState.Attacking)
        {
            _navAgent.SetDestination(vec);
            _battleState = BattleState.NoneTarget;
            _aniState = AniState.Run;
            _target = null;
            _aniController.SetInteger(_animatorParameterAniState, (int)_aniState);

            _navAgent.stoppingDistance = 0;
        }
        else
        {
            _queueAction = () =>
            {
                Move(vec);
                _queueAction = null;
            };
        }
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
        _characterStatus._currentCoolTime = 0;

        if (Random.Range(0, 2) == 0)
            _aniController.SetTrigger(_animatorParameterAttack1);
        else
            _aniController.SetTrigger(_animatorParameterAttack2);
    }


    public void Arrive()
    {
        _aniState = AniState.Idle;
        _aniController.SetInteger(_animatorParameterAniState, (int)_aniState);
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
    public void Stop()
    {
        if (_queueAction != null) return;

        //공격중이던 적 사망.
        _queueAction = () =>
        {
            _battleState = BattleState.NoneTarget;
            _aniState = AniState.Idle;
            _target = null;
            _aniController.SetInteger(_animatorParameterAniState, (int)_aniState);

            _queueAction = null;
        };
    }
}
