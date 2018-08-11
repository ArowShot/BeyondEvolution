using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitsUpdater : MonoBehaviour
{
	private TraitManager _tm;

	public Text Trait1;
	public Text Trait2;
	public Text Trait3;
	public Text Trait4;
	
	private void Start ()
	{
		_tm = FindObjectOfType<TraitManager>().Instance;
	}
	
	private void Update ()
	{
		Trait1.text = _tm.ActiveTraits[0] == null ? "?" : ((Trait) _tm.ActiveTraits[0]).Name;
		Trait2.text = _tm.ActiveTraits[1] == null ? "?" : ((Trait) _tm.ActiveTraits[1]).Name;
		Trait3.text = _tm.ActiveTraits[2] == null ? "?" : ((Trait) _tm.ActiveTraits[2]).Name;
		Trait4.text = _tm.ActiveTraits[3] == null ? "?" : ((Trait) _tm.ActiveTraits[3]).Name;
	}
}
