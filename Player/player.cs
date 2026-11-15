using System;
using Godot;

public partial class Player : CharacterBody2D
{
	public const float Speed = 2000.0f;
	public const float JumpVelocity = -400.0f;

    public static float SpeedThreshhold = 1000;


	//noahs variables
    private lose_screen LoseScreen;

	[Export]
	public Timer StuckUnderWaterTimer;

	public float TimeStuckUnderWater = 0.5f;

    public bool CanSwim = false;

    //probably change later, remember up is negative
    public const float WaterLevel = 150;

	public float slowDescentAmount = 0.025f;


    //maybe change to different variable??
    public float UnderWaterMovementSpeed = JumpVelocity / 10;

    public bool AboveWater = true;

	//end 

	public override void _Ready()
	{
		//GD.Print("ran ready");
		Vector2 velocity = new Vector2();
		velocity.Y = 0;
		Velocity = velocity;


        StuckUnderWaterTimer = GetNode<Timer>("StuckUnderWaterTimer");
        StuckUnderWaterTimer.WaitTime = TimeStuckUnderWater;

        StuckUnderWaterTimer.Timeout += () =>
        {
            GD.Print("TIMER FIRED");
            CanSwim = true;
        };

    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Globals.SetPlayerSpeedX((float)2000);
		MoveAndSlide();

		// Add the gravity.
		//velocity += GetGravity() * (float)delta;

		GD.Print("player y position:" + this.GlobalPosition.Y);

		if (this.GlobalPosition.Y <= WaterLevel)
		{

			//add boost when exiting water??

			AboveWater = true;

            velocity += GetGravity() * (float)delta;

            CanSwim = false;
            StuckUnderWaterTimer.Stop();

            //put this inside of GlideMovement???
            if (Input.IsActionPressed("Glide"))
			{
				//gilde action
				GlideMovement(delta, ref velocity);


            }
		}
		else 
		{
            //underwater stuff 
            AboveWater = false;

            //abrupt slow in descent
			//i'm lerping it
            velocity.Y = Mathf.Lerp(velocity.Y, 0.0f, slowDescentAmount);

            //once this has stop had timer until you can use UnderWaterMovement
            if (StuckUnderWaterTimer.IsStopped())
			{
                StuckUnderWaterTimer.Start();

            }

            //GD.Print(StuckUnderWaterTimer);
            GD.Print("CanSwim: " + CanSwim);
			GD.Print("StuckUnderWaterTimer:" + StuckUnderWaterTimer.IsStopped());
			GD.Print("Time Left:" + StuckUnderWaterTimer.TimeLeft);

            if (CanSwim)
			{
                StuckUnderWaterTimer.Stop();
                UnderWaterMovement(delta, ref velocity);
            }
            
		}

		// Handle Jump.
		//	remove/edit later
		if (Input.IsActionJustPressed("jump"))
		{
			velocity.Y += JumpVelocity;
		}
		Velocity = velocity;



		if (Globals.GetPlayerSpeedX() < SpeedThreshhold)
		{
			LoseScreen.ToggleScreen(true);

		}

	}
	private void GlideMovement(double delta, ref Vector2 velocity)
	{

        velocity.Y = Mathf.Min(velocity.Y, 100.0f); 

        //velocity += GetGravity() * slowDescentAmount * (float)delta;
		GD.Print("ran GlideMovement");
	}

	private void UnderWaterMovement(double delta, ref Vector2 velocity)
	{

		//needs some balancing but works
        //underwater movement
        if (Input.IsActionPressed("jump") || Input.IsActionPressed("move_up"))
        {
			velocity.Y += UnderWaterMovementSpeed;
        }
		else if (Input.IsActionPressed("move_down"))
		{
			velocity.Y -= UnderWaterMovementSpeed;
        }
        GD.Print("ran UnderWaterMovement");
    }

}

	


