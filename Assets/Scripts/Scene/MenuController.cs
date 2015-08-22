using UnityEngine;

public class MenuController : SSController
{
	void OnEnable()
	{
		GetComponent<AudioSource>().Play();
	}

	void OnDisable()
	{
		GetComponent<AudioSource>().Stop();
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
