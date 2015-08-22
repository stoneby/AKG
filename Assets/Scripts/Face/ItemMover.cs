using UnityEngine;
using System.Collections;

public class ItemMover : MonoBehaviour 
{
	public Transform Target;
	public float Duration;
	public iTween.EaseType EaseType;

	public void Move()
	{
		var targetLocation = Camera.main.ScreenToWorldPoint(new Vector3(Target.position.x, Target.position.y, transform.position.z - Camera.main.transform.position.z));
		iTween.MoveTo(gameObject, iTween.Hash("position", targetLocation, "time", Duration, "easetype", EaseType));
	}
}
