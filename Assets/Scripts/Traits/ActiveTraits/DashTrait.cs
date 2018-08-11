using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrait : Trait, IActiveTrait
{
    public int StaminaRequired
    {
        get { return 2; }
    }
    public override string Name
    {
        get { return "Dash Trait"; }
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
        DashMove();
    }
    private void DashMove()
    {
        Debug.Log("Dash move here.");
    }
}