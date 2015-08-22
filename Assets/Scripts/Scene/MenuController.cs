using UnityEngine;

public class MenuController : SSController
{
	void OnEnable()
	{
		audio.Play();
	}

	void OnDisable()
	{
		audio.Stop();
	}

    public void GoBattleMenu()
    {
        SSSceneManager.Instance.Screen("BattleMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
