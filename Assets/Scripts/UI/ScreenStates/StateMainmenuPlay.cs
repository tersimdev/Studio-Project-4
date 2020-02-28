﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateMainmenuPlay : State
{
    [Header("References")]
    [SerializeField]
    [Tooltip("The status message for hosting a room.")]
    private TextMeshProUGUI tmHostStatus = null;
    [SerializeField]
    [Tooltip("The status message for joining a room.")]
    private TextMeshProUGUI tmJoinStatus = null;

    [Header("Inputs")]
    [SerializeField]
    [Tooltip("Host Password's Input Field")]
    private TMP_InputField tmHostPass = null;
    [SerializeField]
    [Tooltip("Join Password's Input Field")]
    private TMP_InputField tmJoinPass = null;

    private bool isConnecting = false;
    public override string Name { get { return "MainmenuPlay"; } }

    private void Start()
    {
        NetworkClient.instance.roomCreateFailedCallback = onCreateLobbyFailed;
        NetworkClient.instance.roomJoinFailedCallback = onJoinRoomFailed;
        NetworkClient.instance.randomRoomJoinFailedCallback = onJoinRandomRoomFailed;
        NetworkClient.instance.roomJoinedCallback = onJoinRoomSuccess;

    }

    private void OnDestroy()
    {
        NetworkClient.instance.roomCreateFailedCallback = null;
        NetworkClient.instance.roomJoinFailedCallback = null;
        NetworkClient.instance.randomRoomJoinFailedCallback = null;
        NetworkClient.instance.roomJoinedCallback = null;
        StateController.Unregister(this);

    }

    private void Update()
    {
        
    }

    public void hostRoom()
    {
        isConnecting = true;
        tmHostStatus.text = "Creating a room...";
        NetworkClient.instance.Host(tmHostPass.text);
    }

    public void joinRoom()
    {
        isConnecting = true;
        tmJoinStatus.text = "Joining room...";
        NetworkClient.instance.Join(tmJoinPass.text);
    }

    public void joinRandom()
    {
        isConnecting = true;
        tmJoinStatus.text = "Joining random...";
        NetworkClient.instance.Join("");
    }


    public override void onShow()
    {
        isConnecting = false;
        base.onShow();
    }


    /* NETWORKING */

    private void onJoinRoomSuccess(List<string> playersInLobby)
    {
        if (isConnecting)
        {
            Debug.Log("Players In Room (MAINMENUPLAY): " + playersInLobby.Count);
            StateController.showNext("MatchLobby");
            FindObjectOfType<StateMatchLobby>()?.onJoinRoom(playersInLobby);
        }
    }

    private void onCreateLobbyFailed()
    {
        tmHostStatus.text = "Lobby already exists";
    }

    private void onJoinRandomRoomFailed()
    {
        
    }

    private void onJoinRoomFailed()
    {
        tmJoinStatus.text = "Lobby does not exist / is in progress.";
    }


}
