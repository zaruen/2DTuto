using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool faceRight = true;
	private Rigidbody2D rb2D;

	Animator anim;

    //Ground stuff
    private bool grounded = false;
    public Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float jumpForce = 700f;

    private bool doubleJump = false;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rb2D.velocity.y);

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

    //Update read the input more accurately than Fixed update
    void Update()
    {
        // don't do Input.GetKey, that's a bad practice
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rb2D.AddForce(new Vector2(0, jumpForce));

            if(!doubleJump && !grounded)
            	doubleJump = true;
        }
    }

	void Flip(){
		faceRight = !faceRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
