using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor1 : MonoBehaviour
{
    public float gravity1 = -15;

    Transform world1;

    private void Awake()
    {
        world1 = GameObject.FindGameObjectWithTag("World1").GetComponent<Transform>();
    }

    public void Attract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity1);
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
