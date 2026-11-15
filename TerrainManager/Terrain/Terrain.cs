using Godot;
using System;

public partial class Terrain : Node2D
{
    public override void _Ready()
    {
		//Possible Chunks to pick from
        int total = DirAccess.GetFilesAt("res://TerrainManager/Terrain/TerrainChunks/").Length;
		//Pick a chunk
		var chunk = ResourceLoader.Load<PackedScene>("res://TerrainManager/Terrain/TerrainChunks/chunk_" + (GD.Randi() % total + 1) + ".tscn").Instantiate();
		//Set OffScreenNotifier Bounds

		//Add to Tree
		AddChild(chunk);
    }

    private void EmitExitedScreen()
	{
		//Call the "I left the screen" function of TerrainManager
		GetParent<TerrainManager>().TerrainLeftScreen();
	}
}
