using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Launcher : MonoBehaviour
{
    public static Launcher Instance;

	[SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text roomNameText;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] Transform playerListContent;
	[SerializeField] GameObject PlayerListItemPrefab;
	[SerializeField] GameObject startGameButton;

	void Awake()
	{
		Instance = this;
	}

    void Start()
	{
		Debug.Log("Connecting to Master");
	}

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        MenuManager.Instance.OpenMenu("loading");
    }

    public void ExitGame()
    {   
        Application.Quit();
    }

    public void StartGame()
    {
        //PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        //PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom()
    {
        MenuManager.Instance.OpenMenu("loading");
    }

    //public override void OnRoomListUpdate(List<> roomList)
    //{
        //foreach (Transform trans in roomListContent)
        //{
        //    Destroy(trans.gameObject);
        //}
        //
        //for (int i = 0; i < roomList.Count; i++)
        //{
         //   if (roomList[i].RemovedFromList)
       //         continue;
     //       Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
   //     }
 //   }

 //   public void OnPlayerEnteredRoom(Player newPlayer)
 //   {
 //       Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
 //   }
}