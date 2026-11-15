using Godot;
using System;

public partial class Terrain : Node2D
{
    public override void _Ready()
    {
		//Possible Chunks to pick from
        int total = DirAccess.GetFilesAt("res://TerrainManager/Terrain/TerrainChunks/").Length;
		//Pick a chunk
		int chunkNum = (int)(GD.Randi() % total + 1);
		var chunk = ResourceLoader.Load<PackedScene>("res://TerrainManager/Terrain/TerrainChunks/chunk_" + chunkNum + ".tscn").Instantiate();
		//Add to Tree
		AddChild(chunk);
		//Set OffScreenNotifier Bounds
		VisibleOnScreenEnabler2D enabler = GetNode<VisibleOnScreenEnabler2D>("VisibleOnScreenEnabler2D");
		Rect2 originalRect = enabler.Rect;
		Vector2 newSize = new Vector2(GetNode<Node2D>("Chunk" + chunkNum + "/Boundaries/TopRight").Position.X, originalRect.Size.Y);
		Rect2 newRect = new Rect2(originalRect.Position, newSize);
		enabler.Rect = newRect;
    }

	public void SetNextPosition(Terrain curr)
	{
		//Gets the x position of the Current Terrain's right boundary. Used to put the Next Terrain after the Current Terrain
		Position = new Vector2(curr.GetChild(1).GetNode<Node2D>("Boundaries/TopRight").Position.X, 0);
	}

    private void EmitExitedScreen()
	{
		//Call the "I left the screen" function of TerrainManager
		GetParent<TerrainManager>().TerrainLeftScreen();
	}
}
