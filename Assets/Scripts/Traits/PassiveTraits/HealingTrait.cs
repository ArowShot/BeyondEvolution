using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTrait : Trait, IPassiveTrait
{
    public override string Name
    {
        get { return "Healing Trait"; }
    }
    public override int Cost
    {
        get { return 2; }
    }
    public override Environment Environment
    {
        get { return Environment.Tundra; }
    }

    public void Update(PlayerController player)
    {
        HealPlayer(player);
    }

    private void HealPlayer(PlayerController player)
    {
        if (player == null)
        {
            return;
        }
        if (player.Health < player.MaxHealth)
        {
            player.Health += (1 * Time.deltaTime);
        }
    }
}
