using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrait : Trait, IActiveTrait {
    public int StaminaRequired
    {
        get { return 3; }
    }
    public override string Name
    {
        get { return "Bomb Trait"; }
    }
    public override int Cost
    {
        get { return 2; }
    }
    public override Environment Environment
    {
        get { return Environment.Tundra; }
    }

    public void DoActive(PlayerController player)
    {
        ThrowBomb();
    }
    private void ThrowBomb()
    {
        Debug.Log("Throw bombs here.");
    }
}
