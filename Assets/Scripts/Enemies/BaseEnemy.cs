using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
	public float turnSpeed = 0.4f;

	private GameObject chosenPlayer;
	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		chosenPlayer = PlayerInput.all[Random.Range(0, PlayerInput.all.Count)].gameObject;
	}
    void Update()
	{
		Vector2 dir = (chosenPlayer.transform.position - transform.position).normalized;
		rb.velocity = dir;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(Mathf.Rad2Deg * -Mathf.Atan2(dir.x, dir.y), Vector3.forward), turnSpeed);
		//transform.rota
	}
}
