using Godot;

public partial class Projectile : Node2D
{
	protected virtual void MoveProjectile(Vector2 shotSpeed)
	{
        Translate(shotSpeed);
    }

    protected virtual void MoveProjectile(float shotSpeed)//Maurice benutzt diese Version fuer seinen Feuermagier
    {
        MoveProjectile(new Vector2(shotSpeed, 0));
    }
}
