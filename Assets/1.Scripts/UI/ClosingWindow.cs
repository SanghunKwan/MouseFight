using TMPro;
using UnityEngine;

public class ClosingWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _killCount;


    public void InitWindow(string killCount)
    {
        _killCount.text = "ÃÑ Å³ ¼ö : " + killCount;

        gameObject.SetActive(true);



    }

}
