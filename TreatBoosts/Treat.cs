using Godot;
using System;

public partial class Treat : Area2D
{
    // oop my dependancies are wack im using an older version of dotnet 

    // an export for the ammount of speed bost it gives so cn be used on all 3 types
    // a Rotate in the process to make them look more fun
    // get child smth to only rotate the sprite and not the area
    // on enter it frees itself

    [Export]
    float speedBostPercent = 1.08f;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Left * 10 * new Vector2(Globals.GetPlayerSpeedX() / 1500, 0); // whatever the starting speed is 
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is player)
        {
            GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
            Globals.SetPlayerSpeedX(Globals.GetPlayerSpeedX() * speedBostPercent); // make an export
            QueueFree();
        }

        // play animation

    }
}
