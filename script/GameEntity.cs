using Godot;
using System;

public abstract partial class GameEntity : Node2D
{
    protected float _delay;
    protected float _animationDelay;
    protected string _idleAnimation = "idle";
    protected string _actionAnimation = "action";
    protected AnimatedSprite2D _sprite;
    protected GameLevel _level;
    protected Timer _actionTimer;
    private Timer _animationTimer;

    public int Health
    {
        get; 
        set;
    }

    public bool ImmuneToDamage {
        get;
    } = false;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _sprite.Play(_idleAnimation);

        _actionTimer = GetNode<Timer>("ActionTimer");
        _actionTimer.Timeout += OnTimerEnd;
        _actionTimer.OneShot = true;
        _animationTimer = GetNode<Timer>("AnimationTimer");
        _animationTimer.Timeout += OnAnimationTimerEnd;
        _animationTimer.OneShot = true;

        _level = GetNode<GameLevel>("/root/GameLevel");
    }

    public void InflictDamage(int damage)
    {
        if (!ImmuneToDamage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Destroy();
            }
        }
    }

    public void OnTimerEnd()
    {
        _sprite.Play(_actionAnimation);
        _animationTimer.Start(_animationDelay);
    }

    public void OnAnimationTimerEnd()
    {
        if (_level != null && _level.LevelStarted)
        {
            Action();
        }
        _sprite.Play(_idleAnimation);
        _actionTimer.Start(_delay);
    }

    public abstract void Action();

    public abstract void Destroy();
}
