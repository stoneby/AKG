using UnityEngine;

public abstract class Abstractstate : MonoBehaviour
{
    public string Name;
    public string NextState;

    public StateMachine StateMachine;

    public abstract void OnEnter();
    public abstract void OnExit();
}
