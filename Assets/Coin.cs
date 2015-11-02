using UnityEngine;
using System.Collections.Generic;

public class Coin : MonoBehaviour {

    static CapsuleCollider playerCollider = null;
    Rigidbody rb;
    public List<AudioClip> coinSounds;

	// Use this for initialization
	void Start () {
        if (playerCollider == null)
            playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ItemCollection>().GetComponent<CapsuleCollider>();
        foreach (BoxCollider collider in GetComponents<BoxCollider>())
            if (collider.isTrigger == false)
                Physics.IgnoreCollision(collider, playerCollider, true);
        rb = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().clip = coinSounds[Random.Range(0, coinSounds.Count - 1)];
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (rb.velocity.y == 0)
            rb.velocity = new Vector3(0, 0, 0);
    }
}
