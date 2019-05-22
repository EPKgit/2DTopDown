using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class Laser : Ability
{
	public GameObject laserPrefab;
	public float damage;

	private bool started;
    private GameObject temp;

	public override void Reinitialize()
	{
		started = false;
		base.Reinitialize();
	}

	public override void Initialize(PlayerAbilities pa)
	{
		PoolManager.instance.AddPoolSize(laserPrefab, 1, true);
		base.Initialize(pa);
	}

	public override bool AttemptUseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		if(DEBUGFLAGS.ABILITY) Debug.Log(string.Format("{0} ATTEMPT USE perf:{1} strt:{2} canc:{3}", name, ctx.performed, ctx.started, ctx.cancelled));
		if(!IsCastable())
		{
			return false;
		}
		if(!started) //the first press of the button
		{
			started = true;
            temp = PoolManager.instance.RequestObject(laserPrefab);

            BaseLaser laser = temp.GetComponent<BaseLaser>();
            // temp.transform.localScale = new Vector3(size, size, size);
            laser.Setup
            (
                playerAbilities.transform.position, inputDirection, playerAbilities.gameObject, laser.totalLength
            );
            temp.GetComponent<Poolable>().Reset();
            return true;
		}
		started = false;
		UseAbility(ctx, inputDirection);
		return false;
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
        GameObject.Destroy(temp);
	}

	public override void FinishAbility()
	{
		if(!started)
		{
			base.FinishAbility();
		}
	}
}
