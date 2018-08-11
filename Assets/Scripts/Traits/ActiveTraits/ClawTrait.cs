using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawTrait : Trait, IActiveTrait
{
    public int StaminaRequired
    {
        get { return 1; }
    }
    public override string Name
    {
        get { return "Claw Trait"; }
    }
    public override int Cost
    {
        get { return 1; }
    }
    public override Environment Environment
    {
        get { return Environment.Desert; }
    }

    public void DoActive(PlayerController player)
    {
        ClawAttack();
    }
    private void ClawAttack()
    {
        Debug.Log("Claw Attack here.");
    }
}
