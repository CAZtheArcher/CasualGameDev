using Godot;
using System;
using System.Collections.Generic;

public partial class Vacuum : Enemy
{
    private enum State {
        /// <summary>Moving towards player normally</summary>
        APPROACHING,
        /// <summary>Standing still and aiming a telegraph</summary>
        WINDING_UP,
        /// <summary>In the dash animation</summary>
        DASHING,
        /// <summary>Brief immobility after a dash</summary>
        COOLING_DOWN
    }
    private int dashInitiateRange; // Distance at which dashing begins
    private float dashVelocity; // Speed of the dash
    private float windUpTime; // Time the vacuum spends "aiming"
    // Placeholder until I figure out how to measure distance travelled and use
    // that to terminate the dash instead
    private float dashTime; 
    private float cooldownTime; // Time spent immobile after a dash
    private double timeSpentInCurrentState;
    private float turnSpeed; // In degrees, per second
    private float turnSpeedRadians; // In rad, per physupdate (1/60th of sec)

    private AnimatedSprite2D telegraph;

    State currentState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		base._Ready();
        damage = 3;
        velocity = 75f;
        
        dashInitiateRange = 150;
        dashVelocity = 400;
        windUpTime = 1f;
        dashTime = 0.5f;
        cooldownTime = 0.25f;
        turnSpeed = 60f;
        turnSpeedRadians = ((float)Math.PI / 180f * turnSpeed) / 60f;

        telegraph = (AnimatedSprite2D)GetNode("Telegraph");

        currentState = State.APPROACHING;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	}

    public override void _PhysicsProcess(double delta)
    {
        direction = (player.GlobalPosition - GlobalPosition).Normalized();
        Rotation = 0; // Prevents collsions with the player from spinning the enemy

        // This finite state machine is split into an if chain and a switch
        // So I can have some code run only once on State change
        // and so I can have some code run every frame while in a State
        if (player.GlobalPosition.DistanceTo(GlobalPosition) < dashInitiateRange
            && currentState == State.APPROACHING)
        {
            currentState = State.WINDING_UP;
            telegraph.LookAt(player.GlobalPosition);
            telegraph.Play();
            // Stretch the telegraph anim across the entire windup duration
            telegraph.SpeedScale = 1f / windUpTime;
            timeSpentInCurrentState = 0f;
        }
        else if (currentState == State.WINDING_UP && timeSpentInCurrentState > windUpTime)
        {
            currentState = State.DASHING;
            telegraph.Stop();
            timeSpentInCurrentState = 0f;
        }
        else if (currentState == State.DASHING && timeSpentInCurrentState > dashTime)
        {
            currentState = State.COOLING_DOWN;
            LinearVelocity = Vector2.Zero;
            timeSpentInCurrentState = 0f;
        }
        else if (currentState == State.COOLING_DOWN && timeSpentInCurrentState > cooldownTime)
        {
            currentState = State.APPROACHING;
            timeSpentInCurrentState = 0f;
        }

        switch (currentState)
        {
            case State.APPROACHING:
                Vector2 desired_velocity = direction * velocity;
                Vector2 velocity_diff = desired_velocity - LinearVelocity;
                Vector2 force = velocity_diff.LimitLength(velocity);
                ApplyCentralForce(force);
                telegraph.Visible = false;
                break;
            case State.WINDING_UP:
                LinearVelocity = Vector2.Zero; // Stop moving
                telegraph.Visible = true;
                
                if(telegraph.GetAngleTo(player.GlobalPosition) > 0.01f)
                    telegraph.GlobalRotation += turnSpeedRadians;
                else if (telegraph.GetAngleTo(player.GlobalPosition) < -0.01f)
                    telegraph.GlobalRotation += -turnSpeedRadians;
                timeSpentInCurrentState += delta;

                // Prevents the animation from looping back to frame 1 in case of lag
                if (timeSpentInCurrentState > windUpTime * 4 / 5)
                    telegraph.Pause(); 
                break;
            case State.DASHING:
                telegraph.Visible = false;
                Vector2 dashDirection = new Vector2(
                    (float)Math.Cos(telegraph.Rotation), 
                    (float)Math.Sin(telegraph.Rotation));
                desired_velocity = dashDirection * dashVelocity;
                velocity_diff = desired_velocity - LinearVelocity;
                force = velocity_diff.LimitLength(dashVelocity);
                ApplyCentralForce(force);
                timeSpentInCurrentState += delta;
                break;
            case State.COOLING_DOWN:
                LinearVelocity = Vector2.Zero;
                timeSpentInCurrentState += delta;
                break;

        }
        


    }
}
