using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSmokeScripts : MonoBehaviour
{
    public GameObject dashSmoke;
    public bool smokeOn = false;
    private GameObject empty;
    private GameObject tempObj;

    // Start is called before the first frame update
    void Start()
    {
        empty = new GameObject();
        dashSmoke = GameObject.Find("DashSmoke");
    }

    // Update is called once per frame
    void Update()
    {
        if (smokeOn)
        {
            empty.transform.rotation = transform.rotation;
            empty.transform.Rotate(transform.up, 180f, Space.World);
            //empty.transform.rotation = Quaternion.Inverse(transform.rotation);
            tempObj = Instantiate(dashSmoke, transform.position, empty.transform.rotation);
            
            Destroy(tempObj, 1.0f);
        }
    }
}