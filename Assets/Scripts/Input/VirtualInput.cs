using UnityEngine;
using System.Collections;
using System;

public class VirtualInput : AbstractInput
{
	public override float GetHorizontal()
	{
		return Input.GetAxis("Horizontal");
	}

	public override float GetVertical()
	{
		return Input.GetAxis("Vertical");
	}

	public override bool DoesJump()
	{
		return Input.GetButtonDown("Jump");
	}
	public override bool DoesFire()
	{
		return Input.GetButtonDown("Fire1");
	}

}
