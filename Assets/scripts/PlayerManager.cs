using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.IO;

public class PlayerManager : NetworkBehaviour
{
    public GameObject controller;
    private void Awake()
    {
    }

    private void Start()
    {
            CreateController();
    }

    void CreateController()
    {
        Transform spawnpoint = SpawnManager.instance.GetSpawnPoint();
        controller = Instantiate(controller, spawnpoint.position, spawnpoint.rotation); //Instantiate Player Controller
    }

    public void Die()
    {
        CreateController();
    }
}
