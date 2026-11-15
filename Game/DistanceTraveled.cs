using Godot;
using System;

public partial class DistanceTraveled : Control
{
    // Called when the node enters the scene tree for the first time.

    [Export]
    public Label _distanceTraveled;

    public float Distance
    {
        set
        {
            _distanceTraveled.Text = value + " Feet";
        }
    }
}
