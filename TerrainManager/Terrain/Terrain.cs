using Godot;
using System;

public partial class Terrain : Node2D
{

	private void EmitExitedScreen()
	{
		GetParent<TerrainManager>().TerrainLeftScreen();
	}
}
