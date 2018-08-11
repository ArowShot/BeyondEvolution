using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CLickAnywehere : MonoBehaviour {
	public void doClick()
	{
		SceneManager.LoadScene("TraitsScene");
	}
}
