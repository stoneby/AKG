using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public void Load(string level)
    {
        Application.LoadLevelAsync(level);
    }
}
