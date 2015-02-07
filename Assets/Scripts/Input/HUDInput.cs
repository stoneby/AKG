using UnityEngine;
using System.Collections;
using System;

public class HUDInput : AbstractInput
{
	public float Horizontal {get;set;}
	public float Vertical {get;set;}
	public bool Jump {get;set;}
	public bool Fire {get;set;}

	public override float GetHorizontal ()
	{
		return Horizontal;
	}

	public override float GetVertical()
	{
		return Vertical;
	}

	public override bool DoesJump ()
	{
		return Jump;
	}

	public override bool DoesFire ()
	{
		return Fire;
	}
}
