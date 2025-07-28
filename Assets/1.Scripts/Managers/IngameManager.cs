using UnityEngine;

public class IngameManager : MonoBehaviour
{
    static IngameManager _instance;
    public static IngameManager Instance => _instance;

    [SerializeField] CameraManager _camManager;
    [SerializeField] SpawnFactoryManager _spawnFactoryManager;
    [SerializeField] Character _player;
    [SerializeField] float _camOffset = 11;

    float currentTime;
    int Kills;
    


    private void Awake()
    {
        _instance = this;
        _camManager.Init(_camOffset);
        _camManager.EnrollUnit(_player);
        _spawnFactoryManager.InitManager();

        _player.InitUnit(0);
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_camManager.GetClickPosition(Input.mousePosition, out RaycastHit floorHit))
            {
                if (floorHit.collider.CompareTag("Enemy"))
                {
                    _player.SetTarget(floorHit.collider.gameObject.GetComponent<UnitBase>());
                }
                else
                    _player.Move(floorHit.point);
            }
        }



    }


    public void DamageCalculate(float damage, UnitBase target)
    {
        //damage�� target._baseStatus�� �̿��� ����� ���.
        target.Damage(damage);
        //target���� �ǰ� ����Ʈ ����.

    }

    public void DieUnit()
    {

    }
}
