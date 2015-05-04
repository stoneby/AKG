using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    private Text countLabel;

    private void SetText(int counter, int total)
    {
        countLabel.text = string.Format("{0} / {1}", counter, total);
    }

    void OnLevelUpdate()
    {
        SetText(GameData.Instance.LevelManager.CurrentIndex + 1, GameData.Instance.LevelManager.TotalCount);
    }

    void Awake()
    {
        countLabel = transform.Find("Text").GetComponent<Text>();
    }

    void Start()
    {
        GameData.Instance.LevelManager.OnLevelUpdate += OnLevelUpdate;
    }
}
