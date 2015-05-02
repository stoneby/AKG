using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BattleSegmentController : MonoBehaviour
{
    public string MapName;
    public int Count;

    public delegate void LoadComplete();

    public LoadComplete OnLoadComplete;

    private const string BackgroundName = "Background";
    private const string MonsterSpawnerName = "MonsterSpawner";

	private List<GameObject> goList = new List<GameObject>();

    public void Load(int index)
    {
        StartCoroutine(DoLoadAll(index));
    }

	private void UnLoad(List<GameObject> list)
	{
		list.ForEach(Destroy);
	}

    private IEnumerator DoLoadAll(int index)
    {
		// change 0 based index to 1 based file index.
		index = index + 1;

		// keep old spawn object list.
		var oldList = new List<GameObject>(goList);

        var backgroundPath = string.Format("{0}/{1}/{2}", MapName, index, BackgroundName);
        yield return StartCoroutine(DoLoad(backgroundPath, goList));
        var monsterSpawnerpath = string.Format("{0}/{1}/{2}", MapName, index, MonsterSpawnerName);
        yield return StartCoroutine(DoLoad(monsterSpawnerpath, goList));

		if (oldList.Count != 0)
		{
			UnLoad(oldList);
		}

        if (OnLoadComplete != null)
        {
            OnLoadComplete();
        }
    }

    private IEnumerator DoLoad(string path, List<GameObject> list)
    {
        var request = Resources.LoadAsync<GameObject>(path);
		while (!request.isDone)
		{
			yield return null;
		}
		var go = Instantiate(request.asset) as GameObject;
		list.Add(go);
    }
}
