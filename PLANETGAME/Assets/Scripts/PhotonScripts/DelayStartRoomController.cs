﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int waitingRoomSceneIndex; // number for the build index to the multiplay scene

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() // successfully created or joined a room
    {
        //Debug.Log("Joined Room");
        //StartGame();
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }

    //private void StartGame()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        Debug.Log("Starting Game");
    //        PhotonNetwork.LoadLevel(waitingRoomSceneIndex);
    //    }
    //}
}