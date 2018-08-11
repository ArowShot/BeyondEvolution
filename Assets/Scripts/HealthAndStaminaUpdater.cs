using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.Experimental.UIElements;
using UnityEngine.UI;

public class HealthAndStaminaUpdater : MonoBehaviour
{
	public PlayerController Player;
	public Text HealthText;
	public RectTransform HealthBar;
	public Text StaminaText;
	public RectTransform StaminaBar;

	private float _healthMax;
	private float _staminaMax;
	
	private void Start ()
	{
		_healthMax = HealthBar.rect.width;
		_staminaMax = StaminaBar.rect.width;
	}
	
	private void Update () {
		HealthText.text = "Health " + Player.Health.ToString("F0") + "/" + Player.MaxHealth;
		HealthBar.sizeDelta = new Vector2(Player.Health / Player.MaxHealth * _healthMax - _healthMax, 0);
		StaminaText.text = "Stamina " + Player.Stamina + "/" + Player.MaxStamina;
		StaminaBar.sizeDelta = new Vector2(Player.Stamina / Player.MaxStamina * _staminaMax - _staminaMax, 0);
	}
}
