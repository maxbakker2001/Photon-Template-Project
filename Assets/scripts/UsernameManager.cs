using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UsernameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;

    public void UsernameValueChange()
    {
        PhotonNetwork.NickName = username.text;
    }
}
