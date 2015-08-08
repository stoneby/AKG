using UnityEngine;
using System.Collections;

public class RandomSpawnController : MonoBehaviour 
{
	public float SleepTime;

	private Animator animator;

	void WakeUp()
	{
		animator.enabled = true;
	}

	void Awake()
	{
		animator = GetComponent<Animator>();
		Invoke("WakeUp", SleepTime);
	}
}
