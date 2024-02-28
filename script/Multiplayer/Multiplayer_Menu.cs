using Godot;
using System;

public partial class Multiplayer_Menu : Control
{
	private LineEdit _TBServerPort;
	private CheckBox _CBServerIsPlayer;
	private CheckBox _CBServerIsNotPlayer;

	private LineEdit _TBClientServersIP;
	private LineEdit _TBClientServersPort;

	public override void _Ready()
	{
		_TBServerPort = GetNode<LineEdit>("Eingabe_Server_Port");
		_CBServerIsPlayer = GetNode<CheckBox>("CB_ServerIsSpieler");
		_CBServerIsNotPlayer = GetNode<CheckBox>("CB_ServerIsNotSpieler");

		_TBClientServersIP = GetNode<LineEdit>("Eingabe_IPAdresse");
		_TBClientServersPort = GetNode<LineEdit>("Eingabe_Client_Port");

		_CBServerIsPlayer.ButtonPressed = true;
	}

	public void _on_cb_server_is_spieler_pressed()
	{
		_CBServerIsNotPlayer.ButtonPressed = false;
        _CBServerIsPlayer.ButtonPressed = true;
    }

	public void _on_cb_server_is_not_spieler_pressed()
	{
		_CBServerIsPlayer.ButtonPressed = false;
        _CBServerIsNotPlayer.ButtonPressed = true;
    }

}
