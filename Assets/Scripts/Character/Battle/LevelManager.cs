using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentIndex;
    public int TotalCount;

    public bool IsLastLevel { get { return CurrentIndex == TotalCount - 1; } }

    public void Init()
    {
        CurrentIndex = -1;
    }

    public void Next()
    {
        ++CurrentIndex;
    }

    void Awake()
    {
        Init();
    }
}
