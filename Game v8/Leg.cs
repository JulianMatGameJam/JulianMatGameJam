using Godot;
using System;

public class Leg : Position2D
{
	
	private int MIN_DIST = 100;

	private int len_upper ;
	private int len_middle ;
	private int len_lower ;

	private Position2D joint1;
	private Position2D joint2;
	private Position2D hand;
	
	[Export] private bool flipped = true;

	private Vector2 goal_pos;
	private Vector2 int_pos;
	private Vector2 start_pos;
	private int step_height = 40;
	private float step_rate = (float) 0.5;
	private float step_time = (float) 0.0;
	
	
	
	public override void _Ready()
	{
		joint1 = GetNode<Position2D>("Joint1");
		joint2 = GetNode<Position2D>("Joint1/Joint2");
		hand = GetNode<Position2D>("Joint1/Joint2/Hand");

		len_upper = (int) joint1.Position.x;
		len_middle = (int) joint2.Position.x;
		len_lower = (int) hand.Position.x;


		if (flipped)
		{
			GetNode<Sprite>("Sprite").FlipH = true;
			joint1.GetNode<Sprite>("Sprite").FlipH = true;
			joint2.GetNode<Sprite>("Sprite").FlipH = true;
		}
	}

	public void step(Vector2 g_pos)
	{
		if (goal_pos != g_pos)
		{
			goal_pos = g_pos;
			var hand_pos = hand.GlobalPosition;
			var highest = goal_pos.y;

			if (hand_pos.y < highest)
			{
				highest = hand_pos.y;
			}

			float mid =  (goal_pos.x + hand_pos.x) / 2;
			start_pos = hand_pos;
			int_pos = new Vector2(mid,highest-step_height);
			step_time = (float) 0.0;

		}
	}
	
	private void update_ik(Vector2 target_pos)
	{

		Vector2 offset = target_pos - GlobalPosition;
		float dis_to_tar = offset.Length();
		if (dis_to_tar < MIN_DIST)
		{
			offset = (offset / dis_to_tar) * MIN_DIST;
			dis_to_tar = MIN_DIST;
		}

		float base_r = offset.Angle();
		var len_total = len_upper + len_middle + len_lower;
		var len_dummy_side = (len_upper + len_middle) * Mathf.Clamp(  (dis_to_tar / len_total), 0, 1);

		var base_angles = calc(len_dummy_side, len_lower, dis_to_tar);
		var next_angles = calc(len_upper, len_middle, len_dummy_side);
		
		

		GlobalRotation = base_angles.Item2 + next_angles.Item2 + base_r;
		joint1.Rotation = next_angles.Item3;
		joint2.Rotation = next_angles.Item3 + next_angles.Item1;


	}
	
	private float law_of_cos(float a, float b, float c)
	{
		
		if (2*a*b == 0)
		{
			return 0;
		}
		else
		{
			return (float) Math.Acos((a * a + b * b - c * c) / (2 * a * b));
		}

		
	}

	private Tuple<float,float,float> calc(float side_a, float side_b, float side_c)
	{
		Tuple<float,float,float> tuple = new Tuple<float, float, float>(0,0,0);
		
		if (side_c >= side_a + side_b)
		{
			return tuple;

		}

		float angle_a = law_of_cos(side_b, side_c, side_a);
		float angle_b = (float) (law_of_cos(side_c, side_a, side_b) + Math.PI);
		float angle_c = (float) (Math.PI - angle_a - angle_b);

		if (flipped)
		{
			angle_a = -angle_a;
			angle_b = -angle_b;
			angle_c = -angle_c;
		}
		
		Tuple<float,float,float> tuple2 = new Tuple<float, float, float>(angle_a,angle_b,angle_c);

		
		return tuple2;
	}


  public override void _Process(float delta)
  {
	  step_time += delta;
	  var target_pos = new Vector2(200,0);

	  var t = step_time / step_rate;
	  if (t < 0.5)
	  {
		  target_pos = start_pos.LinearInterpolate(int_pos, (float) (t / 0.5));
	  }
	  else if (t <1.0)
	  {
		  target_pos = int_pos.LinearInterpolate(goal_pos, (float) ((t - 0.5) / 0.5));
	  }
	  else
	  {
		  target_pos = goal_pos;
	  }
	  update_ik(target_pos);
  }
}
