using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool faceRight = true;
	private Rigidbody2D rb2D;

	Animator anim;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs(move));

		rb2D.velocity = new Vector2 (move * maxSpeed, rb2D.velocity.y);
//		rb2D.AddForce(new Vector2 (move * maxSpeed, rb2D.velocity.y));

		if (move > 0 && !faceRight) {
			Flip ();
		}
		else if(move < 0 && faceRight){
			Flip();
		}

	}

	void Flip(){
		faceRight = !faceRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
