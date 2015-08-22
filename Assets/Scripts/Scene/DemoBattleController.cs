
public class DemoBattleController : SSController
{
	void OnEnable()
	{
		GetComponent<UnityEngine.AudioSource>().Play();
	}

	void OnDisable()
	{
		GetComponent<UnityEngine.AudioSource>().Stop();
	}
}
