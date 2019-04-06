using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Poolable : MonoBehaviour
{
    public GameObject prefab;

	/// <summary>
	/// Called when the poolable object is first created by the pool manager, get references that
	/// will be used multiple times over the objects lifetime
	/// </summary>
	public virtual void PoolInit(GameObject g)
	{
		prefab = g;
	}

	/// <summary>
	/// Should be called when the poolable object is fetched from the PoolManager. Implemented
	/// to reset the object back to it's prefab state.
	/// </summary>
	public abstract void Reset();

	/// <summary>
	/// Call instead of destroy on poolable objects, this will return the object to the pool to be reused.
	/// </summary>
	protected virtual void DestroySelf()
	{
		PoolManager.instance.ReturnObject(prefab, gameObject);
	}
}
