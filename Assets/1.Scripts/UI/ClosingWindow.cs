using TMPro;
using UnityEngine;

public class ClosingWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _killCount;


    public void InitWindow(string killCount)
    {
        _killCount.text = "�� ų �� : " + killCount;

        gameObject.SetActive(true);



    }

}
