using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystem : MonoBehaviour
{
    public GameObject PaperPrefab;
    public Transform PaperSpawnAnchor;
    public WebInput web;
    public float SpawnTimer = 5f;
    public float longTimer = 5f;
    public float shortTimer = .1f;

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
        if(web)
        {
            SpawnTimer = map(web.windIntensity, 0f, 250f, longTimer, shortTimer);

        }
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
        NewPaper.GetComponentInChildren<AnimatorWindReact>().web = web;
    }

    float map(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
}
