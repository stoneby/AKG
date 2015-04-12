using UnityEngine;
using System.Collections;

public class DynamicSpawner : MonoBehaviour 
{
	public GameObject SpawnPrefab;

	/// <summary>
	/// Flag indicates if spawned instance is parented under current transform.
	/// </summary>
	public bool Parented;

	/// <summary>
	/// Flag indicates if generate while awake.
	/// </summary>
	public bool GenerateInAwake;

	public GameObject SpawnInstance { get; set;}

	public void Generate()
	{
		SpawnInstance = Instantiate(SpawnPrefab, transform.position, transform.rotation) as GameObject;

		if (Parented)
		{
			SpawnInstance.transform.parent = transform;
		}
	}

	void Awake()
	{
		if (GenerateInAwake)
		{
			Generate();
		}
	}
}
