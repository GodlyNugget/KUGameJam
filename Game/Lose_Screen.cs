using Godot;
using System;

public partial class Lose_Screen : Control
{
    [Export]
    private Button _restartButton;
    public override void _Ready()
    {
        //this.Position = 
        this.ProcessMode = ProcessModeEnum.Always;

        ToggleScreen(false);

        _restartButton.Pressed += RetryGame;
    }

    public void ToggleScreen(bool shouldEnable)
    {
        this.Visible = shouldEnable;
        GetTree().Paused = shouldEnable;
    }


    private void RetryGame()
    {
        GetTree().ReloadCurrentScene();
        Globals.SetPlayerSpeedX(1500);
        Globals.SetBoatSpeedX(1500);

    }

    // Called when the node enters the scene tree for the first time.



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

}