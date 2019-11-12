using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody1 : MonoBehaviour
{

    public FauxGravityAttractor1 attractor;
    private Transform myTransform;


    // Start is called before the first frame update
    void Awake()
    {
        attractor = GameObject.FindGameObjectWithTag("World1").GetComponent<FauxGravityAttractor1>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        attractor.Attract(myTransform);
        /*
        if (attractor)
        {
            attractor.Attract(myTransform);
        }
        */
    }
}
