using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public float speed;

	public Bounds boundary;

	private Bounds camBounds;

	// Use this for initialization
	void Start () {
		camBounds = Camera.main.OrthographicBounds();
	}
	
	void FixedUpdate()
	{
		if(target != null)	
		{
			transform.position = Vector2.Lerp(transform.position, target.transform.position, Time.fixedDeltaTime * speed);

			// Clamp to boundary
			transform.position = new Vector2(
				Mathf.Clamp(transform.position.x, (boundary.min.x + camBounds.extents.x), (boundary.max.x - camBounds.extents.x)),
				Mathf.Clamp(transform.position.y, (boundary.min.y + camBounds.extents.y), (boundary.max.y - camBounds.extents.y))
			);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;

		Gizmos.DrawWireCube(boundary.center, boundary.size);
	}
}
