using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlant : MonoBehaviour 
{
	public float shootInterval;
	public float range;
	public GameObject projectilePrefab;

	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();

		StartCoroutine("ShootCoroutine");
	}

	private IEnumerator ShootCoroutine()
	{
		anim.SetTrigger("shoot");

		yield return new WaitForSeconds(shootInterval);

		StartCoroutine("ShootCoroutine");
	}

	public void SpawnProjectile()
	{
		GameObject projectile = Instantiate(projectilePrefab);
		
		projectile.transform.localScale = new Vector2(transform.localScale.x, projectile.transform.localScale.y);
		projectile.transform.position = new Vector2(transform.position.x + transform.localScale.x, transform.position.y);

		ShootingPlantProjectile projectileScript = projectile.GetComponent<ShootingPlantProjectile>();
		projectileScript.direction = new Vector2(transform.localScale.x, 0);
		projectileScript.maxRange = range;

		projectile.SetActive(true);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + range*transform.localScale.x, transform.position.y));
	}
}