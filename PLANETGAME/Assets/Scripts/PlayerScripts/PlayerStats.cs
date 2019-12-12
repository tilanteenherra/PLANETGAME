using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHp;
    public int dieRand;
    public float curHp;
    public GameObject firePlace;
    private PlayerController pc;

    Vector3 playerPos;
    public bool keepPlace = false;

    // Start is called before the first frame update
    void Awake()
    {
        curHp = maxHp;
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keepPlace)
        {
            transform.position = playerPos;
        }

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
        dieRand = Random.Range(1, 2);
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        playerPos = transform.position;
        keepPlace = true;

        if (dieRand == 1)
        {
            pc.anim.SetInteger("condition", 66);
        }
        else if(dieRand == 2)
        {
            pc.anim.SetInteger("condition", 67);
        }
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
