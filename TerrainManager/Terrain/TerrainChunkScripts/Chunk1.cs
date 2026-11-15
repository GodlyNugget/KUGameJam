using Godot;
using System;
using System.Collections.Generic;

public partial class Chunk1 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Decide & Set Chunk Width
		int chunkWidth = (int)(GD.Randi() % 4000 + 8000);
		Vector2 newPos = new Vector2(chunkWidth, GetNode<Node2D>("Boundaries/TopRight").Position.Y);
		GetNode<Node2D>("Boundaries/TopRight").Position = newPos;
		newPos = new Vector2(chunkWidth, GetNode<Node2D>("Boundaries/BottomRight").Position.Y);
		GetNode<Node2D>("Boundaries/BottomRight").Position = newPos;

		//Decide Obstacle Count (Bigger chunk = more obstacles)
		int obstacleCount = (int)(GD.Randi() % (chunkWidth / 1000) + 10);

		//Create Obstacles
		List<int> positions = new List<int>();
		int lowestX = 0;
		int highestX = chunkWidth;
		int lowestY = (int)GetNode<Node2D>("Boundaries/TopRight").Position.Y;
		int highestY = (int)GetNode<Node2D>("Boundaries/BottomRight").Position.Y;

		GD.Print(chunkWidth);

		Vector2 newPosition = new Vector2((int)(GD.Randi() % highestX + lowestX), (int)(GD.Randi() % 100 + highestY - 100));
		AnimatedSprite2D newObstacle = (AnimatedSprite2D)GetNode<AnimatedSprite2D>("ObstacleBase").Duplicate();;

		for (int i = 0; i < obstacleCount; i++)
		{
			bool repeat = true;
			while (repeat)
			{
				repeat = false;
				newPosition = new Vector2((int)(GD.Randi() % highestX + lowestX), (int)(GD.Randi() % 100 + highestY - 100));
				newObstacle = (AnimatedSprite2D)GetNode<AnimatedSprite2D>("ObstacleBase").Duplicate();
				
				for (int j = 0; j < positions.Count; j++)
				{
					GD.Print(newPosition.X + " : " + positions[j]);
					if (newPosition.X > positions[j] - 200 && newPosition.X < positions[j] + 200)
					{
						repeat = true;
					}
				}
			}
			positions.Add((int)newPosition.X);
			AddChild(newObstacle);
			newObstacle.Position = newPosition;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
