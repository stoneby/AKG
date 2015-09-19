using UnityEngine;

public class MudMasterDieState : MonoBehaviour 
{
	public AudioClip[] ClipList;

	void OnEnable()
	{
		audio.clip = ClipList[Random.Range(0, ClipList.Length - 1)];
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