using Godot;
using System;

public class Player : KinematicBody2D
{

    public int Velocity = 200;
    public String animation = "Idle";
    AnimatedSprite animatedsprite;

    public override void _Ready()
    {
        animatedsprite = (AnimatedSprite)GetNode("AnimatedSprite");
    }
    public override void _PhysicsProcess(float delta){
        var motion = new Vector2();
        var up = new Vector2(0, -1);
        motion.x = (Input.GetActionStrength("ui_right")-Input.GetActionStrength("ui_left"))* Velocity;
        if(motion.x < 0){
            animatedsprite.FlipH = true;
            animation = "Run";
        }else if(motion.x > 0){
            animatedsprite.FlipH = false;
            animation = "Run";
        }else{
            animation = "Idle";
        }
        //--------------------------------------------------
        //--------------------------------------------------
        if(Input.IsActionPressed("Atack") && motion.x == 0){
            animation = "Atack";
        }else if(Input.IsActionPressed("Atack") && motion.x != 0){
            animation = "AtackRun";
        }
        else{
            animation = animation;
        }
        if(Input.IsActionPressed("Dash") && animatedsprite.FlipH == true){
            motion.x = -600;
            animation = "Dash";
        }else if(Input.IsActionPressed("Dash") && animatedsprite.FlipH == false){
            motion.x = 600;
            animation = "Dash";
        }else{
            animation = animation;
        }
        
        motion = MoveAndSlide(motion, up);
        animatedsprite.Play(animation);
    }
}
