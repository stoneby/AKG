using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour 
{
	private Animator animator;

	void OnTriggerEnter2D(Collider2D collider)
	{	
		if (collider.tag.Equals("Player"))
		{
			//Debug.Log("Item Trigger enter: " + collider.name);

			var player = collider.transform.GetComponent<PlayerControl>();
			//player.HurtFront = true;
			
			var characterCommon = player.GetComponent<CharacterCommon>();
			//characterCommon.Hurt();

			animator.SetTrigger("Die");
		}
	}
	
	void OnTriggerExit2D(Collider2D collider)
	{	
		if (collider.tag.Equals("Player"))
		{
			//Debug.Log("Item Trigger exit: " + collider.name);
		}
	}

	void Awake()
	{
		animator = GetComponent<Animator>();
	}
}
