using System.Threading;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	/*
	 *
	 * This script should handle the behaviour that our player has on the network.
	 * 
	 */

	public GameObject MainControllerPrefab;

	public GameObject WorldLiveInfosPrefab;

	private Main_Controller MainController;
	
	private PhotonView photonView;

	private int teamId = -1;

	public int SetTeamId
	{
		set { this.teamId = value; }
	}

	private bool isGameCanStart = false;
	
	void Start ()
	{
		Debug.Log ( "Going to sleep." );

		StartThisThing ();
	}
	
	void StartThisThing()
	{
		Thread.Sleep ( 6000 );

		teamId = PhotonNetwork.countOfPlayers;
		
		//TODO: differentiate both prefabs.
		//TODO/FIXME: Instatiate pannels and camera for this Main_Controller does it twice... (LOCAL)
		MainController = Instantiate ( this.MainControllerPrefab ).GetComponent <Main_Controller> ();

		this.MainController.player = this;

		this.MainController.name = "Main_Controller_Self";

		this.MainController.isDefending = this.teamId % 2 != 0;

		this.photonView = GetComponent <PhotonView> ();

		GameObject worldLiveInfoInstance = Instantiate ( WorldLiveInfosPrefab, Vector3.zero, Quaternion.identity );

		worldLiveInfoInstance.GetComponentInChildren <WorldLiveInfos> ().MainControllerObject = MainController.transform.gameObject;
	}

	public string spellIUsed = "";

	public Main_Controller.Region myCountry;
	
	public void OnSpellUsed (string spellUsed)
	{
		this.spellIUsed = spellUsed;
		
		this.photonView.RPC ( "SendString", PhotonTargets.Others, spellIUsed );
	}

	public void OnSpellUsed (string spellUsed, Main_Controller.Region country)
	{
		this.spellIUsed = spellUsed;
		this.myCountry = country;
		
		this.photonView.RPC ( "SendString", PhotonTargets.Others, spellIUsed, myCountry );
	}

	[PunRPC]
	void SendString ( string s )
	{
		this.MainController.OnRpcOnSpellUsedCallback ( s );
	}

	[PunRPC]
	void SendString ( string s, Main_Controller.Region country )
	{
		this.MainController.OnRpcOnSpellUsedCallbackRegion ( s, country );
	}
	
}
