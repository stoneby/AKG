
public class DemoBattleController : SSController
{
	void OnEnable()
	{
		audio.Play();
	}

	void OnDisable()
	{
		audio.Stop();
	}
}
