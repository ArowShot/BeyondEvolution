using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrait : Trait
{
    public override string Name
    {
        get { return "Poison Trait"; }
    }
    public override int Cost
    {
        get { return 3; }
    }
    public override Environment Environment
    {
        get { return Environment.Desert; }
    }
}
