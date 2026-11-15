using Godot;
using System;

public partial class EnemySpawner : Node2D
{

	[Export]
    PackedScene[] enemySpawns;

	[Export]
    Timer timer;

	[Export]
    float timerIntervul;

	[Export]
	int maxDepth = 500; // an ofset from zero of where certain enemies can spawn

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        timer.WaitTime = timerIntervul;
        timer.Timeout += OnEnd;
    }

    public void OnEnd()
    {
        int enemyidx = GD.RandRange(0, enemySpawns.Length-1);

        Node2D spawned = (Node2D)(enemySpawns[enemyidx].Instantiate());
        spawned.Position = new Vector2(0, GD.RandRange(0, maxDepth));

        AddChild(spawned);

    }

}
