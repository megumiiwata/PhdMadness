using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAndStop : MonoBehaviour
{
    public float waitTime;
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            particleSystem.Play();
            StartCoroutine("StopParticleSystem");
        }

    }

    IEnumerator StopParticleSystem()
    {
        yield return new WaitForSeconds(waitTime);
        particleSystem.Stop();
    }
}
