using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystem : MonoBehaviour
{
    public GameObject PaperPrefab;
    public Transform PaperSpawnAnchor;
    public float SpawnTimer = 5f;

    private float NextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewPaper();
        NextSpawnTime = Time.time + SpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = PaperSpawnAnchor.position;
        if(Time.time >= NextSpawnTime)
        {
            SpawnNewPaper();
            NextSpawnTime = Time.time + SpawnTimer;
        }
    }

    public void SpawnNewPaper()
    {
        Quaternion SpawnRotation = Quaternion.Euler(0, Random.Range(0f, 359f), 0);

        GameObject NewPaper = Instantiate(PaperPrefab) as GameObject;
        NewPaper.transform.rotation = SpawnRotation;
        NewPaper.transform.position = PaperSpawnAnchor.position;
    }
}
