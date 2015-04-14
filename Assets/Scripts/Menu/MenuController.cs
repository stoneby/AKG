using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        Application.LoadLevelAsync("DemoBattle");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
