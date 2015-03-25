using UnityEngine;
using System.Collections.Generic;

public class MecanimWrapper : MonoBehaviour
{
	public Animator animator;
	public StateBehaviour[] stateBehaviours;
	static int CURRENT_STATE_TIME = Animator.StringToHash ("currentStateTime");
	Dictionary<int, Behaviour[]> behaviourCache;
	int currentState;
	float _currentStateTime;
	
	float currentStateTime {
		get {
			return _currentStateTime;
		}
		set {
			_currentStateTime = value;
			animator.SetFloat (CURRENT_STATE_TIME, _currentStateTime);
		}
	}
	
	void Start ()
	{
		behaviourCache = new Dictionary<int, Behaviour[]> ();
		foreach (StateBehaviour item in stateBehaviours) {
			int nameHash = Animator.StringToHash (item.layer + "." + item.state);
			behaviourCache.Add (nameHash, item.behaviours);
			SetBehavioursEnabled (item.behaviours, false);
		}
	}
	
	void Update ()
	{
		currentStateTime += Time.deltaTime;
		int state = animator.GetCurrentAnimatorStateInfo (0).nameHash;
		if (state != currentState) {
			ChangeState (state);
		}
	}
	
	void ChangeState (int toState)
	{
		if (behaviourCache.ContainsKey (currentState)) {
			SetBehavioursEnabled (behaviourCache [currentState], false);
		}
		if (behaviourCache.ContainsKey (toState)) {
			SetBehavioursEnabled (behaviourCache [toState], true);
		}
		currentState = toState;
		currentStateTime = 0f;
	}
	
	void SetBehavioursEnabled (Behaviour[] behaviours, bool enabled)
	{
		foreach (Behaviour behaviour in behaviours) {
			behaviour.enabled = enabled;
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