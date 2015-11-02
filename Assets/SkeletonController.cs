using UnityEngine;
using System.Collections.Generic;

public class SkeletonController : MonoBehaviour {

    public float speed = 5f;
    public float jumpForce = 5f;
    public int airJump = 1;
    public GameObject ToTurnWithCharacter = null;
    public LayerMask isGround;
    public List<AudioClip> footStepSound;
    public float timeBetweenFootStepSounds = 0.5f;
    private float timerFootStep = 0;
    private AudioSource walkPlayer = null;
    private int currentAirJump;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        if (ToTurnWithCharacter == null)
            Debug.LogWarning("Set a model for the character");
        currentAirJump = airJump;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("DeadMonster"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerFalling"), LayerMask.NameToLayer("DeadMonster"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Coin"), LayerMask.NameToLayer("Coin"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerFalling"), LayerMask.NameToLayer("Platform"), true);
        foreach (AudioSource source in GetComponentsInChildren<AudioSource>())
            if (source.clip != null && source.loop == false)
            {
                walkPlayer = source;
                break;
            }
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsGrounded())
            currentAirJump = airJump;
        if (Input.GetButtonDown("Jump"))
            Jump();
        if (rb.velocity.x != 0 && IsGrounded())
        {
            if (timerFootStep >= timeBetweenFootStepSounds)
            {
                walkPlayer.clip = footStepSound[Random.Range(0, footStepSound.Count - 1)];
                walkPlayer.Play();
                timerFootStep = 0;
            }
            timerFootStep += Time.deltaTime;
        }
        else
            timerFootStep = 0;
        Animate();
    }

	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");

        if (h != 0)
            ToTurnWithCharacter.transform.rotation = Quaternion.Euler(0, (h < 0 ? 180 : 0), 0);
        rb.velocity = new Vector3(h * speed, rb.velocity.y, 0);

        GetComponentInChildren<CapsuleCollider>().gameObject.layer = LayerMask.NameToLayer((Input.GetAxisRaw("Vertical") == -1 ? "PlayerFalling" : "Player"));
    }

    void Jump()
    {
        if (IsGrounded() || currentAirJump > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            if (!IsGrounded())
                --currentAirJump;
        }
    }

    // This function sucks
    bool IsGrounded()
    {
        return (Physics.Raycast(transform.position, -Vector3.up, 1f, ~LayerMask.NameToLayer("Ground")) ||
            Physics.Raycast(transform.position, -Vector3.up, 1f, ~LayerMask.NameToLayer("Platform")));
    }

    void Animate()
    {
        bool moving = (Mathf.Abs(rb.velocity.x) > 0);

        GetComponentInChildren<Animator>().SetFloat("HSpeed", Mathf.Abs(rb.velocity.x));
        GetComponentInChildren<Animator>().SetFloat("VSpeed", rb.velocity.y);
        GetComponentInChildren<Animator>().SetBool("Moving", (moving ? true : false));
        GetComponentInChildren<Animator>().SetBool("Running", (moving ? true : false));
        //GetComponentInChildren<Animator>().SetBool("InAir", (IsGrounded() ? false : true));
    }
}
