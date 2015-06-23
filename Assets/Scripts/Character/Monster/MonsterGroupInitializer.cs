using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterGroupInitializer : MonoBehaviour
{
    public List<Transform> IdleLeftRange;
    public List<Transform> IdleRightRange;

    public List<CharacterHealth> HealthList; 

    private List<IdleState> idleStateList;

	private Transform monsterBornTrans;
	private Transform towerBornTrans;

    void Awake()
    {
		monsterBornTrans = transform.Find("Monster");
		towerBornTrans = transform.Find("Tower");

        GetIdleRange();
    }

    void Start()
    {
        idleStateList = monsterBornTrans.GetComponentsInChildren<IdleState>().ToList();
        var monsterHealthList = idleStateList.Select(item => item.GetComponent<CharacterHealth>()).ToList();

        if (IdleLeftRange.Count != IdleRightRange.Count || IdleLeftRange.Count != idleStateList.Count)
        {
            Debug.LogError("Make sure the counting are equals among left range, right range, and state list, and monster count.");
        }

        for (var i = 0; i < idleStateList.Count; ++i)
        {
            idleStateList[i].Left = IdleLeftRange[i];
            idleStateList[i].Right = IdleRightRange[i];
        }

		var towerHealthList = towerBornTrans.GetComponentsInChildren<CharacterHealth>();

		HealthList.AddRange(monsterHealthList);
		HealthList.AddRange(towerHealthList);
        HealthList.ForEach(item => item.MessageListener = PresentData.Instance.LevelEndChecker.gameObject);
        GameData.Instance.MonsterCount = HealthList.Count;
    }

    private void GetIdleRange()
    {
        if (IdleLeftRange.Count == 0 && IdleRightRange.Count == 0)
        {
			for (var i = 0; i < monsterBornTrans.childCount; ++i)
            {
				IdleLeftRange.Add(monsterBornTrans.GetChild(i).GetChild(0));
				IdleRightRange.Add(monsterBornTrans.GetChild(i).GetChild(1));
            }
        }
    }
}
