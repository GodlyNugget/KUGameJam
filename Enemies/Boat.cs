using Godot;
using System;

public partial class Boat : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 newPos = Position;

		Globals.SetBoatSpeedX( Globals.GetBoatSpeedX() + 3);
		float distannce = Mathf.Lerp(0, 200, Globals.GetBoatSpeedX()*.5f / Globals.GetPlayerSpeedX());

		newPos.X = distannce;
		Position = newPos;

		// on colide game over
    }

}
