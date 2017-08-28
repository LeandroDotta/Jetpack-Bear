using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public float speed;

	private float initPos;
	private Transform cam;
	private Vector2 previousCamPos;
	private SpriteRenderer spriteRenderer;

	private float Width { 
		get
		{
			return spriteRenderer.bounds.size.x;
		} 
	}
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		initPos = transform.localPosition.x;
		cam = Camera.main.transform;
		previousCamPos = cam.position;

		// // Cria uma cópia do sprite para garantir que ele vai estar sempre visivel na tela	
		// GameObject objClone = GameObject.Instantiate(this.gameObject);
		// Destroy(objClone.GetComponent<Parallax>());

		// objClone.transform.SetParent(this.transform);
		// objClone.transform.localPosition = new Vector2(Width, 0);
	}

	void Update () 
	{
		Vector2 direction = new Vector2(cam.position.x - previousCamPos.x, 0);
		transform.Translate(direction * speed * Time.deltaTime);


		// if(direction.x > 0)
		// {
		// 	//shift right if player is moving right
		// 	if(initPos - transform.localPosition.x > Width)
		// 		transform.Translate(new Vector2(Width, 0));
		// }
		// else
		// {
		// 	//shift left if player moving left
		// 	if(initPos - transform.localPosition.x < 0)
		// 		transform.Translate(new Vector2(-Width, 0));
		// }

		previousCamPos = cam.position;
	}
}
