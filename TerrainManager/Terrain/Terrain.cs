using Godot;
using System;

public partial class Terrain : Node2D
{

	private void EmitExitedScreen()
	{
		//Call the "I left the screen" function of TerrainManager
		GetParent<TerrainManager>().TerrainLeftScreen();
	}
}
