using UnityEngine;
using UnityEngine.AI;
using DefineStatus;

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

        _baseStatus = new BaseStatus { _armor = 1, _currentHp = 25, _maxHp = 25 };
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
        Destroy(this);
    }

}
