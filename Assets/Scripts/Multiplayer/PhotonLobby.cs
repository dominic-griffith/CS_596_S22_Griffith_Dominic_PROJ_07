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
        // RoomOptions roomOptions = new RoomOptions(); //creates options for multiplayer room
        // roomOptions.IsVisible = false;
        // roomOptions.MaxPlayers = 4;
        // PhotonNetwork.JoinOrCreateRoom("Salinity", roomOptions, TypedLobby.Default); //checks to see if room exists with name and joins or creates

    }

    public void OnBattleButtonClicked(){
        PhotonNetwork.JoinRandomRoom();
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
    }

    //if room is not found, creates room with default room settings
    public override void OnJoinRandomFailed(short returnCode, string message){
        Debug.Log("tried to join randomGame but failed. No open games.");
        //int randomRoomName = Random.Range(0, 10000);
        CreateRoom();

    }
    void CreateRoom(){
        RoomOptions roomOptions = new RoomOptions();//room options
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("Salinity", roomOptions, TypedLobby.Default);
    }

    public override void OnCreateRoomFailed(short returnCode, string message){
        Debug.Log("tried to create a room but failed. maybe already room with same name?");
    }
    // Update is called once per frame
    public void OnCancelButtonClicked(){
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
