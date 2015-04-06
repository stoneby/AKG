using UnityEngine;
using System.Collections;

public class ShadowController : MonoBehaviour
{
	private Transform hero;
	
	void Start ()
	{
		hero = transform.parent;
	}

	void Update ()
	{
		var hitList = Physics2D.RaycastAll(hero.position, -Vector2.up, Mathf.Infinity, LayerMask.GetMask("Ground"));
		if (hitList == null || hitList.Length == 0 || hitList[0].collider == null)
		{
			return;
		}
		transform.position = hitList[0].point;
	}
}
