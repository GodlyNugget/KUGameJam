using Godot;
using System;

public partial class Enemy : Area2D
{
    [Export]
    private Vector2 MoveDirection = Vector2.Left;

    [Export]
    private float MoveSpeed = 10;

    [Export]
    private float SpeedDecrement = 10; // what the speed would be subtracted by

    [Export]
    private CanvasItem canvas;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        BodyEntered += OnBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += MoveDirection * MoveSpeed + new Vector2(Globals.GetPlayerSpeedX()/1500, 0); // whatever the starting speed is 
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is player)
        {
            Globals.SetPlayerSpeedX(Globals.GetPlayerSpeedX() - SpeedDecrement);
            canvas.Modulate = new Color(1, 1, 1, 1);
        }
        else // if the despawn wall
        {
            
        }
        
        // play animation

    }


}
