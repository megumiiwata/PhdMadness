/*
 * WebInput.cs
 *
 * UBISS 2019 - Workshop A - Project: "PhD Madness"
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebInput : MonoBehaviour
{
    public int windIntensity;

    private void Start()
    {
        windIntensity = 0;
    }


    // DEBUG
    public GameObject cube; // set via inspector
    private float scaleFactor = 0.01f;
    private void scaleCube()
    {
        cube.transform.localScale = new Vector3(windIntensity * scaleFactor, windIntensity * scaleFactor, windIntensity * scaleFactor);
    }

    private void Update()
    {
        scaleCube();
    }
}
