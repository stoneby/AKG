using UnityEngine;

public class LevelInitializer : MonoBehaviour 
{
	public BattleSegmentController BattleLoader;
	
	public GameObject HeroPrefab;

	public delegate void LoadComplete();
	public LoadComplete OnLoadComplete;

	private GameObject hero;
	private Vector3 heroBornLocation;
	private Vector3 heroHideLocation = new Vector3(-15, 15, 0);

	public void Load()
	{
		var levelManager = GameData.Instance.LevelManager;
		levelManager.Next();
		BattleLoader.Load(levelManager.CurrentIndex);

		hero.transform.position = heroHideLocation;
	}

	private void OnBattleLoadComplete()
	{
		heroBornLocation = transform.Find("HeroBornLocation").transform.position;

		hero.transform.position = heroBornLocation;
		hero.rigidbody2D.isKinematic = false;

		if (OnLoadComplete != null)
		{
			OnLoadComplete();
		}
	}

	void Start()
	{
		hero = GameObject.FindGameObjectWithTag("Player");
		Load();

		BattleLoader.OnLoadComplete += OnBattleLoadComplete;
	}
}
