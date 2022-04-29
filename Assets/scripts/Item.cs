using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Item : NetworkBehaviour
{
   public ItemInfo ItemInfo;
   public GameObject ItemGameObject;
   public NetworkBehaviour nb;
    public abstract void Use();

}
