using Godot;
using System;

    public partial class LoseScreen : Control
    {
        [Export]
        private Button _restartButton;

        public override void _Ready()
        {
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
        }

        // Called when the node enters the scene tree for the first time.



        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
