using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TraitManager : MonoBehaviour {
	public TraitManager Instance { get; private set; }

	public GameObject CategoryGui;
	public GameObject TraitsGui;
	public GameObject CategoryPrefab;
	public Button TraitPrefab;
	
	public int EvolutionPoints = 2;

	public List<Trait> AllTraits = new List<Trait>
	{
		new SpeedTrait()
	};
	public List<Type> UnlockedTraits = new List<Type>();
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		} else if (Instance != this)
		{
			Destroy(this);
		}
	}

	private void Start()
	{
		CreateGui();
	}

	private void CreateGui()
	{
		// Reset Gui
		foreach (Transform child in CategoryGui.transform) {
			Destroy(child.gameObject);
		}
		
		// Forest
		var forestCategory = Instantiate(CategoryPrefab);
		forestCategory.name = "Forest Category";
		forestCategory.transform.SetParent(CategoryGui.transform);
		forestCategory.GetComponentInChildren<Text>().text = "Forest Traits";
		AddTraitsToCategory(Environment.Forest, forestCategory);
		
		
		// Tundra
		var tundraCategory = Instantiate(CategoryPrefab);
		tundraCategory.name = "Tundra Category";
		tundraCategory.transform.SetParent(CategoryGui.transform);
		tundraCategory.GetComponentInChildren<Text>().text = "Tundra Traits";
		AddTraitsToCategory(Environment.Tundra, tundraCategory);
		
		
		// Desert
		var desertCategory = Instantiate(CategoryPrefab);
		desertCategory.name = "Desert Category";
		desertCategory.transform.SetParent(CategoryGui.transform);
		desertCategory.GetComponentInChildren<Text>().text = "Desert Traits";
		AddTraitsToCategory(Environment.Desert, desertCategory);
		
		
		// Global
		var globalCategory = Instantiate(CategoryPrefab);
		globalCategory.name = "Global Category";
		globalCategory.transform.SetParent(CategoryGui.transform);
		globalCategory.GetComponentInChildren<Text>().text = "Global Traits";
		AddTraitsToCategory(Environment.Global, globalCategory);
	}
	
	private void AddTraitsToCategory(Environment env, GameObject category)
	{
		foreach(var trait in AllTraits)
		{
			if (trait.Environment != env)
				continue;
			
			var traitButton = Instantiate(TraitPrefab);
			traitButton.transform.SetParent(category.transform);
			traitButton.GetComponentInChildren<Text>().text = trait.Name + " (" + trait.Cost + " points)";
			traitButton.onClick.AddListener(delegate
			{
				PurchaseTrait(trait);
			});
			
			if (UnlockedTraits.Contains(trait.GetType()))
				traitButton.interactable = false;
		}
	}

	private void Update()
	{
		
	}

	private void PurchaseTrait(Trait trait)
	{
		if (UnlockedTraits.Contains(trait.GetType()))
			return;
		
		var cost = trait.Cost;
		if (EvolutionPoints < cost)
			return;
		
		Debug.Log("Purchased " + trait.Name);
		
		EvolutionPoints -= cost;
		UnlockedTraits.Add(trait.GetType());

		CreateGui();
	}

	public void ApplyTraits(PlayerController player)
	{
		foreach(var trait in AllTraits)
		{
			if (!UnlockedTraits.Contains(trait.GetType()))
				return;
			
			var statTrait = trait as IStatTrait;
			if (statTrait != null)
			{
				statTrait.ApplyTrait(player);
			}
		}
	}
	
	public void ShowTraitSelection()
	{
		TraitsGui.SetActive(true);
		CreateGui();
	}
	
	public void HideTraitSelection()
	{
		TraitsGui.SetActive(false);
	}
}
