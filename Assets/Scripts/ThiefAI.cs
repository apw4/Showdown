using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class ThiefAI : MonoBehaviour {
    public GameObject bullet;
    public Transform target;
    public Animator anim;
    bool facingRight = true;
    public Transform shootPoint;
    public float distance;
    public float bowRange;
    public bool attacking;
    private Vector2 direction;

    //Bullet Info
    public float shootInterval;
    public float bulletSpeed = 100;
    public float bulletTimer;

    //How many times a second we update the path
    public float updateRate = 2f;

    //Health
    public int curHealth;
    public int maxHealth;

    [HideInInspector]
    public bool pathIsEnded = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    //The calculated path
    public Path path;

    //AI Speed per sec
    public float speed = 7f;
    public ForceMode2D fMode;

    //Max distance between AI and a waypoint for it to continue
    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    // Use this for initialization
    void Start () {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        curHealth = maxHealth;
        attacking = false;

        if (target == null)
        {
            Debug.LogError("No Player?");
            return;
        }

        seeker.StartPath (transform.position, target.position, OnPathComplete);
        StartCoroutine (UpdatePath());
	}

	IEnumerator UpdatePath() {
        if (target == null)
        {
            yield return false;
        }


        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds ( 1f/updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("We got a path. Any errors?" + p.error);
        if (!p.error)
        {
            path = p;

            currentWaypoint = 0;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void FixedUpdate() {
        if ((target.transform.position.x > transform.position.x) && (!facingRight))
        {
            Flip();
        }

        if ((target.transform.position.x < transform.position.x) && (facingRight))
        {
            Flip();
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (curHealth <= 0)
        {
            anim.SetBool("Dying", true);
            Destroy(gameObject);
        }


        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;

        rb.AddForce(dir, fMode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        //Move the dude
        if (!attacking) { 
             if (dist < nextWaypointDistance) {
                currentWaypoint++;
                return;
            }
        }

        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        distance = Vector3.Distance(transform.position, target.transform.position);

        anim.SetBool("Attacking", attacking);

    }


    public void Attack()
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer >= shootInterval)
        {

            if (distance < bowRange)
            {
                attacking = true;
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
            attacking = false;
        }
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("samRedFlash");
    }
}
