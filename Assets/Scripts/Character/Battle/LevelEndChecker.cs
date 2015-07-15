using UnityEngine;

public class LevelEndChecker : MonoBehaviour
{
	public ResultPanelController PanelController;

    private int deadMonsterCount;
    private PlayerControl player;

	void Dead(GameObject go)
	{
		var monsterDead = !go.tag.Equals("Player");
	    if (monsterDead)
	    {
			Debug.LogWarning("Monster dead. +++++");
	        ++deadMonsterCount;
	    }

	    if (!monsterDead || deadMonsterCount == GameData.Instance.MonsterCount)
	    {
            PanelController.gameObject.SetActive(true);

            var victory = (monsterDead) && (deadMonsterCount == GameData.Instance.MonsterCount);
            PanelController.Victory = victory;

	        player.enabled = false;
	    }
	}

    public void Reset()
    {
        deadMonsterCount = 0;
    }

    void Awake()
    {
        enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
}
