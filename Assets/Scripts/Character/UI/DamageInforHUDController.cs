using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageInforHUDController : MonoBehaviour 
{
	public int Damage;

	private Text damageText;

	public void Show(bool show)
	{
		gameObject.SetActive(show);

		if (show)
		{
			damageText.text = "" + Damage;
		}
	}

	void Awake()
	{
		damageText = transform.Find("NumText").GetComponent<Text>();
	}
}
