using UnityEngine;

public class CharacterInformation : MonoBehaviour 
{
	public GameObject DamageInforPrefab;
	public int Damage;

	public float ShowTime;

	[HideInInspector]
	public Transform DamageLocation;
	private DamageInforHUDController damageHUDController;

	public void Show(bool flag)
	{
		if(flag)
		{
			damageHUDController.Damage = Damage;

			CancelInvoke("Hide");
			Invoke("Hide", ShowTime);
		}
		damageHUDController.Show(flag);
	}


	private void Hide()
	{
		damageHUDController.Show(false);
	}

	void Awake()
	{
		DamageLocation = transform.Find("DamageInforIcon");
		var damageObject = Instantiate(DamageInforPrefab, DamageLocation.position, DamageLocation.rotation) as GameObject;
		damageHUDController = damageObject.GetComponent<DamageInforHUDController>();
		damageHUDController.transform.parent = DamageLocation.transform;

		damageHUDController.Show(false);
	}
}
