using System;
using Godot;

public partial class player : CharacterBody2D
{
	public const float Speed = 1500.0f;
	public const float JumpVelocity = -400.0f;

    [Export]
    private lose_screen _loseScreen;

    Vector2 stoppedMoving = new Vector2(0, 0);

    public override void _Ready()
	{
		Vector2 velocity = new Vector2();
		velocity.Y = -2000;
		Velocity = velocity;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{

            if (Input.IsActionPressed("Glyde") && velocity.Y > 0)
            {
                //GD.Print("before: " + velocity.Y);

                //velocity += GetGravity() * 0.5f * (float)delta;

                //GD.Print("after: " + velocity.Y);
                glydeFall(delta, ref velocity);
            }
            else
			{

                velocity += GetGravity() * (float)delta;

            }
        }

		// Handle Jump.
		if (Input.IsActionJustPressed("jump"))
		{
			velocity.Y += JumpVelocity;
		}


		
		velocity.X = Speed;

		Velocity = velocity;
		MoveAndSlide();

		//Vector2 stoppedMoving = new Vector2(0, 0);
		if (this.Velocity == stoppedMoving)
		{
            GD.Print("stopped moving");
            _loseScreen.ToggleScreen(true);
        }
	}

    //somekind of flying/glyding method
    public void glydeFall(double delta, ref Vector2 velocity)
	{

		//cooldown

        GD.Print("before: " + velocity.Y);

        velocity += GetGravity() * 0.2f * (float)delta;

        GD.Print("after: " + velocity.Y);
    }

}
