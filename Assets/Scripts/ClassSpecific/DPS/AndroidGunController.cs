using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidGunController : Poolable
{
	private GameObject bulletPrefab;
	private float damage;
	private float bulletSpeed;
	private float aggroRadius;
	private float maxDisableTime;
	private float shootingCooldown;
	private GameObject creator;
	
	private CircleCollider2D col;
	private Rigidbody2D rb;
	private List<GameObject> inside;
	private float shootTimer;
	private Vector3 offset;

    public override void PoolInit(GameObject g)
	{
		base.PoolInit(g);
		col = GetComponent<CircleCollider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
		if(shootTimer <= 0 && inside.Count > 0)
		{
			Shoot();
			shootTimer = shootingCooldown;
			return;
		}
		shootTimer -= Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position, creator.transform.position + offset, Time.deltaTime);
	}

	void Shoot()
	{
		GameObject g = inside[Random.Range(0, inside.Count)];
		while(g == null)
		{
			inside.Remove(g);
			if(inside.Count <= 0)
			{
				return;
			}
			g = inside[Random.Range(0, inside.Count)];
		}
		Vector2 dir = (g.transform.position - transform.position).normalized * bulletSpeed;
		GameObject temp = PoolManager.instance.RequestObject(bulletPrefab);
		temp.GetComponent<Bullet>().Setup(transform.position, dir, creator, damage);
		temp.GetComponent<Poolable>().Reset();
	}

	public void Setup(Vector3 startPos, float d, float s, float a, float t, float cd, GameObject c, Vector3 o, GameObject b)
	{
		transform.position = startPos;
		offset = o;
		damage = d;
		bulletSpeed = s;
		aggroRadius = a;
		maxDisableTime = t;
		shootingCooldown = cd;
		inside = new List<GameObject>();
		col.radius = a;
		creator = c;
		Lib.FindInHierarchy<BaseHealth>(c).postDamageEvent += OnTakeDamage;
		bulletPrefab = b;
	}

	public void OnTakeDamage(HealthChangeNotificationData hcnd)
	{
		if(hcnd.value == 0)
		{
			return;
		}
		this.enabled = false;
		StartCoroutine(Reenable());
	}

	IEnumerator Reenable()
	{
		yield return new WaitForSeconds(maxDisableTime);
		this.enabled = true;
	}

	public override void DestroySelf()
	{
		base.DestroySelf();
		Lib.FindInHierarchy<BaseHealth>(creator).postDamageEvent -= OnTakeDamage;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(Lib.HasTagInHierarchy(col.gameObject, "Enemy"))
		{
			if(!inside.Contains(col.gameObject))
			{
				inside.Add(col.gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		inside.Remove(col.gameObject);
	}
}
