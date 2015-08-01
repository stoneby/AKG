using UnityEngine;
using System.Collections;

public class DemoLoading : SSController
{
	public override void OnShow ()
	{
		base.OnShow ();

		Debug.LogError("On loading show");
	}

	public override void OnHide ()
	{
		base.OnHide ();

		Debug.LogError("On loading hide.");
	}
}
