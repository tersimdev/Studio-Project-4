﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MonsterEnergy : MonoBehaviour, IPunObservable
{
    [SerializeField]
    private CharHealth health;
    [SerializeField]
    private float maxEnergy = 100;
    [SerializeField] [Tooltip("How many seconds it takes to lose 1 energy")]
    private float decayRate = 0.01f;

    private float energy;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(energy);
        }
        else 
        {
            energy = (float)stream.ReceiveNext();
        }
    }

    private void OnEnable()
    {
        health.OnRespawn += RechargeFull;
    }

    private void OnDisable()
    {
        health.OnRespawn -= RechargeFull;
    }

    private void Start()
    {
        RechargeFull();
        health.SetInvulnerable(true);
        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
    {
        while (!health.dead && energy > 0)
        {
            yield return new WaitForSeconds(decayRate);
            energy -= 1.0f;
            if (energy <= 0)
            {
                NoMoreEnergy();
            }
            //Debug.Log("Energy at " +  energy);
        }
    }

    private void NoMoreEnergy()
    {
        health.SetInvulnerable(false);
        Debug.Log("Lost all energy");
    }
    public bool UseUp(float amt)
    {
        if (energy < amt)
            return false;

        energy -= amt;
        if (energy <= 0)
        {
            NoMoreEnergy();
        }
        return true;
    }
    public void Recharge(float amt)
    {
        energy += amt;
        if (energy > maxEnergy)
            energy = maxEnergy;
    }
    public void RechargeFull()
    {
        Recharge(maxEnergy);
    }
}
