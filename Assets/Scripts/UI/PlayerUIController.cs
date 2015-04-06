using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIController : MonoBehaviour 
{
	private Slider hpSlider;

	public void UpdateSlider(float value)
	{
		hpSlider.value = value;
	}

	void Awake()
	{
		hpSlider = transform.Find("HPSlider").GetComponent<Slider>();
	}
}
