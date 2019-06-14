using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePaper : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem partSys;
    public AudioSource audio;
    public GameObject modelObject;
    bool startedOnce;


    // Start is called before the first frame update
    void Start()
    {
        startedOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(animator && animator.GetCurrentAnimatorStateInfo(1).IsName("Explode"))
        {
            if(startedOnce == false)
            {
                partSys.Play();
                startedOnce = true;
                audio.Play();
                modelObject.SetActive(false);
            }
            
            if(!partSys.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
