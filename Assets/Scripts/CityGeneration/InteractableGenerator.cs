﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGenerator : Generator
{
    [SerializeField]
    private int numInteractables = 5;

    private List<GameObject> interactables = new List<GameObject>();

    [SerializeField]
    private GoDropList dropList;

    public override void Clear()
    {
        foreach (GameObject i in interactables)
        {
            DestroyImmediate(i);
        }
        interactables.Clear();
    }

    public override void Generate()
    {
        Debug.Log("Generating Interactables");
        PoissonGenerator poisson = new PoissonGenerator();
        poisson.GenerateDensity(numInteractables);
        poisson.Scale(scale);
        foreach (PoissonPoint point in poisson.GetPoints())
        {
            interactables.Add(InstantiateHandler.mInstantiate(dropList.SelectGO(), point.pos, transform));
        }
        Debug.Log("Done Interactables");
    }

    private void OnDrawGizmos()
    {
        if (!gizmosEnabled)
            return;
        Gizmos.color = Color.blue;
        foreach (GameObject go in interactables)
        {
            Gizmos.DrawSphere(go.transform.position, 3);
        }
    }
}