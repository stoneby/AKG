using UnityEngine;

public class TowerDieState : MonoBehaviour 
{
	public AudioClip Clip;

	void OnEnable()
	{
		audio.clip = Clip;
		audio.Play();
	}

	public void OnDie()
	{
		Destroy(gameObject);
	}

	void Start()
	{
	}
}