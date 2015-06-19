using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject Prefab;

    void Spawn()
    {
        var go = Instantiate(Prefab) as GameObject;
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
    }

    void Awake()
    {
        Spawn();
    }
}
