using UnityEngine;

public class SpawnFactoryManager : MonoBehaviour
{
    [SerializeField] int _SpawnAtOnce;
    [SerializeField] int _SpawnMaxCount;
    [SerializeField] float _SpawnInterval;

    GameObject _prefabEnemy;

    float _currentDelay;

    Transform[] spawnPositions;

    public void InitManager()
    {
        spawnPositions = GetComponentsInChildren<Transform>();
        _prefabEnemy = Resources.Load<GameObject>("Enemy/monster");
    }

    public void Update()
    {
        if (_currentDelay < _SpawnInterval)
        {
            _currentDelay += Time.deltaTime;
        }
        else
        {
            _currentDelay -= _SpawnInterval;
            int spawnIndex = Random.Range(1, spawnPositions.Length);
            //0은 자기 자신이므로 제외

            int spawnCount = 0;
            UnitBase tempBase;
            while (IngameManager.Instance.transform.childCount < _SpawnMaxCount && spawnCount < _SpawnAtOnce)
            {
                tempBase = Instantiate(_prefabEnemy, spawnPositions[spawnIndex].position, Quaternion.identity, IngameManager.Instance.transform).GetComponent<UnitBase>();
                tempBase.InitUnit(0);
            }
        }
    }
}
