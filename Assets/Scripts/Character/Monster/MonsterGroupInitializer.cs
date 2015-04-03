using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterGroupInitializer : MonoBehaviour
{
    public List<Transform> IdleLeftRange;
    public List<Transform> IdleRightRange;

    private List<IdleState> idleStateList;

    void Awake()
    {
        GetIdleRange();
    }

    void Start()
    {
        idleStateList = GetComponentsInChildren<IdleState>().ToList();

        if (IdleLeftRange.Count != IdleRightRange.Count || IdleLeftRange.Count != idleStateList.Count)
        {
            Debug.LogError("Make sure the counting are equals among left range, right range, and state list.");
        }

        for (var i = 0; i < idleStateList.Count; ++i)
        {
            idleStateList[i].Left = IdleLeftRange[i];
            idleStateList[i].Right = IdleRightRange[i];
        }
    }

    private void GetIdleRange()
    {
        if (IdleLeftRange.Count == 0 && IdleRightRange.Count == 0)
        {
            for (var i = 0; i < transform.childCount; ++i)
            {
                IdleLeftRange.Add(transform.GetChild(i).GetChild(0));
                IdleRightRange.Add(transform.GetChild(i).GetChild(1));
            }
        }
    }
}
