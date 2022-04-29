using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;


    public void SetUp()
    {
    }

    public void Onclick()
    {
        Launcher.Instance.JoinRoom();
    }
}
