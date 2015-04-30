using UnityEngine;

public class MenuController : SSController
{
    public void Play()
    {
        SSSceneManager.Instance.Screen("DemoBattle");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
