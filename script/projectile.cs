using Godot;
using System;

public partial class projectile : Node2D
{
	protected virtual void MoveProjectile(Vector2 ShotSpeed)
	{
        Translate(ShotSpeed);
    }

    protected virtual void MoveProjectile(float ShotSpeed)//Maurice benutzt diese Version fuer seinen Feuermagier
    {
        Vector2 movement = new(ShotSpeed, 0);
        Translate(movement);
    }
}
