using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("collision");
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("trigger");
	}
}
