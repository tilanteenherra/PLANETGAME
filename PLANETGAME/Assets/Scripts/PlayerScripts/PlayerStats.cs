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
    // Start is called before the first frame update
    void Awake()
    {
        curHp = maxHp;
        pc = GetComponent<PlayerController>();
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
        dieRand = Random.Range(1, 2);
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        if(dieRand == 1)
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
