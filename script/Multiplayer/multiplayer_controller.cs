using Godot;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TowerDefense;

public partial class multiplayer_controller : Control
{
	private int _port = 8910;				// Todo:Port muss man selbst eingeben können
	private string _IP_adress = "127.0.0.1";// Todo:Port muss man selbst eingeben können
	private ENetMultiplayerPeer _peer;
	private string _name;
	private ItemList _Chat;



    public override void _Ready()
	{
		Multiplayer.PeerConnected += PeerConnected;
		Multiplayer.PeerDisconnected += PeerDisconnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
        Multiplayer.ConnectionFailed += ConnectionFailed;
		_Chat = GetNode<ItemList>("Chat");
    }

	
	public override void _Process(double delta)
	{
		if (Chat_API.NewMsgAvailable == true)
			_Chat.AddItem(Chat_API.Chat_Archiev.First(x=>x.msg==Chat_API.NewMsg).AuthorName+": " + Chat_API.NewMsg);
	}

	//nur auf Clientseite
	private void ConnectionFailed()
	{
		GD.Print("Verbindung fehlgeschlagen!");
	}

	//nur auf Clientseite
	private void ConnectedToServer()
	{
        GD.Print("Verbindung zum Server erfolgreich!");
		RpcId(1, "sendPlayerInformation", _name, Multiplayer.GetUniqueId());//RPC wird nur von der ID 1, vom Server ausgeführt,denk ich
    }

	//id des Spielers welcher den Verbindungsabbruch hat
	private void PeerDisconnected(long id)
	{
        GD.Print("Verbindung zum Spieler: "+id.ToString()+ "abgebrochen!");
    }

	//id de Spielers welcher sich verbunden hat
	private void PeerConnected(long id)
	{
        GD.Print("Spieler: "+id.ToString()+"verbunden!");
    }

	public void _on_start_server_pressed()
	{
		_peer = new ENetMultiplayerPeer();
		var error = _peer.CreateServer(_port, 2);//max. 2 Spieler
		if(error != Error.Ok)
		{
			GD.Print("Fehler, kann nicht Server starten!");
			return;
		}
		_peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);

		Multiplayer.MultiplayerPeer = _peer; //Server spielt beim Spiel mit, denke ich

		GD.Print("Warte auf Spieler");
		//sendPlayerInformation(_name, 1);//weil Server id von 1 hat
        _name = GetNode<LineEdit>("Eingabe_Name").Text;
    }

	public void _on_join_server_pressed()
	{
		_peer=new ENetMultiplayerPeer();
		_peer.CreateClient(_IP_adress, _port);

		_peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = _peer;
		GD.Print("Trete Spiel bei");
        _name = GetNode<LineEdit>("Eingabe_Name").Text;
    }

	public void _on_start_chat_pressed()
	{
		foreach(var item in GameManager.Players)
		{
			GD.Print(item.Name);
		}
		Rpc("StartChat");
	}

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void StartChat()
	{
		GD.Print("Chat gestartet!");
	}

    [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
    private void sendPlayerInformation(string name, int id)
	{
		PlayerInfo playerInfo = new PlayerInfo(name,id,PlayerRole.Attacker); //ToDo: Rollen festlegen
		if(!GameManager.Players.Contains(playerInfo))
			GameManager.Players.Add(playerInfo);
		if(Multiplayer.IsServer())
		{
			foreach(var item in GameManager.Players)
			{
				Rpc("sendPlayerInformation", name, id);
			}
		}
	}

	public void _on_senden_pressed()
	{
		//Rpc("Chat_API.SendNewMsg", _name, GetNode<LineEdit>("Nachricht").Text);
		Rpc("send", _name, GetNode<LineEdit>("Nachricht").Text);//muss man so beschissen lösen, da 1. Version nicht geht
	}
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)] //CallLocal, wichtig, denn da wird Methode auf beiden aufgerufen wenn nicht rufen die Instanzen die jeweils andere Methode auf
    private void send(string a, string n)
	{
		Chat_API.SendNewMsg(a, n);
	}

}


//https://www.youtube.com/watch?v=MOJVjFngOs4&t=5249s
//bis 31:28