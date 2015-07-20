using UnityEngine;

public class TowerDieState : MonoBehaviour 
{
	public void OnDie()
	{
		Destroy(gameObject);
	}

	void Start()
	{
	}
}