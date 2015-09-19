using UnityEngine;
using System.Collections;

public class HeroJumpState : MonoBehaviour 
{
	public AudioClip[] ClipList;

	void OnEnable()
	{
		audio.clip = ClipList[Random.Range(0, ClipList.Length - 1)];
		audio.Play();
	}
}
