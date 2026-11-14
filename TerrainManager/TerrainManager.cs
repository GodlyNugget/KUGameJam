using Godot;
using System;

public partial class TerrainManager : Node2D
{	
	//Pointers to the Terrains in use
	Terrain CurrentTerrain;
	Terrain NextTerrain;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Set up initial terrains
		CurrentTerrain = PickNextTerrain();
		AddChild(CurrentTerrain);
		NextTerrain = PickNextTerrain();
		//The 1254 is hardcoded for the width of the current terrains. Will need to change for variating terrain sizes.
		NextTerrain.Position = new Vector2(1254, 0);
		AddChild(NextTerrain);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Move Terrains left
		CurrentTerrain.Position -= new Vector2(Globals.GetPlayerSpeedX() * (float)delta, 0);
		NextTerrain.Position -= new Vector2(Globals.GetPlayerSpeedX() * (float)delta, 0);
	}

	//Called by Terrain scene when leaving the screen
	public void TerrainLeftScreen()
	{
		//Delete old Terrain
		CurrentTerrain.QueueFree();
		//Create new Terrain
		CurrentTerrain = NextTerrain;
		NextTerrain = PickNextTerrain();
		NextTerrain.Position = new Vector2(1254, 0);
		AddChild(NextTerrain);
	}

	private Terrain PickNextTerrain()
	{
		//Create a new Terrain. Terrain scene should randomly pick a level design
		return (Terrain)ResourceLoader.Load<PackedScene>("res://TerrainManager/Terrain/terrain.tscn").Instantiate();
	}
}
