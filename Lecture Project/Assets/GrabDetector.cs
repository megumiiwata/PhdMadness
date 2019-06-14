using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetector : MonoBehaviour
{
    public GameObject RightControllerAnchor;
    public float grabTimer;
    public AudioSource grabSound;
    float nextGrabTime;
    float startGrabTime;
    bool GrabIsPossible;
    bool IsGrabbing;
    GameObject OldParent;
    GameObject CurrentlyGrabbedObject;
    GameObject GrabObjectCandidate;
    Material paperMat;
    Animator paperAnimator;
    // Start is called before the first frame update
    void Start()
    {
        CurrentlyGrabbedObject = null;
        IsGrabbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGrabbing)
        {
            paperMat.SetColor("_Color", new Color(1, 1, 1, 1 - Mathf.InverseLerp(startGrabTime, nextGrabTime, Time.time)));
            if(Time.time >= nextGrabTime)
            {
                IsGrabbing = false;
                CurrentlyGrabbedObject.transform.SetParent(OldParent.transform);
                Destroy(CurrentlyGrabbedObject.transform.parent.gameObject);
                //CurrentlyGrabbedObject = null;
            }
            if(Input.GetAxis("Fire1") == 0)
            {
                //IsGrabbing = false;
                //CurrentlyGrabbedObject.transform.SetParent(null);
                //paperAnimator = null;
                //CurrentlyGrabbedObject.GetComponent<PaperMovementTest>().MoveSpeed = 1;
                //CurrentlyGrabbedObject = null;
            }
        }

        //else if (GrabIsPossible && !IsGrabbing)
        //{
        //    if (Input.GetAxis("Fire1") == 1)
        //    {

        //        IsGrabbing = true;
        //        CurrentlyGrabbedObject = GrabObjectCandidate;
        //        Vector3 worldPos = CurrentlyGrabbedObject.transform.position;
        //        CurrentlyGrabbedObject.transform.SetParent(RightControllerAnchor.transform);
        //        CurrentlyGrabbedObject.transform.position = worldPos;
        //        CurrentlyGrabbedObject.GetComponent<PaperMovementTest>().MoveSpeed = 0;
        //        SetupAnimation();
        //        nextGrabTime = Time.time + grabTimer;
        //        startGrabTime = Time.time;
        //    }
        //}



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Paper") && !IsGrabbing)
        {
            //GrabIsPossible = true;
            //GrabObjectCandidate = other.gameObject;

            IsGrabbing = true;
            CurrentlyGrabbedObject = other.gameObject;
            Vector3 worldPos = CurrentlyGrabbedObject.transform.position;
            Quaternion worldRot = CurrentlyGrabbedObject.transform.rotation;
            OldParent = CurrentlyGrabbedObject.transform.parent.gameObject;
            CurrentlyGrabbedObject.transform.SetParent(RightControllerAnchor.transform, true);
            //CurrentlyGrabbedObject.GetComponent<PaperMovementTest>().MoveSpeed = 0;
            SetupAnimation();
            nextGrabTime = Time.time + grabTimer;
            startGrabTime = Time.time;
            grabSound.Play();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Paper"))
    //    {
    //        GrabIsPossible = false;
    //    }
    //}

    private void SetupAnimation()
    {
        paperAnimator = CurrentlyGrabbedObject.GetComponentInChildren<Animator>();
        paperAnimator.SetTrigger("stopMovement");
        paperMat = CurrentlyGrabbedObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
    }
}
