using UnityEngine;
using System.Collections;

public class HeroJumpState : MonoBehaviour 
{
	public AudioClip[] ClipList;

	void OnEnable()
	{
		GetComponent<AudioSource>().clip = ClipList[Random.Range(0, ClipList.Length - 1)];
		GetComponent<AudioSource>().Play();
	}
}
