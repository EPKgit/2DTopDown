using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
	//set true if you want the ability's Tick function and FinishAbility functions to get called
	public bool instantEffect;
	public float cost;

	//returns true if the ability is used succesfully
	//parental default just checks if the cost is payable, children can have more requirements
    public virtual bool AttemptUseAbility(PlayerAbilities pa)
	{
		UseAbility(pa);
		return true;
	}

	//actual effect of the ability
	protected virtual void UseAbility(PlayerAbilities pa)
	{

	}

	//returns true if the ability wants to end
	//gets called if the ability has an ongoing effect, over multiple frames
	protected virtual bool Tick(float deltaTime, PlayerAbilities pa)
	{
		return false;
	}

	//gets called when the abilitys tick returns true and the ability is finished
	protected virtual void FinishAbility(PlayerAbilities pa)
	{

	}
}
