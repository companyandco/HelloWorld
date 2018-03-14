using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	/*
	 *
	 * This script should handle the behaviour that our player has on the network.
	 * 
	 */

	public GameObject MainControllerPrefab;

	private Main_Controller MainControllerSelf, MainControllerOther;
	
	private PhotonView PhotonView;
	
	private int teamId;

	public int SetTeamId
	{
		set { this.teamId = value; }
	}

	private bool isGameCanStart = false;
	
	void Start ()
	{
		
		Debug.Log ( "Going to sleep." );
		
		Thread.Sleep ( 6000 );
		
		Debug.Log ( PhotonNetwork.countOfPlayersInRooms );
		
		//TODO: differentiate both prefabs.
		//TODO: Instatiate pannels and camera for this Main_Controller. (LOCAL)
		MainControllerSelf = Instantiate ( this.MainControllerPrefab ).GetComponent<Main_Controller> ();
		
		MainControllerOther = Instantiate ( this.MainControllerPrefab ).GetComponent<Main_Controller> ();
		
		this.MainControllerSelf.isDefending = this.teamId % 2 != 0;

		this.MainControllerOther.isDefending = !this.MainControllerSelf.isDefending;
		
		this.PhotonView = GetComponent <PhotonView> ();
		
	}

	public void OnSpellUsed (string spellUsed)
	{
		this.PhotonView.RPC ( "SendString", PhotonTargets.Others, spellUsed );
	}

	public void OnSpellUsed (string spellUsed, Main_Controller.Region country)
	{
		this.PhotonView.RPC ( "SendString", PhotonTargets.Others, spellUsed, country );
	}

	[PunRPC]
	void SendString ( string s )
	{
		this.MainControllerOther.OnRpcOnSpellUsedCallback ( s );
	}

	[PunRPC]
	void SendString ( string s, Main_Controller.Region country )
	{
		this.MainControllerOther.OnRpcOnSpellUsedCallbackRegion ( s, country );
	}
	
}
