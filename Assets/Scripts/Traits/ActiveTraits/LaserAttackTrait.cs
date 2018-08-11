using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackTrait : Trait, IActiveTrait {
    public int StaminaRequired
    {
        get { return 1; }
    }
    public override string Name
    {
        get { return "Lasers Trait"; }
    }
    public override int Cost
    {
        get { return 3; }
    }
    public override Environment Environment
    {
        get { return Environment.Desert; }
    }

    public void DoActive(PlayerController player)
    { 
        ShootLasers();
    }

    private void ShootLasers()
    {
        Debug.Log("Lasers go here");
    }
}
