using Godot;
using System;

public partial class TerrainManager : Node2D
{
	Node2D Floor1;
	Node2D Floor2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Floor1 = GetNode<Node2D>("Floor1");
		Floor2 = GetNode<Node2D>("Floor2");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float PlayerSpeed = Globals.GetPlayerSpeedX();

		//Move Floor1 and Floor2, repeating as needed
		Floor1.Position -= new Vector2(PlayerSpeed * (float)delta, 0);
		Floor2.Position -= new Vector2(PlayerSpeed * (float)delta, 0);
	}

	private void TerrainLeftScreen(bool FloorNum)
	{
		if (FloorNum)
		{
			Floor1.Position = new Vector2((float)1542, 0);
		} else if (!FloorNum)
		{
			Floor2.Position = new Vector2((float)1542, 0);
		}
	}
}
