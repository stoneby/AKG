using UnityEngine;

public class LevelEndChecker : MonoBehaviour
{
	public ResultPanelController PanelController;

    private int deadMonsterCount;

	void Dead(GameObject go)
	{
		var monsterDead = !go.tag.Equals("Player");
	    if (monsterDead)
	    {
	        ++deadMonsterCount;
	    }

	    if (!monsterDead || deadMonsterCount == GameData.Instance.MonsterCount)
	    {
            PanelController.gameObject.SetActive(true);

            var victory = (monsterDead) && (deadMonsterCount == GameData.Instance.MonsterCount);
            PanelController.Victory = victory;
        }
	}

    public void Reset()
    {
        deadMonsterCount = 0;
    }
}
