using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurTrait : Trait, IStatTrait
{
    public override string Name
    {
        get { return "Fur Trait"; }
    }
    public override int Cost
    {
        get { return 3; }
    }
    public override Environment Environment
    {
        get { return Environment.Global; }
    }

    public void ApplyTrait(PlayerController player)
    {
        player.MaxStamina += 5;
    }
}
