using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    public GameObject[] players;
    int i = 0;
    // Start is called before the first frame update
    void Awake()
    {
        players = new GameObject[4];
        CreatePlayer();

    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        players[i] = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Skeleton"), Vector3.zero, Quaternion.identity);
        i++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
