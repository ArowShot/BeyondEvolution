using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsTrait : Trait
{
    public override string Name
    {
        get { return "Thorns Trait"; }
    }
    public override int Cost
    {
        get { return 2; }
    }
    public override Environment Environment
    {
        get { return Environment.Global; }
    }
}
