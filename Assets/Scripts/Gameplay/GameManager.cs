﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isHuman;
    [SerializeField]
    private GameObject humanPrefab;
    [SerializeField]
    private GameObject monsterPrefab;
    [SerializeField]
    private CharTPCamera playerCamera;

    public static GameObject playerObj = null;

    private void Awake()
    {
        playerObj = PhotonNetwork.Instantiate(isHuman ? humanPrefab.name : monsterPrefab.name, new Vector3(0.0f, 4.0f, 0.0f), Quaternion.identity);
        playerCamera.GiveMeCharController(playerObj.GetComponent<CharTPController>());
    }
}
