using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour {

    public GameObject Character = null;

    public GameObject Entrance = null;
    public List<GameObject> rooms = null;
    public float DefaultRoomWidth = 100f;
    public float DefaultRoomHeight = 50f;
    private Room[][] dungeon;

	// Use this for initialization
	void Start () {
        dungeon = new Room[10][];
        int i = 0;
        foreach (Room[] g in dungeon)
        {
            dungeon[i] = new Room[10];
            ++i;
        }
        if (rooms == null || Entrance == null)
            Debug.LogWarning("Please, set rooms for dungeon generation");
        else if (Character == null)
            Debug.LogWarning("Please, set character for dungeon generation");
        else
        {
            GenerateRoom(0, dungeon[0].Length / 2, null, true);
            for (int j = 1000; j > 0; --j)
            {
                Room tmp = dungeon[Random.Range(0, dungeon.Length - 1)][Random.Range(0, dungeon[0].Length - 1)];
                if (tmp && tmp.GetComponentInChildren<Door>())
                {
                    tmp.GetComponentInChildren<Door>().enabled = true;
                    tmp.GetComponentInChildren<Door>().GetComponent<BoxCollider>().enabled = true;
                    tmp.GetComponentInChildren<Door>().GetComponent<MeshRenderer>().enabled = true;
                    break;
                }
                if (j == 1)
                    Debug.Log("No end level door set");
            }
            if (DungeonState.Instance.character == null)
            {
                DungeonState.Instance.character = (GameObject)Instantiate(Character, dungeon[0][dungeon[0].Length / 2].transform.position, Quaternion.Euler(Vector3.zero));
                DontDestroyOnLoad(DungeonState.Instance.character);
            }
            else
                DungeonState.Instance.character.transform.position = dungeon[0][dungeon[0].Length / 2].transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void GenerateRoom(int x, int y, string prevDir, bool entrance = false)
    {
        Vector3 position;
        GameObject room;
        List<GameObject> possibleRooms;

        if (x < dungeon.Length && y < dungeon[0].Length &&
            x >= 0 && y >= 0 && dungeon[x][y] == null)
        {
            // Just setting variables to place the chunk
            position.x = x * DefaultRoomWidth;
            position.y = y * DefaultRoomHeight;
            position.z = 0;

            // Checking all rooms that match our previous room
            possibleRooms = rooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == prevDir && ((b.state && b.forceState) || (!b.forceState))));

            bool state;

            if (x == 0)
                possibleRooms = possibleRooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == "Left" && ((!b.state && b.forceState) || (!b.forceState))));
            else if (x == dungeon.Length - 1)
                possibleRooms = possibleRooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == "Right" && ((!b.state && b.forceState) || (!b.forceState))));
            else if (dungeon[x + 1][y])
            {
                state = (dungeon[x + 1][y] && dungeon[x + 1][y].exits.Find(a => a.key == "Left").state);
                possibleRooms = possibleRooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == "Right" && ((b.state == state && b.forceState) || (!b.forceState))));
            }
            else if (dungeon[x - 1][y])
            {
                state = (dungeon[x - 1][y] && dungeon[x - 1][y].exits.Find(a => a.key == "Right").state);
                possibleRooms = possibleRooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == "Left" && ((b.state == state && b.forceState) || (!b.forceState))));
            }
            if (y == 0)
                possibleRooms = possibleRooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == "Bottom" && ((!b.state && b.forceState) || (!b.forceState))));
            else if (y == dungeon[0].Length - 1)
                possibleRooms = possibleRooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == "Top" && ((!b.state && b.forceState) || (!b.forceState))));
            else if (dungeon[x][y - 1])
            {
                state = (dungeon[x][y - 1] && dungeon[x][y - 1].exits.Find(a => a.key == "Top").state);
                possibleRooms = possibleRooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == "Bottom" && ((b.state == state && b.forceState) || (!b.forceState))));
            }
            else if (dungeon[x][y + 1])
            {
                state = (dungeon[x][y + 1] && dungeon[x][y + 1].exits.Find(a => a.key == "Bottom").state);
                possibleRooms = possibleRooms.FindAll(a => a.GetComponent<Room>().exits.Exists(b => b.key == "Top" && ((b.state == state && b.forceState) || (!b.forceState))));
            }

            if (!entrance)
                    room = (GameObject)Instantiate(possibleRooms[Random.Range(0, possibleRooms.Count)], position, Quaternion.Euler(Vector3.zero));
                else
                    room = (GameObject)Instantiate(Entrance, position, Quaternion.Euler(Vector3.zero));

            if (prevDir != null)
            {
                room.GetComponent<Room>().exits.Find(a => a.key == prevDir).state = true;
                room.GetComponent<Room>().exits.Find(a => a.key == prevDir).forceState = true;
            }

            Room.Exit exit = room.GetComponent<Room>().exits.Find(a => a.key == "Left");
            bool roomExists = x > 0 && dungeon[x - 1][y];
            bool forceState = (x == 0 || roomExists);
            bool open = (x == 0 ? false : (roomExists ? dungeon[x - 1][y].exits.Find(a => a.key == "Right").state : false));
            if (forceState && exit.forceState != true)
            {
                exit.state = open;
                exit.forceState = forceState;
            }

            exit = room.GetComponent<Room>().exits.Find(a => a.key == "Right");
            roomExists = x < dungeon.Length - 1 && dungeon[x + 1][y];
            forceState = (x == dungeon.Length - 1 || roomExists);
            open = (x == dungeon.Length - 1 ? false : (roomExists ? dungeon[x + 1][y].exits.Find(a => a.key == "Left").state : false));
            if (forceState && exit.forceState != true)
            {
                exit.state = open;
                exit.forceState = forceState;
            }

            exit = room.GetComponent<Room>().exits.Find(a => a.key == "Bottom");
            roomExists = y > 0 && dungeon[x][y - 1];
            forceState = (y == 0 || roomExists);
            open = (y == 0 ? false : (roomExists ? dungeon[x][y - 1].exits.Find(a => a.key == "Top").state : false));
            if (forceState && exit.forceState != true)
            {
                exit.state = open;
                exit.forceState = forceState;
            }

            exit = room.GetComponent<Room>().exits.Find(a => a.key == "Top");
            roomExists = y < dungeon[0].Length - 1 && dungeon[x][y + 1];
            forceState = (y == dungeon[0].Length - 1 || roomExists);
            open = (y == dungeon[0].Length - 1 ? false : (roomExists ? dungeon[x][y + 1].exits.Find(a => a.key == "Bottom").state : false));
            if (forceState && exit.forceState != true)
            {
                exit.state = open;
                exit.forceState = forceState;
            }

            room.GetComponent<Room>().Init();
            dungeon[x][y] = room.GetComponent<Room>();
            if (dungeon[x][y].HasExit("Top"))
                GenerateRoom(x, y + 1, "Bottom");
            if (dungeon[x][y].HasExit("Bottom"))
                GenerateRoom(x, y - 1, "Top");
            if (dungeon[x][y].HasExit("Left"))
                GenerateRoom(x - 1, y, "Right");
            if (dungeon[x][y].HasExit("Right"))
                GenerateRoom(x + 1, y, "Left");
        }
    }
}
