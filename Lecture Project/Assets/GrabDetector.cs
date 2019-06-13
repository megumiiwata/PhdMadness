using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetector : MonoBehaviour
{
    bool IsGrabPossible;
    GameObject CurrentlyGrabbedObject;
    // Start is called before the first frame update
    void Start()
    {
        CurrentlyGrabbedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentlyGrabbedObject)
        {
            CurrentlyGrabbedObject.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Paper"))
        {
            Debug.Log("Grab possible");
            IsGrabPossible = true;

            if(CurrentlyGrabbedObject)
            {
                CurrentlyGrabbedObject.GetComponent<PaperMovementTest>().MoveSpeed = 1;
            }
            CurrentlyGrabbedObject = other.gameObject;
            CurrentlyGrabbedObject.GetComponent<PaperMovementTest>().MoveSpeed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            Debug.Log("Grab NOT possible");
            IsGrabPossible = false;
        }
    }
}
