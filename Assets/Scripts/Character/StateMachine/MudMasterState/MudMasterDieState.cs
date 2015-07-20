using UnityEngine;

public class MudMasterDieState : MonoBehaviour 
{
	private MonsterControll monster;

	public void OnDie()
	{
		Destroy(gameObject);
	}

	void Awake()
	{
		monster = GetComponent<MonsterControll>();
	}
}