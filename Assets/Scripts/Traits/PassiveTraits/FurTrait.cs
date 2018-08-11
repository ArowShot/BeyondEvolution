using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurTrait : Trait
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
}
