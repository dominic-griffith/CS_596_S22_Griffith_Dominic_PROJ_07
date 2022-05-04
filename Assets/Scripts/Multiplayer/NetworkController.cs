using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Photon Network
    }

    public override void OnConnectedToMaster(){
        Debug.Log("We are now connected to: " + PhotonNetwork.CloudRegion); 
        RoomOptions roomOptions = new RoomOptions(); //creates options for multiplayer room
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Salinity", roomOptions, TypedLobby.Default); //checks to see if room exists with name and joins or creates

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
