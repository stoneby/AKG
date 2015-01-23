using System.Collections.Generic;
using UnityEngine;

public class StateMachine : Singleton<StateMachine>
{
    public List<Abstractstate> StateList;

    public string LastState;
    public string CurrentState;

    protected Dictionary<string, Abstractstate> StateMap;

    public void Execute()
    {
        if (string.IsNullOrEmpty(CurrentState))
        {
            Debug.LogError("CurrentState should not be null or empty, set it before execute statemachine.");
            return;
        }

        if (!string.IsNullOrEmpty(LastState))
        {
            var lastState = StateMap[LastState];
            lastState.OnExit();
        }

        var currentState = StateMap[CurrentState];
        currentState.OnEnter();

        LastState = CurrentState;
    }

    public void SetNextState(string nextState)
    {
        CurrentState = nextState;
        Execute();
    }

    private void CollectStates()
    {
        if (StateList == null)
        {
            StateList = new List<Abstractstate>();
        }

        if (StateMap == null)
        {
            StateMap = new Dictionary<string, Abstractstate>();
        }

        for (var i = 0; i < transform.childCount; ++i)
        {
            var state = transform.GetChild(i).GetComponent<Abstractstate>();
            state.StateMachine = this;
            state.Name = state.name;
            StateList.Add(state);
            StateMap[state.Name] = state;
        }

        if (StateList.Count == 0)
        {
            Debug.LogWarning("There is nothing states at all. please double check.");
        }
    }

    private void Awake()
    {
        CollectStates();
    }

    private void Start()
    {
        Execute();  
    }
}
