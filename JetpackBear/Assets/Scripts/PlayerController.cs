using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour {
	public float flyForce;
	public float moveForce;

	public float invulnerabilityTime;

	public LayerMask groundLayer;
	public Vector2 groundCheckSize;

	private bool holdFly;
	private bool invulnerable;
	private float axisHorizontal;
	private float accelerationX;

	private Rigidbody2D rb2d;
	private Collider2D coll;

	[HideInInspector]
	public PlayerShield shieldController;
	[HideInInspector]
	public Magnet magnetController;
	
	private Animator burstAnim1;
	private Animator burstAnim2;
	private ParticleSystem[] smokes;

	private AudioSource audioSource;
	
	public PlayableDirector AnimationLoseBee { get; private set; }
	public PlayableDirector AnimationLoseExplosion { get; private set; }
	public PlayableDirector AnimationWin { get; private set; }
	public Animator Anim { get; private set; }

	void Start () 
	{		
		rb2d = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		Anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		shieldController = GetComponentInChildren<PlayerShield>();
		magnetController = GetComponentInChildren<Magnet>();	

		AnimationLoseBee = transform.Find("LoseAnimation_Bee").GetComponent<PlayableDirector>();
		AnimationLoseExplosion = transform.Find("LoseAnimation_Explosion").GetComponent<PlayableDirector>();		
		AnimationWin = transform.Find("WinAnimation").GetComponent<PlayableDirector>();		

		Transform jetpack = transform.Find("Jetpack");
		burstAnim1 = jetpack.Find("JetpackBurst1").GetComponent<Animator>();
		burstAnim2 = jetpack.Find("JetpackBurst2").GetComponent<Animator>();

		smokes = jetpack.GetComponentsInChildren<ParticleSystem>();
		
		// if(DataManager.Shield)
		shieldController.Show();

		// if(DataManager.MagnetCount > 0)
		magnetController.Show();
	}

	void FixedUpdate()
	{
		if(holdFly)
			rb2d.AddForce(Vector2.up * flyForce);

		if(axisHorizontal != 0)
			rb2d.AddForce(new Vector2(axisHorizontal * moveForce, 0));	

		if(accelerationX != 0)
			rb2d.AddForce(new Vector2(accelerationX * (moveForce*2), 0));	


		Anim.SetFloat("velocityX", rb2d.velocity.x);
		Anim.SetFloat("velocityY", rb2d.velocity.y);
	}

	void Update () {
		holdFly = Input.GetButton("Fly") || Input.GetAxisRaw("Vertical") == 1 || Input.GetMouseButton(0);
		axisHorizontal = Input.GetAxis("Horizontal");
		accelerationX = Input.acceleration.x;

		TurnJetpack(holdFly);

		Vector3 scale = transform.localScale;
		if(rb2d.velocity.normalized.x > 0)
			scale.x = 1;
		else if(rb2d.velocity.normalized.x < 0)
			scale.x = -1;

		transform.localScale = scale;
		transform.rotation = Quaternion.Euler(0, 0, -(rb2d.velocity.x*3));


		audioSource.mute = !holdFly || !enabled;

		if(Input.GetButtonDown("Cancel"))
		{
			if(StageManager.Instance.IsPaused)
				StageManager.Instance.Resume();
			else
				StageManager.Instance.Pause();
		}

		

		Vector2 center = new Vector2(transform.position.x, coll.bounds.min.y);
		RaycastHit2D grounded = Physics2D.BoxCast(center, groundCheckSize, 0, Vector2.down, 0, groundLayer);
		Anim.SetBool("flying", !grounded);
	}

	void OnDisable()
	{
		if(gameObject.activeSelf)
		{
			TurnJetpack(false);

			Anim.SetBool("none", true);
			audioSource.Stop();
		}
	}

	private IEnumerator InvulnerableCoroutine()
	{
		invulnerable = true;

		yield return new WaitForSeconds(invulnerabilityTime);

		invulnerable = false;
	}

	void OnDrawGizmos()
	{
		coll = GetComponent<Collider2D>();
		Gizmos.color = Color.red;

		Vector2 center = new Vector2(transform.position.x, coll.bounds.min.y);
		Gizmos.DrawWireCube(center, groundCheckSize);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(!enabled) return;
			
		if(other.CompareTag("Hazard"))
		{
			Hazard hazard = other.transform.GetComponent<Hazard>();
			
			if(hazard.type == Hazard.HazardType.Flytrap)
			{
				// Desativa o jogador
				enabled = false;
				
				// Chama a rotina para exibir a tela de derrota
				StageManager.Instance.Lose();

				// Animação sendo comido pela planta
				FlytrapPlant plant = other.transform.parent.GetComponent<FlytrapPlant>();
				rb2d.velocity = Vector2.zero;
				rb2d.isKinematic = true;
				plant.SwallowBear(this);
			}
			else if(!invulnerable)
			{
				// Toca o som de receber dano
				SoundEffects.Instance.Play(SoundEffects.Instance.sfxHit);

				if(shieldController.powerUp.Enabled)
				{
					shieldController.Break();
					StartCoroutine(InvulnerableCoroutine());
				}
				else
				{
					// Desativa o jogador
					enabled = false;
					
					// Chama a rotina para exibir a tela de derrota
					StageManager.Instance.Lose();

					// Exibe a animação para cada tipo de ameaça
					if(hazard.type == Hazard.HazardType.Spike)
						AnimationLoseExplosion.Play();
					else if(hazard.type == Hazard.HazardType.Bee)
						AnimationLoseBee.Play();
				}
			}
		}

		if(other.CompareTag("Hive"))
		{
			SoundEffects.Instance.Play(SoundEffects.Instance.sfxPickup);
			StageManager.Instance.AddHive();

			Destroy(other.gameObject);
		}

		if(other.CompareTag("Coin"))
		{
			SoundEffects.Instance.Play(SoundEffects.Instance.sfxCoin);
			StageManager.Instance.AddCoin();

			Destroy(other.gameObject);
		}

		if(other.CompareTag("Finish"))
		{
			transform.rotation = Quaternion.identity;
			enabled = false;

			AnimationWin.Play();

			SoundEffects.Instance.Play(SoundEffects.Instance.sfxWin);

			StartCoroutine(StageManager.Instance.WinCoroutine());
		}
	}

	private void TurnJetpack(bool isOn)
	{
		
		burstAnim1.SetBool("on", isOn);
		burstAnim2.SetBool("on", isOn);

		if(isOn && !smokes[0].isEmitting)
		{
			smokes[0].Play();
			smokes[1].Play();
		}
		else if(!isOn && smokes[0].isPlaying)
		{
			smokes[0].Stop();
			smokes[1].Stop();
		}
	}
}
