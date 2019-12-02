﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Starting");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We're now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
