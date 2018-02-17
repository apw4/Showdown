using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    public Text coincountText;
    public Text timesup;
    bool facingRight = true;
    private Rigidbody2D rb2d;
    public float timer = 60;
   
    Animator anim;
    private int coincount;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
        coincount = 0;
        coincountText.text = "Score: " + coincount.ToString ();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        rb2d.velocity = new Vector2(moveHorizontal * maxSpeed, rb2d.velocity.y);

        timer -= Time.deltaTime;
        timesup.text = ((int)timer).ToString();
        if (timer <0)
        {
            Time.timeScale = 0;
            timesup.text = "Time's up!";

        }
           
        if (moveHorizontal > 0 && !facingRight)
            Flip();
        else if (moveHorizontal < 0 && facingRight)
            Flip();
	}

    void Flip ()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickups"))
        {
            Destroy(collision.gameObject);
            coincount += 1;
            coincountText.text = "Score: " + (coincount*100).ToString();
        }
    }
}
