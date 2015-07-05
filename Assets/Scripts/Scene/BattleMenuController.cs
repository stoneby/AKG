using UnityEngine;

public class BattleMenuController : SSController
{
    public void GoBattleFight()
    {
        SSSceneManager.Instance.Screen("DemoBattle");
    }
}
