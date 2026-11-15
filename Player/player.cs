using System;
using Godot;

public partial class player : CharacterBody2D
{
	//public float Speed = 1500.0f;
	public const float JumpVelocity = -400.0f;

    public static float SpeedThreshhold = 1000.0f;


    //noahs variables

    [Export]
    private Lose_Screen LoseScreen;

    [Export]
    public DistanceTraveled FeetTraveled;

    [Export]
	public Timer StuckUnderWaterTimer;

	public float TimeStuckUnderWater = 0.45f;

    /*
    [Export]
    public Timer GlidingCooldown;

    public float GlidingTime = 3.0f;

    [Export]
    public Timer GlidingDuration;
    */

    public const float MaxGlideEnergy = 2.5f;
    public float GlideEnergy = MaxGlideEnergy;


    public bool CanGlide = true;

    public bool IsGliding = false;
    public bool CanSwim = false;

    //probably change later, remember up is negative
    public const float WaterLevel = 150.0f;

	public float slowDescentAmount = 0.03f;


    //maybe change to different variable??
    public float UnderWaterMovementSpeed = JumpVelocity / 12.0f;

    public bool AboveWater = true;

    public float SimulatedPlayerPosition;

    //public float WaterLevelOffset = 2.0f;

    //end 

    public override void _Ready()
	{
        //LoseScreen.Position = this.Position;

        //GD.Print("ran ready");
        Vector2 velocity = new Vector2();
		velocity.Y = 0;
		Velocity = velocity;

        StuckUnderWaterTimer = GetNode<Timer>("UnderWaterTimer");
        StuckUnderWaterTimer.WaitTime = TimeStuckUnderWater;

        StuckUnderWaterTimer.Timeout += () =>
        {
            //GD.Print("TIMER FIRED");
            CanSwim = true;
        };

        /*
        GlidingCooldown = GetNode<Timer>("GlideCooldown");
        //maybe change duration variable later
        GlidingCooldown.WaitTime = GlidingTime;

        GlidingCooldown.Timeout += () =>
        {
            GD.Print("Cooldown finished");
            CanGlide = true;
        };


        GlidingDuration = GetNode<Timer>("GlideTime");
        GlidingDuration.WaitTime = GlidingTime;

        GlidingDuration.Timeout += () =>
        {
            GD.Print("Duration finished");
            IsGliding = false;
            CanGlide = false;
            GlidingCooldown.Start();
        };
        */
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		//Globals.SetPlayerSpeedX((float)2000);
		MoveAndSlide();

        // Add the gravity.
        //velocity += GetGravity() * (float)delta;

        //GD.Print("player y position:" + this.GlobalPosition.Y);
        //GD.Print(AboveWater);
        //GD.Print(WaterLevel - WaterLevelOffset);

        //GD.Print("player y position:" + this.GlobalPosition.Y);
        //GD.Print("player y position:" + this.GlobalPosition.Y);

        SimulatedPlayerPosition += (Globals.GetPlayerSpeedX() * (float)delta) / 1000;
        GD.Print("Player Speed: " + Globals.GetPlayerSpeedX());
        GD.Print("Player 'position': " + SimulatedPlayerPosition);

        GD.Print("FeetTraveled = " + FeetTraveled);
        FeetTraveled.Distance = (int)SimulatedPlayerPosition;


        //GD.Print("CanGlide: " + CanGlide);
        // GD.Print("GlideEnergy: " + GlideEnergy);


        if (this.GlobalPosition.Y <= WaterLevel)
		{

            //add boost when exiting water??
            if (!AboveWater)
            {
                //splash animation, looking at you here Tatiana

                velocity.Y += JumpVelocity / 2;
                //GD.Print("Jump Splash");

            }


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
            CanGlide = true;

            GlideEnergy = MaxGlideEnergy;

            //abrupt slow in descent
            //i'm lerping it
            velocity.Y = Mathf.Lerp(velocity.Y, 0.0f, slowDescentAmount);

            //once this has stop had timer until you can use UnderWaterMovement
            if (StuckUnderWaterTimer.IsStopped())
			{
                StuckUnderWaterTimer.Start();

            }

            //GD.Print(StuckUnderWaterTimer);
            //GD.Print("CanSwim: " + CanSwim);
			//GD.Print("StuckUnderWaterTimer:" + StuckUnderWaterTimer.IsStopped());
			//GD.Print("Time Left:" + StuckUnderWaterTimer.TimeLeft);

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


        if (Input.IsActionPressed("slow_down"))
        {
            Globals.SetPlayerSpeedX((float)0);
        }


		if (Globals.GetPlayerSpeedX() < SpeedThreshhold)
		{
			LoseScreen.ToggleScreen(true);

		}

	}
	private void GlideMovement(double delta, ref Vector2 velocity)
	{
        /*
        GD.Print("CanGlide: " + CanGlide);
        GD.Print(GlidingDuration);
        GD.Print(GlidingCooldown);
        GD.Print("GlidingDuration: " + GlidingDuration.IsStopped());
        GD.Print("GlidingCooldown: " + GlidingCooldown.IsStopped());
        GD.Print("Glyding Time Left: " + GlidingDuration.TimeLeft);
        GD.Print("Glyding Cooldown Timet: " + GlidingDuration.TimeLeft);
        */

        if (CanGlide && GlideEnergy > 0)
        {   
            /*
            if (GlidingDuration.IsStopped())
            {
                GlidingDuration.Start();
            }
            */

            //IsGliding = true;
            //GlidingDuration.Start();

            velocity.Y = Mathf.Min(velocity.Y, 100.0f);
            GD.Print("start glide");

            GlideEnergy -= (float)delta;

        }
        if (GlideEnergy <= 0)
        {
            GlideEnergy = 0;
            CanGlide = false;
        }

        /*
        if (IsGliding)
        {
            velocity.Y = Mathf.Min(velocity.Y, 100.0f);
            GD.Print("gliding");
        }
        */
        //GlidingDuration.Start();


        //add cooldown


        //velocity.Y = Mathf.Min(velocity.Y, 100.0f);
        //GD.Print("gliding");

        //velocity += GetGravity() * slowDescentAmount * (float)delta;
        //GD.Print("ran GlideMovement");
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
        //GD.Print("ran UnderWaterMovement");
    }
}

	


