using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour 
{
	private CharacterData characterData;
	private CharacterHealth characterHealth;

	private List<ItemController> heartList = new List<ItemController>();
	private List<ItemController> tntList = new List<ItemController>();
	private List<ItemController> goldList = new List<ItemController>();
	private List<ItemController> diamondList = new List<ItemController>();
	private List<ItemController> cupList = new List<ItemController>();

	private Transform heartLocation;
	private Transform tntLocation;
	private Transform goldLocation;
	private Transform diamondLocation;
	private Transform cupLocation;

	void OnHeartActivate(Item item)
	{
		characterHealth.AddHealth(item.Amount);

		var itemMover = item.GetComponent<ItemMover>();
		itemMover.Target = InterfaceController.Instance.HeroContainer;
		itemMover.Move();
	}

	void OnTNTActivate(Item item)
	{
		characterData.SkillQ += item.Amount;
	}

	void OnGoldActivate(Item item)
	{
	}

	void OnDiamondActivate(Item item)
	{
		// test heart.
		OnHeartActivate(item);
	}

	void OnCupActivate(Item item)
	{
	}

	void Awake()
	{
		heartLocation = transform.Find("HeartGroup");
		tntLocation = transform.Find("TNTGroup");
		goldLocation = transform.Find("GoldGroup");
		diamondLocation = transform.Find("DiamondGroup");
		cupLocation = transform.Find("CupGroup");
	}

	void Start()
	{
		heartList.AddRange(heartLocation.GetComponentsInChildren<ItemController>());
		tntList.AddRange(tntLocation.GetComponentsInChildren<ItemController>());
		goldList.AddRange(goldLocation.GetComponentsInChildren<ItemController>());
		diamondList.AddRange(diamondLocation.GetComponentsInChildren<ItemController>());
		cupList.AddRange(cupLocation.GetComponentsInChildren<ItemController>());

		heartList.ForEach(heart => heart.OnActivate += OnHeartActivate);
		tntList.ForEach(tnt => tnt.OnActivate += OnTNTActivate);
		goldList.ForEach(gold => gold.OnActivate += OnGoldActivate);
		diamondList.ForEach(diamond => diamond.OnActivate += OnDiamondActivate);
		cupList.ForEach(cup => cup.OnActivate += OnCupActivate);

		characterData = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterData>();
		characterHealth = characterData.GetComponent<CharacterHealth>();
	}
}
