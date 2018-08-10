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
        get { return 1; }
    }
    public override Environment Environment
    {
        get { return Environment.Global; }
    }  

    public void ApplyTrait(PlayerController player)
    {
        player.speed += 2;
    }
}
