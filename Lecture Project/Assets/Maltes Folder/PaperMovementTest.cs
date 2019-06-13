using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMovementTest : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float Lifetime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * MoveSpeed;
    }
}
