using UnityEngine;
using System.Collections;
using System;

[Serializable]
public abstract class AbstractInput : MonoBehaviour
{
	public abstract float GetHorizontal();
	public abstract float GetVertical();
	public abstract bool DoesJump();
	public abstract bool DoesFire();
}
