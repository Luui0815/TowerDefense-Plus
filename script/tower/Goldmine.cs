using Godot;

public partial class Goldmine : Defender
{
    [Signal]
    public delegate void MoneyGeneratedEventHandler(int moneyAmount);

    private int _moneyPerCycle = 250;
    private bool _moneyGenerated = false;

    public Goldmine()
    {
        _delay = 15;
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
        //_animatedSprite.Play(_actionAnimation);
        _animatedSprite.AnimationLooped += OnAnimationLooped;
    }

    public override void _Process(double delta)
    {
        if (Health <= 0)
        {
            Destroy();
        }

        if(_moneyGenerated)
            _animatedSprite.Play("action");
        else
            _animatedSprite.Play("idle");

    }

    public override void Action()
    {
        EmitSignal(SignalName.MoneyGenerated, _moneyPerCycle);
        _moneyGenerated = true;
        //wenn man hier die Anmiation abspielen wuerde, geht es nicht
        //und das ganze in die alte Version aufzurufen dauert
        //ich weiss auch nicht wie und wann und wo die Action Methode aufgerufen wird
    }

    private void OnAnimationLooped()
    {
        if (_animatedSprite.Animation == "action")
            _moneyGenerated = false;
    }
}