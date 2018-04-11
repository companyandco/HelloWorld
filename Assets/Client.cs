using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using System.Collections.Generic;

public class Client : MonoBehaviour
{

	public string ClientName;
	
	[NonSerialized]
	public bool IsHost;
	
	private List <GameClient> players;
	
	private bool isSocketReady;
	private TcpClient socket;
	private NetworkStream stream;
	private StreamWriter writer;
	private StreamReader reader;

	void Start ()
	{
		players = new List <GameClient> ();
		
		DontDestroyOnLoad ( gameObject );
	}
	
	public bool ConnectToServer ( string host, int port )
	{
		if ( this.isSocketReady )
			return false;

		try
		{
			this.socket = new TcpClient ( host, port );
			this.stream = this.socket.GetStream ();
			this.writer = new StreamWriter ( this.stream );
			this.reader = new StreamReader ( this.stream );

			this.isSocketReady = true;
		} catch ( Exception e )
		{
			Debug.Log ( "Socket error: " + e.Message );
		}

		return this.isSocketReady;
	}

	private void OnIncomingData ( string data )
	{
		// TODO: link this to actual game.
		Debug.Log ( "Client: " + data );

		string[] aData = data.Split ( '|' );

		switch ( aData [0] )
		{
			case "SWHO":
				for(int i = 1; i < aData.Length; i++)
				{
					UserConnected ( aData[i], false );
				}

				Send ( "CWHO|" + this.ClientName + "|" + ( this.IsHost ? 1 : 0 ) );
				break;
			case "SCON":
				UserConnected ( aData [1], IsHost ); //TODO: define if it's a host or not.
				break;
		}
	}

	public void Send ( string data )
	{
		if ( !this.isSocketReady )
			return;

		this.writer.WriteLine ( data );
		this.writer.Flush ();
	}

	private void UserConnected ( string userName, bool isHost )
	{
		if ( userName != "" )
		{
			GameClient c = new GameClient ();

			c.name = userName;

			this.players.Add ( c );

			/*
			foreach ( GameClient client in this.players )
			{
				Debug.Log ( client.name );
			}
			*/
		} else
		{
			//Debug.Log ( "It came from there, that little slut." );
		}

		if ( this.players.Count == 2 )
		{
			MultiplayerMenuManager.Instance.StartGame ();
		}
	}

	void Update ()
	{

		if ( !this.isSocketReady )
			return;

		if ( this.stream.DataAvailable )
		{
			string data = this.reader.ReadLine ();

			if ( data != null )
				OnIncomingData ( data );
		}

	}

	private void OnApplicationQuit ()
	{
		CloseSocket ();
	}

	private void OnDisable ()
	{
		CloseSocket ();
	}
	
	private void CloseSocket ()
	{
		if ( !this.isSocketReady )
			return;

		this.writer.Close ();
		this.reader.Close ();
		this.socket.Close ();
		this.isSocketReady = false;
	}

}

public class GameClient
{

	public string name;
	public bool isHost;
	
	/*
	/// <Summary>  ///
	/// TODO?
	/// Insert all the variables we need for our player in here.
	/// a.k.a: Player 1 or 2 ? (Defense / Attack)
	/// </Summary> ///
	*/
	
}