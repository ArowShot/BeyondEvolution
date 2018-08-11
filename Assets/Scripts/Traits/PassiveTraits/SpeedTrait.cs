using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTrait : Trait, IStatTrait
{
    public override string Name
    {
        get { return "Speed Trait"; }
    }
    public override int Cost
    {
        get { return 2; }
    }
    public override Environment Environment
    {
        get { return Environment.Tundra; }
    }  

    public void ApplyTrait(PlayerController player)
    {
        ApplySpeed(player);
    }

    private void ApplySpeed(PlayerController player)
    {
        player.Speed *= 1.3f;
    }
}
