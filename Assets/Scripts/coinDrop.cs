using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinDrop : MonoBehaviour {
    public Vector2 fallspot;
    public GameObject cGold;
    public float dropTime;
    public float dropWait;
    public float countdown;


	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWaves());
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(dropTime);
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 falldrop = new Vector2(Random.Range(-fallspot.x, fallspot.x), fallspot.y);
                Quaternion qman = Quaternion.identity;
                Instantiate(cGold, falldrop, qman);
                yield return new WaitForSeconds(dropWait);
            }
            yield return new WaitForSeconds(5);
        }
    }
}
