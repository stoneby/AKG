using UnityEngine;
using System.Collections;

public class LoadingTest : MonoBehaviour 
{
	public Animator Fader;

	private string path = "Background";

	private GameObject sync;
	private GameObject async;

	void OnGUI()
	{
		if (GUILayout.Button("Loading Direct"))
		{
			Fader.Play("FadeIn");

			Debug.Log("Loading direct before: " + Time.frameCount);
			var background = Resources.Load(path) as GameObject;
			sync = Instantiate(background) as GameObject;
			Debug.Log("Loading direct after: " + Time.frameCount);
		}

		if (GUILayout.Button("Unload"))
		{
			Fader.Play("FadeOut");

			Destroy(sync);
			Resources.UnloadUnusedAssets();
		}

		if (GUILayout.Button("Loading Unsync"))
		{
			Fader.Play("FadeIn");

			StartCoroutine(LoadingAsync());
			//LoadingAsync ();
		}

		if (GUILayout.Button("Unload"))
		{
			Fader.Play("FadeOut");

			Destroy(async);
			Resources.UnloadUnusedAssets();
		}
	}

	IEnumerator LoadingAsync ()
	{
		Debug.Log ("Loading unsync: " + Time.frameCount);
		var request = Resources.LoadAsync (path);
		yield return request;
		if (request.isDone)
		{
			async = Instantiate(request.asset) as GameObject;
			Debug.Log ("Loading unsync:" + Time.frameCount);
		}
	}
}
