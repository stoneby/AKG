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
	private Transform mudMasterBornTrans;

    void Awake()
    {
		monsterBornTrans = transform.Find("Monster");
		towerBornTrans = transform.Find("Tower");		
		mudMasterBornTrans = transform.Find("MudMaster");

        GetIdleRange();
    }

    void Start()
    {
        idleStateList = monsterBornTrans.GetComponentsInChildren<IdleState>().ToList();
        if (IdleLeftRange.Count != IdleRightRange.Count || IdleLeftRange.Count != idleStateList.Count)
        {
            Debug.LogError("Make sure the counting are equals among left range, right range, and state list, and monster count.");
        }

        for (var i = 0; i < idleStateList.Count; ++i)
        {
            idleStateList[i].Left = IdleLeftRange[i];
            idleStateList[i].Right = IdleRightRange[i];
        }

        var monsterHealthList = monsterBornTrans.GetComponentsInChildren<CharacterHealth>().ToList();
        var towerHealthList = towerBornTrans.GetComponentsInChildren<CharacterHealth>();
		var mudMasterHealthList = mudMasterBornTrans.GetComponentsInChildren<CharacterHealth>();

		HealthList.AddRange(monsterHealthList);
		HealthList.AddRange(towerHealthList);
		HealthList.AddRange(mudMasterHealthList);

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
