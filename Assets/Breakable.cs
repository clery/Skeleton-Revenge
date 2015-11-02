using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {

    public int minCoins = 5;
    public int maxCoins = 10;
    public GameObject coin = null;
    public float timeBetweenCoinSpawn = 0.2f;

    void Start()
    {
        if (coin == null)
            Debug.LogWarning("Please add coin gameobject to breakable items");
    }

    public void Break()
    {
        int rand = Random.Range(minCoins, maxCoins);
        for (int i = rand; i > 0; --i)
            Invoke("SpawnCoin", i * timeBetweenCoinSpawn);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, 2f);
    }

    private void SpawnCoin()
    {
        GameObject obj;

        Debug.Log("Spawn coin !");
        obj = (GameObject)Instantiate(coin, transform.position, Quaternion.Euler(Vector3.zero));
        obj.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-6, 6), Random.Range(20, 35), 0);

    }
}
