using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSmokeScripts : MonoBehaviour
{
    public GameObject dashSmoke;
    public bool smokeOn = false;
    private GameObject tempObj;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (smokeOn)
        {
            tempObj = Instantiate(dashSmoke, transform.position,dashSmoke.transform.rotation);
            Destroy(tempObj, 1.0f);
        }
    }
}
