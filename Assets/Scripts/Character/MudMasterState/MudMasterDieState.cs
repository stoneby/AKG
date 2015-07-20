using UnityEngine;

public class MudMasterDieState : MonoBehaviour 
{
	public void OnDie()
	{
		Destroy(gameObject);
	}

	void Start()
	{
	}
}