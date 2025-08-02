using DefineEnums;
using DefineStatus;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour
{
    protected NavMeshAgent _navAgent;
    protected Animator _aniController;

    protected int _index;

    protected BaseStatus _baseStatus;
    public float _armed => _baseStatus._armor;


    public virtual void InitUnit(int unitIndex)
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _aniController = GetComponent<Animator>();

        _index = unitIndex;

        TableBase table = IngameManager.Instance.TableManager.TableDict[TableType.BaseData];

        int hp = table.ToInt(_index, AllIndexType.HP);
        float armor = table.ToFloat(_index, AllIndexType.ARMOR);

        _baseStatus = new BaseStatus { _armor = armor, _currentHp = hp, _maxHp = hp };
    }

    public void Damage(float damage)
    {
        _baseStatus._currentHp -= (int)Mathf.Max((damage - _baseStatus._armor), 1);

        if (_baseStatus._currentHp <= 0)
        {
            _aniController.SetTrigger("Death");
            IngameManager.Instance.DieUnit();
        }
    }
    public void OnDie()
    {
        Destroy(gameObject);
    }

}
