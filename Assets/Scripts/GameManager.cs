using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static  GameManager Instance { get; private set; }
	private static TraitManager _tm = TraitManager.Instance;
	
	private void Awake()
	{
		DontDestroyOnLoad(this);
		if (Instance == null)
		{
			Instance = this;
		} else
		{
			Destroy(gameObject);
		}
	}
	
	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}
	
	public void OnDead()
	{
		SceneManager.LoadScene("TraitsScene");
	}
}
