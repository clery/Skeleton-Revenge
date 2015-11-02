using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    void Start()
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.tag == "Player")
            if (Input.GetButtonDown("Interact"))
            {
                GetComponent<AudioSource>().Play();
                DungeonState.Instance.currentFloor++;
                Application.LoadLevel("Dungeon");
            }
    }
}
