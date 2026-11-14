using Godot;
using System;

public partial class Globals : Node
{
	static float PlayerSpeedX = 0;

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

}
