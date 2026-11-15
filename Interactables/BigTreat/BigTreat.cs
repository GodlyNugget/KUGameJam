using Godot;
using System;

public partial class BigTreat : Area2D
{
	private void action(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			Globals.SetPlayerSpeedX(Globals.GetPlayerSpeedX() * (float)1.05);
		}
	}
}
