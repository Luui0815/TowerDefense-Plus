using Godot;

public abstract partial class Projectile : Node2D
{
    protected GameEntity _target;
    protected Vector2 _targetPosition;
    protected Vector2 _velocity = Vector2.Zero;
    protected Area2D _hitboxArea;
    protected AnimatedSprite2D _animatedSprite;

    [Signal]
    public delegate void TargetHitEventHandler();

    public void Init(GameEntity target, float velocity)
    {
        _target = target;
        if (IsInstanceValid(target))
        {
            _targetPosition = target.Position;
        }
        _velocity.X = velocity;
    }

    protected bool HasTarget()
    {
        foreach (Node2D body in _hitboxArea.GetOverlappingAreas())
        {
            if (body.Name == "HitboxArea" && body.GetParent() == _target)
            {
                return true;
            }
        }
		return false;
    }
}
