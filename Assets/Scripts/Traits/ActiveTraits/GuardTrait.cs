using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTrait : Trait, IActiveTrait
{
    public int StaminaRequired
    {
        get { return 2; }
    }
    public override string Name
    {
        get { return "Guard Trait"; }
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
        Guard();
    }
    private void Guard()
    {
        Debug.Log("Guard stuff here.");
    }
}