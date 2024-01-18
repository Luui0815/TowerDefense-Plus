using Godot;

public partial class Goldmine : Defender
{
    [Signal]
    public delegate void MoneyGeneratedEventHandler(int moneyAmount);

    private int _moneyPerCycle = 150;
    private bool _moneyGenerated = false;

    public Goldmine()
    {
        _delay = 12;
        _animationDelay = 1;

        //TODO: Add action animation
        _actionAnimation = "idle";
        Health = 5;
    }

    public override void _Ready()
    {
        base._Ready();
        _actionTimer.Start(_delay);
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _animatedSprite.AnimationLooped += OnAnimationLooped;
    }

    public override void _Process(double delta)
    {
        if (Health <= 0)
        {
            OnDefenderDefeated();
        }

        if(Health > 0)
        {
            if (_moneyGenerated)
                _animatedSprite.Play("action");
            else
                _animatedSprite.Play("idle");
        }
    }

    public override void Action()
    {
        _moneyGenerated = true;
    }

    private void OnAnimationLooped()
    {
        if (_animatedSprite.Animation == "action")
        {
            _moneyGenerated = false;
            EmitSignal(SignalName.MoneyGenerated, _moneyPerCycle);
        }
        if (_animatedSprite.Animation == "death")
            Destroy();
    }
}