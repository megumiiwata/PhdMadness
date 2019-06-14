using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testControllerInput : MonoBehaviour
{
    public GameObject Object;
    // Start is called before the first frame update
    void Start()
    {
        Object.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > .9 || Input.GetAxis("Fire1") == 1)
        {
            Object.SetActive(true);
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) <= .9 || Input.GetAxis("Fire1") == 0)
        {
            Object.SetActive(false);
        }
    }
}
