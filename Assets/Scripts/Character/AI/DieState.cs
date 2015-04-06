using UnityEngine;
using System.Collections;

public class DieState : MonoBehaviour 
{
	public float DisappearTime;

	private MonsterControll monster;

	void OnEnable()
	{
		rigidbody2D.velocity = Vector2.zero;

		Destroy(gameObject, DisappearTime);
	}

	void Awake()
	{
		monster = GetComponent<MonsterControll>();
	}
}
