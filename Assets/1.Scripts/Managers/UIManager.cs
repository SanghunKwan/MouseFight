using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timeText;
    [SerializeField] TextMeshProUGUI _killCount;
    [SerializeField] ClosingWindow _closingWindow;

    float _timeLeft;


    public void InitManager(int leftTime)
    {
        _timeLeft = leftTime;

        int minute = leftTime / 60;
        int second = leftTime % 60;

        _timeText.text = minute.ToString("D2") + ":" + second.ToString("D2");
    }

    private void Update()
    {
        int leftTime = (int)_timeLeft;

        int minute = leftTime / 60;
        int second = leftTime % 60;

        _timeText.text = minute.ToString("D2") + ":" + second.ToString("D2");
        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0)
        {
            IngameManager.Instance.GameEnd();

            ShowClosingWindow();
            enabled = false;
        }
    }
    public void UpdateKillCount(int killCount)
    {
        _killCount.text = killCount.ToString();
    }
    void ShowClosingWindow()
    {
        _timeText.transform.parent.gameObject.SetActive(false);
        _killCount.transform.parent.gameObject.SetActive(false);
        _closingWindow.InitWindow(_killCount.text);
    }
}

