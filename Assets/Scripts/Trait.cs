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

public interface IStatTrait
{
	void ApplyTrait(PlayerController player);
}

public interface IActiveTrait
{
	void DoActive(PlayerController player);
}

public interface IPassiveTrait
{
	void Update(PlayerController player);
}

public abstract class Trait
{
	public abstract string Name { get; }
	public abstract int Cost { get; }
	public abstract Environment Environment { get; }
}
