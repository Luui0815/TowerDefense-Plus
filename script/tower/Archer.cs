using Godot;
using System;

public partial class Archer : RangeDefender
{
    public Archer()
    {
        //TODO: Change values and add action animation
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";
    }

    public override void Action()
    {
        
    }
}