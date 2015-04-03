using UnityEngine;
using System.Collections;

public class CharacterCommon : MonoBehaviour 
{
	[HideInInspector]
	public bool FacingRight = true;			// For determining which way the player is currently facing.

	public int HurtHealth;

	private CharacterHealth health;
	private CharacterInformation infor;

	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

		var inforScale = infor.HurtLocation.transform.localScale;
		inforScale.x *= -1;
		infor.HurtLocation.transform.localScale = inforScale;
	}
	
	public void Hurt()
	{
		health.HurtHealth = HurtHealth;
		health.Hurt();
	}

	void Awake()
	{
		health = GetComponent<CharacterHealth>();
		infor = GetComponent<CharacterInformation>();
	}
}
