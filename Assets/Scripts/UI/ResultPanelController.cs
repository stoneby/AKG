using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultPanelController : MonoBehaviour 
{
	public bool Victory 
	{
		set 
		{
			text.text = (value) ? "You Win" : "You Lose";
		}
	}

	private Text text;

	public void OnRestartClicked()
	{
		SSSceneManager.Instance.Screen("Menu");
		gameObject.SetActive(false);
	}

	void Awake()
	{
		text = transform.Find("Text").GetComponent<Text>();
	}
}
