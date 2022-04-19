using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] private GameObject graphics;

    private void Awake()
    {
        graphics.SetActive(false);
    }
}
