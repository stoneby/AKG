using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour 
{
	public delegate void OnActivateCallback(Item item);
	public OnActivateCallback OnActivate;

	private Animator animator;
	private Item item;

	void OnTriggerEnter2D(Collider2D collider)
	{	
		if (collider.tag.Equals("Player"))
		{
			//Debug.Log("Item Trigger enter: " + collider.name);

			if (OnActivate != null)
			{
				OnActivate(item);
			}

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
		item = GetComponent<Item>();
	}
}
