using UnityEngine;
using System.Collections;

public class DungeonState {

    static private DungeonState _instance = null;
    static public DungeonState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DungeonState();
            return (_instance);
        }
    }

    public GameObject character = null;
    public int currentFloor = 0;
}
