using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Environment
{
	Global,
	Forest,
	Tundra,
	Desert
}

public abstract class Trait
{
	public abstract string Name { get; }
	public abstract int Cost { get; }
	public abstract Environment Environment { get; }
	public abstract void ApplyTrait(PlayerController player);
}
