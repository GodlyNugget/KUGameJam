using Godot;
using System;
using System.Collections.Generic;

public partial class Chunk2 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Decide & Set Chunk Width ----EVERY CHUNK WILL NEED THIS------
		int chunkWidth = (int)(GD.Randi() % 4000 + 8000);
		Vector2 newPos = new Vector2(chunkWidth, GetNode<Node2D>("Boundaries/TopRight").Position.Y);
		GetNode<Node2D>("Boundaries/TopRight").Position = newPos;
		newPos = new Vector2(chunkWidth, GetNode<Node2D>("Boundaries/BottomRight").Position.Y);
		GetNode<Node2D>("Boundaries/BottomRight").Position = newPos;

		//Decide Obstacle Count (Bigger chunk = more obstacles) ----EVERY CHUNK WILL NEED THIS------
		int obstacleCount = (int)(GD.Randi() % (chunkWidth / 1000) + 7);

		//Create Obstacles ----EVERY CHUNK WILL NEED THIS BUT DOESN'T NEED TO BE THIS EXACTLY------
		List<int> positions = new List<int>();
		int lowestX = 0;
		int highestX = chunkWidth;
		int lowestY = (int)GetNode<Node2D>("Boundaries/TopRight").Position.Y;
		int highestY = (int)GetNode<Node2D>("Boundaries/BottomRight").Position.Y;

		Vector2 newPosition = new Vector2((int)(GD.Randi() % highestX + lowestX), (int)(GD.Randi() % 75 + highestY - 75));
		AnimatedSprite2D newObstacle = (AnimatedSprite2D)GetNode<AnimatedSprite2D>("ObstacleBase").Duplicate();

		//Place Obstacles
		for (int i = 0; i < obstacleCount; i++)
		{
			bool repeat = true;
			while (repeat)
			{
				repeat = false;
				newPosition = new Vector2((int)(GD.Randi() % highestX + lowestX), (int)(GD.Randi() % 75 + highestY - 75));
				newObstacle = (AnimatedSprite2D)GetNode<AnimatedSprite2D>("ObstacleBase").Duplicate();
				if ((int)(GD.Randi() % 3) == 1) 
				{
					newObstacle.Play("Rock2");
				}
				
				for (int j = 0; j < positions.Count; j++)
				{
					//Check proximity to other objects
					if (newPosition.X > positions[j] - 275 && newPosition.X < positions[j] + 275)
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

	public void PlayerHitTerrain(Node2D body)
	{
		if (body is player)
		{
			GD.Print(Globals.GetPlayerSpeedX());
			Globals.SetPlayerSpeedX(Globals.GetPlayerSpeedX() - 5);
		}
	}
}
