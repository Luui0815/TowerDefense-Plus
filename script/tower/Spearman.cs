using Godot;
using System;

public partial class Spearman : MeleeDefender
{
	public Spearman()
    {
        //TODO: Change values and add action animation
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";
    }
}
