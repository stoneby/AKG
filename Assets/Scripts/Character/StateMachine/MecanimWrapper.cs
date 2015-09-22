using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MecanimWrapper : MonoBehaviour
{
    public Animator animator;
    public StateBehaviour[] stateBehaviours;

    private static readonly int CurrentStateTimeHash = Animator.StringToHash("currentStateTime");
    private Dictionary<int, Behaviour[]> behaviourCache;
    private Dictionary<int, string> nameCache; 
    private int currentState;
    private float currentStateTime;

    float CurrentStateTime
    {
        get
        {
            return currentStateTime;
        }
        set
        {
            currentStateTime = value;
            animator.SetFloat(CurrentStateTimeHash, currentStateTime);
        }
    }

    void Start()
    {
        behaviourCache = new Dictionary<int, Behaviour[]>();
        nameCache = new Dictionary<int, string>();

        foreach (var item in stateBehaviours)
        {
            var nameHash = Animator.StringToHash(item.layer + "." + item.state);
            nameCache.Add(nameHash, item.state);
            item.behaviours = item.behaviours.Any()
                ? item.behaviours
                : GetComponents<Behaviour>().Where(behaviour => behaviour.GetType().Name.Contains(item.state)).ToArray();
            behaviourCache.Add(nameHash, item.behaviours);
            SetBehavioursEnabled(item.behaviours, false);
        }
    }

    void Update()
    {
        CurrentStateTime += Time.deltaTime;
        var state = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
        if (state != currentState)
        {
            ChangeState(state);
        }
    }

    void ChangeState(int toState)
    {
        if (behaviourCache.ContainsKey(currentState))
        {
            SetBehavioursEnabled(behaviourCache[currentState], false);
        }
        if (behaviourCache.ContainsKey(toState))
        {
            SetBehavioursEnabled(behaviourCache[toState], true);
        }
        currentState = toState;
        CurrentStateTime = 0f;
    }

    void SetBehavioursEnabled(Behaviour[] behaviours, bool enable)
    {
        foreach (Behaviour behaviour in behaviours)
        {
            behaviour.enabled = enable;
        }
    }

    [System.Serializable]
    public class StateBehaviour
    {
        public string state;
        public string layer = "Base Layer";
        public Behaviour[] behaviours;
    }
}