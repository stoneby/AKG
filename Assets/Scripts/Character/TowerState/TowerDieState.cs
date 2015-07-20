using UnityEngine;

public class TowerDieState : MonoBehaviour 
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