using Godot;
using System;

public class Spider : KinematicBody2D
{
	
	private RayCast2D front_check;
	private RayCast2D low_mid_check;
	private RayCast2D high_mid_check;
	private RayCast2D back_check;

	[Export] private int x_speed = 20;
	[Export] private int y_speed = 40;
	
	[Export] private float step_rate = (float) 0.6;
	private float time_since_last_step = 0;
	private int cur_f_leg = 0;
	private int cur_b_leg = 0;
	private bool use_front = false;
	

	
	
	
	public override void _Ready()
	{
		front_check = GetNode<RayCast2D>("FrontCheck");
		low_mid_check = GetNode<RayCast2D>("LowMidCheck");
		high_mid_check = GetNode<RayCast2D>("HighMidCheck");
		back_check = GetNode<RayCast2D>("BackCheck");

		
		
		
		front_check.ForceRaycastUpdate();
		back_check.ForceRaycastUpdate();
		for (int i = 0; i < 8; i++)
		{
			step();
		}
	}


	private void step()
	{
		var front_legs = GetNode<Node2D>("FrontLegs").GetChildren();
		var back_legs = GetNode<Node2D>("BackLegs").GetChildren();
		
		Leg leg = null;
		RayCast2D sensor = null;
		if (use_front)
		{
			leg = (Leg) front_legs[cur_f_leg];
			cur_f_leg += 1;
			cur_f_leg %= front_legs.Count;
			sensor = front_check;
		}
		else
		{
			leg = (Leg) back_legs[cur_b_leg];
			cur_b_leg += 1;
			cur_b_leg %= back_legs.Count;
			sensor = back_check;
		}
		
		use_front = !use_front;

		var target = sensor.GetCollisionPoint();
		leg.step(target);



	}

	public override void _PhysicsProcess(float delta)
	{
		var move_vec = new Vector2(x_speed,0);
		if (high_mid_check.IsColliding())
		{
			move_vec.y = -y_speed;
		}
		else if (!low_mid_check.IsColliding())
		{
			move_vec.y = y_speed;
		}

		MoveAndCollide(move_vec * delta);
	}

	public override void _Process(float delta)
	{
		
		time_since_last_step += delta;
		if (time_since_last_step >= step_rate)
		{
			time_since_last_step = 0;
			step();
		}
	}
}
