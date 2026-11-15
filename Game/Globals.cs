using Godot;
using System;

public partial class Globals : Node
{
	static float PlayerSpeedX = 1500;
	static float BoatSpeedX = 300;

	public static Globals Instance {get; private set;}
	public override void _Ready()
	{
		Instance = this;
	}

    public static float GetPlayerSpeedX()
    {
        return PlayerSpeedX;
    }
	public static void SetPlayerSpeedX(float newPlayerSpeedX)
    {
        PlayerSpeedX = newPlayerSpeedX;
    }

    // constantly goes up at a rate 
    public static float GetBoatSpeedX()
    {
        return BoatSpeedX;
    }
    public static void SetBoatSpeedX(float newsBoatSpeedY)
    {
        BoatSpeedX = newsBoatSpeedY;
    }

}
