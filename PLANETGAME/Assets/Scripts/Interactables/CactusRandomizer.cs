using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusRandomizer : MonoBehaviour
{
    public GameObject[] cactuses;
    private GameObject child;
    public Material cactusMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        if (cactuses != null)
        {
            GameObject cactus = Instantiate(cactuses[Random.Range(0, cactuses.Length)], child.transform.position,
                child.transform.rotation,transform);
            Destroy(child);
            MeshRenderer asd = cactus.AddComponent<UnityEngine.MeshRenderer>();
            asd.material = cactusMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
