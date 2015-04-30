using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterGroupInitializer : MonoBehaviour
{
    public List<Transform> IdleLeftRange;
    public List<Transform> IdleRightRange;

    public List<CharacterHealth> HealthList; 

    private List<IdleState> idleStateList;

    void Awake()
    {
        GetIdleRange();
    }

    void Start()
    {
        idleStateList = GetComponentsInChildren<IdleState>().ToList();
        HealthList = idleStateList.Select(item => item.GetComponent<CharacterHealth>()).ToList();

        if (IdleLeftRange.Count != IdleRightRange.Count || IdleLeftRange.Count != idleStateList.Count || IdleLeftRange.Count != GameData.Instance.MonsterCount)
        {
            Debug.LogError("Make sure the counting are equals among left range, right range, and state list, and monster count.");
        }

        for (var i = 0; i < idleStateList.Count; ++i)
        {
            idleStateList[i].Left = IdleLeftRange[i];
            idleStateList[i].Right = IdleRightRange[i];
        }

        HealthList.ForEach(item => item.MessageListener = PresentData.Instance.LevelEndChecker.gameObject);
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
