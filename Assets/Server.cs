using System;
using System.Net.Sockets;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Net;

public class Server : MonoBehaviour
{
	public int port = 6321;

	private List <ServerClient> clients;
	private List <ServerClient> disconnectList;

	private TcpListener server;
	private bool isServerStarted;

	public void Init ()
	{
		DontDestroyOnLoad ( gameObject );

		this.clients = new List <ServerClient> ();
		this.disconnectList = new List <ServerClient> ();

		try
		{
			this.server = new TcpListener ( IPAddress.Any, port );
			this.server.Start ();

			isServerStarted = true;
			StartListening ();
		} catch ( Exception e )
		{
			Debug.Log ( "Socket error: " + e.Message );
		}

	}

	private void Update ()
	{
		if ( !this.isServerStarted )
			return;

		foreach ( ServerClient c in this.clients )
		{
			if ( !IsConnected ( c.tcp ) )
			{
				c.tcp.Close ();
				this.disconnectList.Add ( c );
				continue;
			} else
			{
				NetworkStream s = c.tcp.GetStream ();
				if ( s.DataAvailable )
				{
					StreamReader reader = new StreamReader ( s, true );
					string data = reader.ReadLine ();

					if ( data != null )
					{
						OnIncomingData ( c, data );
					}
				}
			}
		}

		for ( int i = 0; i < this.disconnectList.Count - 1; i++ )
		{
			this.clients.Remove ( this.disconnectList [i] );
			this.disconnectList.RemoveAt ( i );
		}
	}

	// this overload MIGHT not work the way it should.
	private void Broadcast ( string data, ServerClient sc )
	{
		try
		{
			StreamWriter writer = new StreamWriter ( sc.tcp.GetStream () );
			writer.WriteLine ( data );
			writer.Flush ();
		} catch ( Exception e )
		{
			Debug.Log ( "Write error: " + e.Message );
		}
	}

	private void Broadcast ( string data, List <ServerClient> cl )
	{
		foreach ( ServerClient sc in cl )
		{
			Broadcast ( data, sc );
		}
	}

	private void OnIncomingData ( ServerClient c, string data )
	{
		//Debug.Log ( "Server: " + data );

		string[] aData = data.Split ( '|' );
		switch ( aData[0] )
		{
			case "CWHO":
				c.clientName = aData [1];
				
				//TODO: assign role depending on this variable.
				c.IsHost = aData [2] == "1";
				
				Broadcast ( "SCON|" + c.clientName, this.clients );
				break;
			case "CSPELL":
				Broadcast ( "SSPELL|" + aData [1], this.clients );
				break;
			case "CSPELLR":
				Broadcast ( "SSPELLR|" + aData [1] + "|" + aData [2], this.clients );
				break;
		}
	}

	private bool IsConnected ( TcpClient c )
	{
		try
		{
			if ( c != null && c.Client != null && c.Client.Connected )
			{
				if ( c.Client.Poll ( 0, SelectMode.SelectRead ) )
					return !( c.Client.Receive ( new byte[1], SocketFlags.Peek ) == 0 );

				return true;
			}
		} catch ( Exception e )
		{
			return false;
		}

		return true;
	}

	private void StartListening ()
	{
		this.server.BeginAcceptTcpClient ( AcceptTcpClient, this.server );
	}

	private void AcceptTcpClient ( IAsyncResult ar )
	{
		TcpListener listener = ( TcpListener ) ar.AsyncState;

		string allUsers = "";

		foreach ( ServerClient client in this.clients )
		{
			allUsers += client.clientName + "|";
		}
		
		ServerClient sc = new ServerClient ( listener.EndAcceptTcpClient ( ar ) );

		this.clients.Add ( sc );

		//Debug.Log ( "Somebody has connected !" );

		Broadcast ( "SWHO|" + allUsers, this.clients [this.clients.Count - 1] );
		
		// this should give us the ip of the person that just connected.
		Debug.Log ( "Connected! ip : " + ( ( IPEndPoint ) sc.tcp.Client.RemoteEndPoint ).Address );

		if ( this.clients.Count == 2 )
		{
			Broadcast ( "SSTART", this.clients );
		} else
		{
			StartListening ();
		}
	}

}

public class ServerClient
{

	public string clientName;
	public bool IsHost;
	public TcpClient tcp;

	//public GameClient Client;
	
	public ServerClient ( TcpClient tcp )
	{
		this.tcp = tcp;
	}

}