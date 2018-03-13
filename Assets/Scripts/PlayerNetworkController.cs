using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkController : NetworkBehaviour {

	public GameObject PlayerUnitPrefab;

	//[SyncVar(hook="OnPlayerNameChange")] to avoid using Rpc, have to re-set the name still after that.
	private string ObjectName;
	
	void Start ()
	{

		this.ObjectName = Random.Range ( 0, 10 ).ToString();

		if ( !isLocalPlayer )
			return;
		
		CmdSpawnMyUnit ();
		
	}

	private GameObject myPlayerUnit;
	
	[Command]
	void CmdSpawnMyUnit ()
	{
		this.PlayerUnitPrefab.name = this.ObjectName;
		
		GameObject go = Instantiate ( this.PlayerUnitPrefab );

		this.myPlayerUnit = go;

		NetworkServer.SpawnWithClientAuthority ( go, connectionToClient );
		
		RpcCHangeName ();
	}

	[ClientRpc]
	void RpcCHangeName ()
	{
		this.myPlayerUnit.name = ObjectName;
	}
	
}
