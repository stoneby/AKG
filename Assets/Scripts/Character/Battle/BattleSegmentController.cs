using System.Collections;
using UnityEngine;

public class BattleSegmentController : MonoBehaviour
{
    public string MapName;
    public int Count;

    public delegate void LoadComplete();

    public LoadComplete OnLoadComplete;

    private const string BackgroundName = "Background";
    private const string MonsterSpawnerName = "MonsterSpawner";

    public void Load(int index)
    {
        StartCoroutine(DoLoadAll(index));
    }

    private IEnumerator DoLoadAll(int index)
    {
        var backgroundPath = string.Format("{0}/{1}/{2}", MapName, index, BackgroundName);
        yield return StartCoroutine("DoLoad", backgroundPath);
        var monsterSpawnerpath = string.Format("{0}/{1}/{2}", MapName, index, MonsterSpawnerName);
        yield return StartCoroutine("DoLoad", monsterSpawnerpath);

        if (OnLoadComplete != null)
        {
            OnLoadComplete();
        }
    }

    private IEnumerator DoLoad(object value)
    {
        var path = (string)value;
        var request = Resources.LoadAsync<GameObject>(path);
        yield return request;

        var go = request.asset as GameObject;
        go.transform.parent = gameObject.transform;
    }
}
