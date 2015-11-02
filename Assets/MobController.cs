using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour {

    public float maxSpeed = 3f;
    public float visionDistance = 25f;

    private CharacterState player;
    private Rigidbody rigidbody;
    private Animator animator;
    private CharacterState self;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterState>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        self = GetComponent<CharacterState>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!self.IsDead)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < visionDistance)
                MoveTowards(player.transform.position);
            animator.SetFloat("HSpeed", Mathf.Abs(rigidbody.velocity.x));
        }
    }

    void MoveTowards(Vector3 target)
    {
        float xVelocity;

        xVelocity = maxSpeed * (target.x - transform.position.x < 0 ? -1 : Mathf.Abs(target.x - transform.position.x) < 1 ? 0 : 1);
        if (target.y > transform.position.y)
            Jump();
        rigidbody.velocity = new Vector3(xVelocity, rigidbody.velocity.y, rigidbody.velocity.z);
        if (xVelocity != 0)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Abs(transform.localScale.z) * (xVelocity > 0 ? -1 : 1));
    }

    void Jump()
    {

    }
}
