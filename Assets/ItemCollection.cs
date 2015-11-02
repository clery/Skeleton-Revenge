using UnityEngine;
using System.Collections;

public class ItemCollection : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Coin"))
            Destroy(other.gameObject);
    }
}
