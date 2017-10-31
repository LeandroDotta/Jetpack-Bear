using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFollow : BeeObject {

	public LayerMask targetLayer;
	public float range;

	protected override void Start()
	{
		base.Start();
	}

	void Update()
	{
		RaycastHit2D hit = Physics2D.CircleCast(transform.position, range, Vector2.zero, 0, targetLayer);
		
		if(hit)
		{
			Vector2 direction = (hit.collider.transform.position - transform.position).normalized;
			transform.Translate(direction * speed * Time.deltaTime);

			if(direction.x != 0)
            {
				Vector2 scale = transform.localScale;
				scale.x = Mathf.Sign(direction.x);
				transform.localScale = scale;
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
