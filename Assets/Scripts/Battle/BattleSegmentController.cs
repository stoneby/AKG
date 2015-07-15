using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BattleSegmentController : MonoBehaviour
{
	public List<string> MapList;
	public List<string> MonsterSpawnerList;

    public delegate void LoadComplete();

    public LoadComplete OnLoadComplete;

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
		// keep old spawn object list.
		var oldList = new List<GameObject>(goList);

        yield return StartCoroutine(DoLoad(MapList[index], goList));
        yield return StartCoroutine(DoLoad(MonsterSpawnerList[index], goList));

		if (oldList.Count != 0)
		{
			UnLoad(oldList);
		}

		// for destroy done.
		yield return null;

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
