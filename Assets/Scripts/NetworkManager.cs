using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{

	private const string VERSION = "v0.0.1";

	public string roomName = "Room#001";

	public GameObject Player2;
	
	void Start ()
	{
		Connect ();
	}

	void Connect ()
	{
		PhotonNetwork.ConnectUsingSettings ( VERSION );
	}

	void OnJoinedLobby ()
	{
        PhotonNetwork.JoinOrCreateRoom(this.roomName, null, null);
		//PhotonNetwork.JoinRoom ( this.roomName );
	}

	void OnPhotonJoinRoomFailed ()
	{
		PhotonNetwork.CreateRoom ( this.roomName );
	}
	
	void OnJoinedRoom ()
	{
		Debug.Log ( "RoomJoined" );
		
		
		string objectToSpawn = "Player" + (PhotonNetwork.countOfPlayersInRooms % 2 + 1) % 10;
		
		Debug.Log ( objectToSpawn );

		Cube = PhotonNetwork.Instantiate ( objectToSpawn, Vector3.zero, Quaternion.identity, 0 );
		

		//Debug.Log ( "Joined, spawning." );
		
		GameObject go = PhotonNetwork.Instantiate ( "playerPrefab", Vector3.zero, Quaternion.identity, (byte)PhotonNetwork.countOfPlayersInRooms );
		
		go.name = "Player" /*+ (PhotonNetwork.countOfPlayersInRooms + 1)*/;
		
		PlayerScript ps = go.GetComponent <PlayerScript> ();

		ps.SetTeamId = PhotonNetwork.countOfPlayersInRooms;
	}

	private GameObject Cube;
	
	void MoveUp ()
	{
		this.Cube.transform.position += Vector3.up;
	}
	
	void MoveDown ()
	{
		this.Cube.transform.position -= Vector3.up;
	}

	void Update ()
	{
		
		if (Input.GetKeyDown ( KeyCode.B ))
			MoveDown ();
		if ( Input.GetKeyDown ( KeyCode.H ) )
			MoveUp ();
	}

	private void OnGUI ()
	{
		GUILayout.Label ( PhotonNetwork.connectionStateDetailed.ToString() );
	}

}
