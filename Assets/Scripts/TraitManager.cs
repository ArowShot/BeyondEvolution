using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TraitManager : MonoBehaviour {
	public TraitManager Instance { get; private set; }

	public Text EvolutionPointsText;
	public GameObject CategoryGui;
	public GameObject TraitsGui;
	public GameObject CategoryPrefab;
	public GameObject TraitPrefab;
	public GameObject ActiveTraitPrefab;
	
	public int EvolutionPoints = 5;

    public List<Trait> AllTraits = new List<Trait>
    {
        new SpeedTrait(),
        new HealingTrait(),
	    new ThornsTrait(),
        new DebugTrait()
	};
	public List<Type> UnlockedTraits = new List<Type>();
	
	public IActiveTrait[] ActiveTraits = new IActiveTrait[4];

	public void ActivateAbility(PlayerController player, int ability)
	{
		var trait = ActiveTraits[ability - 1];
		if (trait == null)
			return;
		
		if (player.Stamina < trait.StaminaRequired)
			return;

		player.Stamina -= trait.StaminaRequired;
		trait.DoActive(player);
	}
	
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
		CreateTraitSelectionGui();
	}

	private void CreateTraitSelectionGui()
	{
		EvolutionPointsText.text = "Evolution Points: " + EvolutionPoints;
			
			
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

			GameObject traitButton;
			if (trait is IActiveTrait)
			{
				traitButton = Instantiate(ActiveTraitPrefab);
				var button1 = traitButton.transform.Find("Button 1").GetComponent<Button>();
				button1.interactable = false;
				
				var button2 = traitButton.transform.Find("Button 2").GetComponent<Button>();
				
				var button3 = traitButton.transform.Find("Button 3").GetComponent<Button>();
				
				var button4 = traitButton.transform.Find("Button 4").GetComponent<Button>();
				
			}
			else
			{
				traitButton = Instantiate(TraitPrefab);
			}
			traitButton.transform.SetParent(category.transform);
			traitButton.GetComponentInChildren<Text>().text = trait.Name + " (" + trait.Cost + " points)";
			traitButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				PurchaseTrait(trait);
			});
			
			
			
			if (UnlockedTraits.Contains(trait.GetType()))
				traitButton.GetComponent<Button>().interactable = false;
		}
	}

	private void Update()
	{
		var player = FindObjectOfType<PlayerController>();
		foreach(var trait in AllTraits)
		{
			if (!UnlockedTraits.Contains(trait.GetType()))
				return;
			
			var passiveTrait = trait as IPassiveTrait;
			if (passiveTrait != null)
			{
				passiveTrait.Update(player);
			}
		}
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

		CreateTraitSelectionGui();

        var atrait = trait as IActiveTrait;
        if (atrait != null)
            ActiveTraits[0] = atrait;
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
		CreateTraitSelectionGui();
	}
	
	public void HideTraitSelection()
	{
		TraitsGui.SetActive(false);
	}
}
