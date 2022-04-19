using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public abstract override void Use();

    public GameObject bulletImpactPrefab;
    public ParticleSystem MuscleFlash;
    public Transform GunTip;
}
