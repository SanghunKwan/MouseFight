using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameManager : MonoBehaviour
{
    static IngameManager _instance;
    public static IngameManager Instance => _instance;

    [SerializeField] CameraManager _camManager;
    [SerializeField] SpawnFactoryManager _spawnFactoryManager;
    [SerializeField] TableManager _tableManager;
    [SerializeField] UIManager _uiManager;

    [SerializeField] Character _player;
    [SerializeField] float _camOffset = 11;
    [SerializeField] GameObject _effect;

    int killCount;

    public TableManager TableManager => _tableManager;

    private void Awake()
    {
        _instance = this;

        _tableManager.InitManager();

        _camManager.Init(_camOffset);
        _camManager.EnrollUnit(_player);
        _spawnFactoryManager.InitManager();

        _player.InitUnit(100);

        _uiManager.InitManager(300);
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
        Instantiate(_effect, target.transform.position + Vector3.up * 2, Quaternion.identity);
        //DelayDestory();
    }
    IEnumerator DelayDestory(GameObject destoryTarget)
    {
        yield return new WaitForSeconds(1f);
        Destroy(destoryTarget);
    }

    public void DieUnit()
    {
        killCount++;
        _uiManager.UpdateKillCount(killCount);
        //_player���� ���� �ߴ� ���.
        _player.Stop();
    }

    public void GameEnd()
    {
        enabled = false;
        Time.timeScale = 0;
    }

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
