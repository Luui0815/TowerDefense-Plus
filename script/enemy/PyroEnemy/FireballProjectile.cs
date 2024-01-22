using Godot;

public partial class FireballProjectile : Projectile
{
    private bool _projectileHit = false;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _animatedSprite.Play("fireball");
        _animatedSprite.AnimationLooped += OnAnimationLooped;
        _hitboxArea = GetNode<Area2D>("HitboxArea");
    }

    public override void _Process(double delta)
    {
        Translate(-_velocity);

        if (HasTarget() && !_projectileHit)
        {
            _projectileHit = true;
            EmitSignal(Projectile.SignalName.TargetHit);
            _velocity = Vector2.Zero;
            _animatedSprite.Play("explosion");
        }

        //Falls Gegner schon besiegt wurde
        if (Position.X < _targetPosition.X - 150)
        {
            _animatedSprite.Play("explosion");
        }
    }

    private void OnAnimationLooped()
    {
        if(_animatedSprite.Animation == "explosion")
        {
            QueueFree();
        }
    }
}