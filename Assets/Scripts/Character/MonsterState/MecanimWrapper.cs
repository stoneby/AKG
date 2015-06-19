using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MecanimWrapper : MonoBehaviour
{
    public Animator animator;
    public StateBehaviour[] stateBehaviours;

    static readonly int CurrentStateTimeHash = Animator.StringToHash("currentStateTime");
    Dictionary<int, Behaviour[]> behaviourCache;
    int currentState;
    float currentStateTime;

    float CurrentStateTime
    {
        get
        {
            return currentStateTime;
        }
        set
        {
            Debug.LogWarning(animator.gameObject.name);
            currentStateTime = value;
            animator.SetFloat(CurrentStateTimeHash, currentStateTime);
        }
    }

    void Start()
    {
        behaviourCache = new Dictionary<int, Behaviour[]>();
        foreach (StateBehaviour item in stateBehaviours)
        {
            int nameHash = Animator.StringToHash(item.layer + "." + item.state);
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
        int state = animator.GetCurrentAnimatorStateInfo(0).nameHash;
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