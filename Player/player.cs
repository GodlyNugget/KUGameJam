using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 2000.0f;
	public const float JumpVelocity = -400.0f;
    public static Vector2 WaterLevel = new Vector2(0, 0);

	private lose_screen LoseScreen;

    public static float SpeedThreshhold = 1000;

    public override void _Ready()
	{
		Vector2 velocity = new Vector2();
		velocity.Y = 0;
		Velocity = velocity;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

        Globals.SetPlayerSpeedX((float)2000);
        MoveAndSlide();

        // Add the gravity.
        //velocity += GetGravity() * (float)delta;

        if (this.GlobalPosition.Y > WaterLevel.Y)
		{
			if (Input.IsActionJustPressed("glyde"))
			{
				//gylde action
			}
        }
		else
		{
			//underwater movement
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump"))
		{
			velocity.Y += JumpVelocity;
		}
        Velocity = velocity;

		if (Globals.GetPlayerSpeedX() < SpeedThreshhold)
		{
			LoseScreen.ToggleScreen(true);

        }

        //gylde action method


        //underwater movement method



    }
}
