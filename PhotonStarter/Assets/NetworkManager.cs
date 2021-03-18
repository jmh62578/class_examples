using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkManager : MonoBehaviourPunCallbacks,ILobbyCallbacks
{

	
	public GameObject roomUI;
	private void Awake()
	{
        Object.DontDestroyOnLoad(this);
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
		PhotonNetwork.SendRate = 60;
		PhotonNetwork.SerializationRate =5;
	}

    public override void OnConnectedToMaster()
	{
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
	}
	public override void OnJoinedLobby()
	{
        Debug.Log("Joined Lobby");
		roomUI.SetActive(true);
	}
	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		Debug.Log("Room list updated");
		foreach(RoomInfo ri in roomList)
		{
			Debug.Log(ri.Name);
		}
	}

	public void OnJoinRandomRoomClicked()
	{
		roomUI.SetActive(false);
		PhotonNetwork.JoinRandomRoom();
	}
	public void OnCreateRoomClicked()
	{
		roomUI.SetActive(false);
		PhotonNetwork.CreateRoom(null);
	}
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("Join Random Room Failed");
		roomUI.SetActive(true);
	}
	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.Log("Create Room Failed");
		roomUI.SetActive(true);
	}
	public override void OnJoinedRoom()
	{
		Debug.Log("Joined Room");
		PhotonNetwork.Instantiate("Player",Vector3.zero,Quaternion.identity);
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
