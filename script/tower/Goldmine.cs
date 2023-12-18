using Godot;

public partial class Goldmine : Defender
{
    [Signal]
    public delegate void MoneyGeneratedEventHandler(int moneyAmount);

    private int _moneyPerCycle = 250;

    public Goldmine()
    {
        _delay = 15;
        _animationDelay = 1;

        //TODO: Add action animation
        _actionAnimation = "idle";
    }

    public override void _Ready()
    {
        base._Ready();

        _actionTimer.Start(_delay);
    }

    public override void Action()
    {
        EmitSignal(SignalName.MoneyGenerated, _moneyPerCycle);
    }
}
