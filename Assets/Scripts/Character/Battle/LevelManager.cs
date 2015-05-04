using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentIndex;
    public int TotalCount;

    public bool IsLastLevel { get { return CurrentIndex == TotalCount - 1; } }

    public delegate void LevelUpdate();

    public LevelUpdate OnLevelUpdate;

    public void Init()
    {
        CurrentIndex = -1;
    }

    public void Next()
    {
        ++CurrentIndex;

        if (OnLevelUpdate != null)
        {
            OnLevelUpdate();
        }
    }

    void Awake()
    {
        Init();
    }
}
