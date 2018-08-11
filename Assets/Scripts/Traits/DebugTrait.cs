using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTrait : Trait, IActiveTrait
{
    public int StaminaRequired 
    {
        get { return 2; }
    }
    public override string Name
    {
        get { return "Test Trait"; }
    }
    public override int Cost
    {
        get { return 2; }
    }
    public override Environment Environment
    {
        get { return Environment.Desert; }
    }

    public void DoActive(PlayerController player)
    {
        Debug.Log("Debug thing works.");
    }
}
