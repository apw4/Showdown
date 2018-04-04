using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //Floats
    public float maxSpeed = 10f;
    public float jumpPower = 30f;
    public float timer = 60;

    public Text coincountText;
    public Text timesup;
    bool facingRight = true;
    
    //Booleans
    public bool canDoubleJump;
    public bool grounded;

    //Stats
    public int curHealth;
    public int maxHealth = 5;

    //References
    private Rigidbody2D rb2d;
    Animator anim;
    private int coincount;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        curHealth = maxHealth;


        coincount = 0;
        coincountText.text = "Score: " + coincount.ToString ();
    }

    void Update()
    {
        anim.SetBool("Grounded",grounded);

        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            Die();
        }
    }
    // Update is called once per frame
    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        
        //Jump Controls
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")))
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower);
                }
            }
        }

        rb2d.velocity = new Vector2(moveHorizontal * maxSpeed, rb2d.velocity.y);

        timer -= Time.deltaTime;
        timesup.text = ((int)timer).ToString();
        if (timer <0)
        {
            Time.timeScale = 0;
            timesup.text = "Time's up!";

        }
           
        if (moveHorizontal > 0.1f && !facingRight)
            Flip();
        else if (moveHorizontal < -0.1f && facingRight)
            Flip();

        //Inertia and Friction Controls
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }
	}

    void Flip (){
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

    void Die()
    {
        //Restart
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("samRedFlash");
    }

    public IEnumerator Knockback(float knockDur, float knockPwr, Vector3 knockDir)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;

            rb2d.velocity = new Vector2(0, 0);

            rb2d.AddForce(new Vector3(knockDir.x * -1000, knockDir.y * knockPwr, transform.position.z));
        }

        yield return 0;
    }


}
