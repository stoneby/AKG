using UnityEngine;

public class MudMasterDieState : MonoBehaviour 
{
	public AudioClip[] ClipList;

	void OnEnable()
	{
		GetComponent<AudioSource>().clip = ClipList[Random.Range(0, ClipList.Length - 1)];
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