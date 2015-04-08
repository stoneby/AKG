using UnityEngine;

public class LevelInit : MonoBehaviour
{
	public ResultPanelController panelController;

	void Dead(GameObject go)
	{
		var victory = !go.tag.Equals("Player");

		panelController.gameObject.SetActive(true);

		panelController.Victory = victory;
	}
}
