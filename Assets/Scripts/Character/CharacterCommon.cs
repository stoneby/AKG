using System.Collections.Generic;
using UnityEngine;

public class CharacterCommon : MonoBehaviour 
{
	public bool FacingRight = true;			// For determining which way the player is currently facing.

	public int HurtHealth;

    public List<GameObject> AniFlippingList;

    public bool Dead { get { return health.Dead; } }

	private CharacterHealth health;

	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

        AniFlippingList.ForEach(item =>
        {
            var inforScale = item.transform.localScale;
            inforScale.x *= -1;
            item.transform.localScale = inforScale;
        });
	}
	
	public void Hurt()
	{
		health.HurtHealth = HurtHealth;
		health.Hurt();
	}

	void Awake()
	{
		health = GetComponent<CharacterHealth>();
	}
}
