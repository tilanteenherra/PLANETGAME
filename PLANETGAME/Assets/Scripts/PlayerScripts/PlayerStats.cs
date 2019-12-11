using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHp;
    public float curHp;
    public GameObject firePlace;
    // Start is called before the first frame update
    void Awake()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        // Player health checks
        if (curHp > maxHp)
        {
            curHp = maxHp;
        }
        if (curHp <= 0)
        {
            Die();
        }   
    }

    void Die()
    {
        Destroy(gameObject);
        Destroy(firePlace);
    }
}
