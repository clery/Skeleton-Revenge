using UnityEngine;
using System.Collections.Generic;

public class SkillTemplate : MonoBehaviour {

    public List<Collider> colliders = new List<Collider>();

    void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other) && (other.tag == "Breakable" || other.tag == "Monster"))
            colliders.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (colliders.Contains(other))
            colliders.Remove(other);
    }
}
