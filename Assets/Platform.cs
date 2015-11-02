using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    static CapsuleCollider playerCollider = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") ||
            other.gameObject.layer == LayerMask.NameToLayer("PlayerFalling"))
        {
            if (playerCollider == null)
                playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>();
            Physics.IgnoreCollision(playerCollider, transform.parent.GetComponentInParent<BoxCollider>(), true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") ||
            other.gameObject.layer == LayerMask.NameToLayer("PlayerFalling"))
        {
            if (playerCollider == null)
                playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>();
            Physics.IgnoreCollision(playerCollider, transform.parent.GetComponentInParent<BoxCollider>(), false);
        }
    }
}
