using Godot;
using System;

public partial class TerrainManager : Node2D
{	
	Terrain CurrentTerrain;
	Terrain NextTerrain;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CurrentTerrain = PickNextTerrain();
		AddChild(CurrentTerrain);
		NextTerrain = PickNextTerrain();
		NextTerrain.Position = new Vector2(1254, 0);
		AddChild(NextTerrain);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float PlayerSpeed = Globals.GetPlayerSpeedX();

		//Move Floor1 and Floor2, repeating as needed
		CurrentTerrain.Position -= new Vector2(PlayerSpeed * (float)delta, 0);
		NextTerrain.Position -= new Vector2(PlayerSpeed * (float)delta, 0);
	}

	public void TerrainLeftScreen()
	{
		CurrentTerrain = NextTerrain;
		NextTerrain = PickNextTerrain();
		AddChild(NextTerrain);
	}

	private Terrain PickNextTerrain()
	{
		return (Terrain)ResourceLoader.Load<PackedScene>("res://TerrainManager/Terrain/terrain.tscn").Instantiate();
	}
}
