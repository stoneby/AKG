using UnityEngine;

public class TowerDieState : MonoBehaviour 
{
	public AudioClip Clip;

	void OnEnable()
	{
		GetComponent<AudioSource>().clip = Clip;
		GetComponent<AudioSource>().Play();
	}

	public void OnDie()
	{
		Destroy(gameObject);
	}

	void Start()
	{
	}
}