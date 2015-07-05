using UnityEngine;

public class MenuController : SSController
{
    public void GoBattleMenu()
    {
        SSSceneManager.Instance.Screen("BattleMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
