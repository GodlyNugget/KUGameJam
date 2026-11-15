using Godot;
using System;

public partial class TerrainManager : Node2D
{	
	//Pointers to the Terrains in use
	Terrain CurrentTerrain;
	Terrain NextTerrain;

	int terrainCounter = 0;

	[Export]
	int winTerrainAmmount = 3; // after completing 3 biomes toogle win screen

	[Export]
	private WinScreen winScreen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		//Set up initial terrains
		CurrentTerrain = PickNextTerrain();
		AddChild(CurrentTerrain);
		NextTerrain = PickNextTerrain();
		NextTerrain.SetNextPosition(CurrentTerrain);
		AddChild(NextTerrain);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Move Terrains left
		CurrentTerrain.Position -= new Vector2((Globals.GetPlayerSpeedX() /1500) , 0) * 10;
		NextTerrain.Position -= new Vector2((Globals.GetPlayerSpeedX() / 1500) , 0) * 10;
	}

	//Called by Terrain scene when leaving the screen
	public void TerrainLeftScreen()
	{
		//Delete old Terrain
		CurrentTerrain.QueueFree();
		//Create new Terrain
		CurrentTerrain = NextTerrain;
		NextTerrain = PickNextTerrain();
		//Set the new Terrain's position based on the width of the current one
		NextTerrain.SetNextPosition(CurrentTerrain);
		AddChild(NextTerrain);

		terrainCounter++;
		if (winTerrainAmmount == terrainCounter)
		{
			winScreen.ToggleScreen(true);
		}

    }

	private Terrain PickNextTerrain()
	{
		//Create a new Terrain
		return (Terrain)ResourceLoader.Load<PackedScene>("res://TerrainManager/Terrain/terrain.tscn").Instantiate();
	}
}
