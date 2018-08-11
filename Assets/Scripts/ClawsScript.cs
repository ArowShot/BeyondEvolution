using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawsScript : MonoBehaviour {
	
	private TraitManager _tm = TraitManager.Instance;

	public void AnimationComplete()
	{
		transform.parent.gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		var enemy = other.gameObject.GetComponent<EnemyController>();
		if (enemy == null)
			return;
		enemy.Health -= 5;

		if (_tm.UnlockedTraits.Contains(typeof(PoisonTrait)))
		{
			enemy.isPoisoned = true;
		}
	}
}
