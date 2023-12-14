using Godot;
using System;

public  partial class Knight : MeleeDefender
{
    public Knight()
    {
        //TODO: Change values and add action animation
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";
    }
}
