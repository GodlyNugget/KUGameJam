using Godot;
using System;

public partial class Boat : Area2D
{
	[Export]
	Lose_Screen loose;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        BodyEntered += OnBodyEntered;
        Globals.SetBoatSpeedX(300);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 newPos = Position;

		Globals.SetBoatSpeedX( Globals.GetBoatSpeedX() + 1.5f);
		float distannce = Mathf.Lerp(-500, 500, Globals.GetBoatSpeedX()*.5f / Globals.GetPlayerSpeedX());

		newPos.X = distannce;
		Position = newPos;

		// on colide game over
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is player)
        {
			loose.ToggleScreen(true);
			Globals.SetBoatSpeedX(300);
			Globals.SetPlayerSpeedX(1500);
        }
        //
    }

}
