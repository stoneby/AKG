using UnityEngine;

public class CharacterInformation : MonoBehaviour 
{
	public GameObject HurtPrefab;

	public float ShowTime;

	[HideInInspector]
	public Transform HurtLocation;
	private GameObject hurtObject;

	public void Show(bool flag)
	{
		hurtObject.SetActive(flag);
		if(flag)
		{
			CancelInvoke("Hide");
			Invoke("Hide", ShowTime);
		}
	}

	private void Hide()
	{
		hurtObject.SetActive(false);
	}

	void Awake()
	{
		HurtLocation = transform.Find("HurtIcon");
		hurtObject = Instantiate(HurtPrefab, HurtLocation.position, HurtLocation.rotation) as GameObject;
		hurtObject.transform.parent = HurtLocation.transform;

		Show(false);
	}
}
