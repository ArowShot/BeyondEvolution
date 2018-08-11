using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TraitManager : MonoBehaviour {
	public static TraitManager Instance { get; private set; }

	public Text EvolutionPointsText;
	public GameObject CategoryGui;
	public GameObject TraitsGui;
	public GameObject CategoryPrefab;
	public GameObject TraitPrefab;
	public GameObject ActiveTraitPrefab;
	
	public int EvolutionPoints = 1;

    public List<Trait> AllTraits = new List<Trait>
    {
        new SpeedTrait(),
        new HealingTrait(),
        new ThornsTrait(),
        new DebugTrait(),
        new LaserAttackTrait(),
        new BombTrait(),
        new DashTrait(),
        new ClawTrait(),
        new GuardTrait(),
        new PoisonTrait(),
        new FurTrait(),
        new ScalesTrait()
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
		DontDestroyOnLoad(this);
		if (Instance == null)
		{
			Instance = this;
		} else
		{
			var traitsGui = GameObject.Find("TraitsGUI");
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		Debug.Log("start");
		EvolutionPointsText = GameObject.Find("Evolution Points").GetComponent<Text>();
		TraitsGui = GameObject.Find("TraitsGUI");
		CategoryGui = GameObject.Find("Categories");
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
			var atrait = trait as IActiveTrait;
			if (atrait != null)
			{
				traitButton = Instantiate(ActiveTraitPrefab);
				var button1 = traitButton.transform.Find("AbilitiesButtons/Button 1").GetComponent<Button>();
				if (!UnlockedTraits.Contains(trait.GetType())) {
					button1.interactable = false;
				} else
				{
					if (ActiveTraits[0] != null && ActiveTraits[0].GetType() == trait.GetType())
					{
						button1.interactable = false;
					}

					button1.onClick.AddListener(delegate
					{
						ActiveTraits[0] = atrait;
						CreateTraitSelectionGui();
					});
				}
				
				var button2 = traitButton.transform.Find("AbilitiesButtons/Button 2").GetComponent<Button>();
				if (!UnlockedTraits.Contains(trait.GetType())) {
					button2.interactable = false;
				} else
				{
					if (ActiveTraits[1] != null && ActiveTraits[1].GetType() == trait.GetType())
					{
						button2.interactable = false;
					}

					button2.onClick.AddListener(delegate
					{
						ActiveTraits[1] = atrait;
						CreateTraitSelectionGui();
					});
				}

				var button3 = traitButton.transform.Find("AbilitiesButtons/Button 3").GetComponent<Button>();
				if (!UnlockedTraits.Contains(trait.GetType())) {
					button3.interactable = false;
				} else
				{
					if (ActiveTraits[2] != null && ActiveTraits[2].GetType() == trait.GetType())
					{
						button3.interactable = false;
					}

					button3.onClick.AddListener(delegate
					{
						ActiveTraits[2] = atrait;
						CreateTraitSelectionGui();
					});
				}

				var button4 = traitButton.transform.Find("AbilitiesButtons/Button 4").GetComponent<Button>();
				if (!UnlockedTraits.Contains(trait.GetType())) {
					button4.interactable = false;
				} else
				{
					if (ActiveTraits[3] != null && ActiveTraits[3].GetType() == trait.GetType())
					{
						button4.interactable = false;
					}

					button4.onClick.AddListener(delegate
					{
						ActiveTraits[3] = atrait;
						CreateTraitSelectionGui();
					});
				}
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
		if (CategoryGui == null && GameObject.Find("Categories") != null)
		{
			EvolutionPointsText = GameObject.Find("Evolution Points").GetComponent<Text>();
			TraitsGui = GameObject.Find("TraitsGUI");
			CategoryGui = GameObject.Find("Categories");
			CreateTraitSelectionGui();
		}
		var player = FindObjectOfType<PlayerController>();
		foreach(var trait in AllTraits)
		{
			if (!UnlockedTraits.Contains(trait.GetType()))
				continue;
			
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
    }

	public void ApplyTraits(PlayerController player)
	{
		foreach(var trait in AllTraits)
		{
			if (!UnlockedTraits.Contains(trait.GetType()))
				continue;
			
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
