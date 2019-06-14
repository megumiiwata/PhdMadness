using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorWindReact : MonoBehaviour
{
    public WebInput web;
    public float animationSpeed;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(web)
        {
            animationSpeed = map(web.windIntensity, 0f, 250f, 0.1f, 1.2f);
            animator.speed = animationSpeed;
            Debug.Log(animationSpeed);

        }
    }

    float map(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
}
