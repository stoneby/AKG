using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		//Debug.Log("Trap Trigger enter: " + collider.name);

		if (collider.tag.Equals("Player"))
		{
			var player = collider.transform.GetComponent<PlayerControl>();
			player.HurtFront = true;

			var characterCommon = player.GetComponent<CharacterCommon>();
			characterCommon.Hurt();
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		//sDebug.Log("Trap Trigger exit: " + collider.name);

		if (collider.tag.Equals("Player"))
		{
		}
	}
}
