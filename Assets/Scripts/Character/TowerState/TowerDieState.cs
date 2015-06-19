using UnityEngine;

public class TowerDieState : MonoBehaviour 
{
	public float DisappearTime;

	private MonsterControll monster;

	void OnEnable()
	{
		Destroy(gameObject, DisappearTime);
	}

	void Awake()
	{
		monster = GetComponent<MonsterControll>();
	}
}
