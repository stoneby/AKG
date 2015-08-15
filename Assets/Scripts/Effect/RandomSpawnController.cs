using UnityEngine;
using System.Collections;

public class RandomSpawnController : MonoBehaviour 
{
	public bool UseRandom;
	public float SleepTime;

	private Animator animator;

	void WakeUp()
	{
		animator.enabled = true;
	}

	void Awake()
	{
		animator = GetComponent<Animator>();

		SleepTime = (UseRandom) ? Random.Range(0, 1f) : SleepTime;
		Invoke("WakeUp", SleepTime);
	}
}
