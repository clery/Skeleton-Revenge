using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

    [System.Serializable]
    public class Exit
    {
        public string key;
        public bool state;
        public GameObject exit;
        public bool forceState = false;
    }

    public List<Exit> exits;

	// Use this for initialization
	void Start () {
	}
	
    public void Init()
    {
        foreach (ReCalcCubeTexture rcct in GetComponentsInChildren<ReCalcCubeTexture>())
            rcct.reCalcCubeTexture();
        if (exits.Find(x => x.key == "Top").forceState == false)
            exits.Find(x => x.key == "Top").state = (Random.Range(0, 10) < 5 ? false : true);
        if (exits.Find(x => x.key == "Bottom").forceState == false)
            exits.Find(x => x.key == "Bottom").state = (Random.Range(0, 10) < 5 ? false : true);
        if (exits.Find(x => x.key == "Left").forceState == false)
            exits.Find(x => x.key == "Left").state = (Random.Range(0, 10) < 5 ? false : true);
        if (exits.Find(x => x.key == "Right").forceState == false)
            exits.Find(x => x.key == "Right").state = (Random.Range(0, 10) < 5 ? false : true);
        foreach (Exit e in exits)
            if (e.state == true && e.exit != null)
                e.exit.SetActive(false);
    }

    public bool HasExit(string key)
    {
        return (exits.Find(x => x.key == key).state);
    }

    public void SetExit(string key, bool value)
    {
        exits.Find(x => x.key == key).state = value;
    }
}
