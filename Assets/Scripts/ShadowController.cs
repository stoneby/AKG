using UnityEngine;
using System.Collections;

public class ShadowController : MonoBehaviour
{
	private Transform hero;

	// Use this for initialization
	void Start ()
	{
		hero = transform.parent.Find("Body");
	}
	
	// Update is called once per frame
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
