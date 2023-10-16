using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscTemp : MonoBehaviour
{
    public GameObject selectObj;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb = selectObj.GetComponent<Rigidbody>();
    }
}
