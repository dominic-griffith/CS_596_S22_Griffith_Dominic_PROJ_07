using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject battleButton;
    public GameObject cancelButton;
    // Start is called before the first frame update
    private void Awake() {
        lobby = this;//creates singleton within Main menu scene
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Photon Network
    }

    public override void OnConnectedToMaster(){
        Debug.Log("We are now connected to: " + PhotonNetwork.CloudRegion); 
        battleButton.SetActive(true); 
    }

    public void OnBattleButtonClicked(){
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        if (PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        {
            PhotonNetwork.JoinRandomRoom();
        }   
        else
        {
            Debug.LogError("Can't join random room now, client is not ready");
        }

    }

    //if room is not found, creates room with default room settings
    public override void OnJoinRandomFailed(short returnCode, string message){
        Debug.Log("tried to join randomGame but failed. No open games.");
        CreateRoom();

    }
    void CreateRoom(){
        Debug.Log("Creating Room");

        int randomRoomName = Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions();//room options
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        roomOptions.IsOpen = true;
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom(){
        Debug.Log("We are now in a room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message){
        Debug.Log("tried to create a room but failed. maybe already room with same name?");
        CreateRoom();
    }
    // Update is called once per frame
    public void OnCancelButtonClicked(){
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
