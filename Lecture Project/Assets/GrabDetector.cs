using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetector : MonoBehaviour
{
    public GameObject RightControllerAnchor;
    bool GrabIsPossible;
    bool IsGrabbing;
    GameObject CurrentlyGrabbedObject;
    GameObject GrabObjectCandidate;
    // Start is called before the first frame update
    void Start()
    {
        CurrentlyGrabbedObject = null;
        IsGrabbing = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(GrabIsPossible && !IsGrabbing)
        {
            Debug.Log("Grab Possible");
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > .9)
            {
                Debug.Log("Grab durchgef�hrt");
                IsGrabbing = true;
                CurrentlyGrabbedObject = GrabObjectCandidate;
                CurrentlyGrabbedObject.transform.SetParent(RightControllerAnchor.transform);
                CurrentlyGrabbedObject.GetComponent<PaperMovementTest>().MoveSpeed = 0;
            }
        }

        if(IsGrabbing)
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) <= .9)
            {
                IsGrabbing = false;
                CurrentlyGrabbedObject.transform.SetParent(null);
                CurrentlyGrabbedObject.GetComponent<PaperMovementTest>().MoveSpeed = 1;
                CurrentlyGrabbedObject = null;
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Paper") && CurrentlyGrabbedObject == null)
        {
            GrabIsPossible = true;
            GrabObjectCandidate = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            GrabIsPossible = false;
        }
    }
}
