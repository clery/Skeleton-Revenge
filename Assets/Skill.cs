using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour {

    public int id;
    public int damages;
    public float cooldown;

    public abstract void Launch();
}
