using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KeyFrameController : MonoBehaviour 
{
	public List<float> ScaleList;

	public float Time;
	public iTween.EaseType EaseType;

	private enum AnimateStyle
	{
		Counterwise,
		Inverse,
	};

	private AnimateStyle animateStyle;
	private bool isAnimating;
	private List<GameObject> animateObjectList = new List<GameObject>();

	private Transform buttonRoot;
	private GameObject activeButtonFrame;

	public void OnLeftButtonClicked()
	{
		animateStyle = AnimateStyle.Inverse;
		Animate();
	}

	public void OnRightButtonClicked()
	{
		animateStyle = AnimateStyle.Counterwise;
		Animate();
	}

	public void Animate()
	{
		if (isAnimating)
		{
			Debug.LogWarning("Animating please wait.");
			return;
		}

		isAnimating = true;

		if (animateStyle == AnimateStyle.Counterwise)
		{
			StartCoroutine("DoAnimate");
		}
		else
		{
			StartCoroutine("DoAnimateInverse");
		}
	}

	IEnumerator DoAnimate()
	{
		activeButtonFrame.transform.parent = buttonRoot;
		activeButtonFrame.SetActive(false);

		for (var i = 0; i < animateObjectList.Count; ++i)
		{
			var button = animateObjectList[i].transform.GetChild(0).gameObject;
			var targetIndex = (i == animateObjectList.Count - 1) ? 0 : (i + 1);

			MoveTo (button, targetIndex);
			ScaleTo (button, targetIndex);
		}

		yield return new WaitForSeconds(Time);

		for (var i = 0; i < animateObjectList.Count; ++i)
		{
			var button = animateObjectList[i].transform.GetChild(0).gameObject;
			var targetIndex = (i == animateObjectList.Count - 1) ? 0 : (i + 1);

			ParentTo (button, targetIndex);
		}

		Debug.LogWarning("Animating complete.");
		isAnimating = false;

		activeButtonFrame.SetActive(true);
		activeButtonFrame.transform.parent = buttonRoot.FindChild("1");
		activeButtonFrame.transform.SetAsFirstSibling();
	}

	IEnumerator DoAnimateInverse()
	{
		activeButtonFrame.transform.parent = buttonRoot;
		activeButtonFrame.SetActive(false);

		for (var i = animateObjectList.Count - 1; i >= 0; --i)
		{
			var button = animateObjectList[i].transform.GetChild(0).gameObject;
			var targetIndex = (i == 0) ? (animateObjectList.Count - 1) : (i - 1);

			ParentTo (button, targetIndex);

			MoveTo (button, targetIndex);
			ScaleTo (button, targetIndex);
		}

		yield return new WaitForSeconds(Time);

		Debug.LogWarning("Animating complete.");
		isAnimating = false;
	
		EnableActiveFrame();
	}

	void MoveTo (GameObject button, int targetIndex)
	{
		// move to next.
		var targetPosition = animateObjectList [targetIndex].transform.position;
		iTween.MoveTo (button, iTween.Hash ("position", targetPosition, "time", Time, "easetype", EaseType));
	}

	void ScaleTo (GameObject button, int targetIndex)
	{
		// scale to next.
		var targetScale = ScaleList [targetIndex];
		iTween.ScaleTo (button, new Vector2 (targetScale, targetScale), Time);
	}

	void ParentTo (GameObject button, int targetIndex)
	{
		// transform to next.
		var targetParent = buttonRoot.FindChild ("" + (targetIndex + 1));
		button.transform.parent = targetParent;
	}

	void EnableActiveFrame ()
	{
		activeButtonFrame.SetActive (true);
		activeButtonFrame.transform.parent = buttonRoot.FindChild ("1");
		activeButtonFrame.transform.SetAsFirstSibling ();
	}

	void Awake()
	{
	 	buttonRoot = transform.FindChild("Buttons");
		if (buttonRoot.childCount != ScaleList.Count)
		{
			Debug.LogError("Please make sure AnimateObject with Image count: " + animateObjectList.Count + " equals to ScaleList count: " + ScaleList.Count);
		}

		for (var i = 0; i < buttonRoot.childCount; ++i)
		{
			animateObjectList.Add(buttonRoot.FindChild("" + (i + 1)).gameObject);
		}
		activeButtonFrame = transform.FindChild("ActiveButtonFrame").gameObject;
		EnableActiveFrame();
	}
}
