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
	public GameObject CategoryPrefab;
	public Button TraitPrefab;
	
	public int EvolutionPoints = 2;

	public List<Trait> AllTraits = new List<Trait>
	{
		new SpeedTrait()
	};
	public List<Trait> UnlockedTraits = new List<Trait>();
	
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
		// Forest
		var forestCategory = Instantiate(CategoryPrefab);
		forestCategory.name = "Forest Category";
		forestCategory.transform.SetParent(CategoryGui.transform);
		
		
		// Tundra
		var tundraCategory = Instantiate(CategoryPrefab);
		tundraCategory.name = "Tundra Category";
		tundraCategory.transform.SetParent(CategoryGui.transform);
		
		
		// Desert
		var desertCategory = Instantiate(CategoryPrefab);
		desertCategory.name = "Desert Category";
		desertCategory.transform.SetParent(CategoryGui.transform);
		
		
		// Global
		var globalCategory = Instantiate(CategoryPrefab);
		globalCategory.name = "Global Category";
		globalCategory.transform.SetParent(CategoryGui.transform);

		var speedTrait = AllTraits[0];
		var speedTraitButton = Instantiate(TraitPrefab);
		speedTraitButton.transform.SetParent(globalCategory.transform);
		speedTraitButton.GetComponentInChildren<Text>().text = speedTrait.Name;
		speedTraitButton.onClick.AddListener(delegate
		{
			PurchaseTrait(speedTrait);
		});
	}

	private void Update()
	{
		
	}

	private void PurchaseTrait(Trait trait)
	{
		var cost = trait.Cost;
		Debug.Log(cost);

		EvolutionPoints -= cost;
		
		UnlockedTraits.Add(trait);
	}
	
	public void ShowTraitSelection()
	{
		
	}
}
