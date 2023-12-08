using Godot;
using System;

public partial class goldmine : defender
{
    [Signal]
    public delegate void generated_mine_moneyEventHandler(int _newmoney);

    private AnimatedSprite2D _mine;
    private Timer _moneytimer;
    private int _money_per_time = 50;

    public override void Init()
    {
        _name = "goldmine";
        //_cost = get_Tower_cost(_name);
        _cost = 250;

        _moneytimer = GetNode<Timer>("money_timer");
        _mine= GetNode<AnimatedSprite2D>("goldmine");
        //_moneytimer.Start(25);
        _mine.Play();
    }

    public override void _Process(double delta)
    {
        //if( _moneytimer.TimeLeft==0 )
        //{
            //EmitSignal(SignalName.generated_mine_money, _money_per_time);
        //}
    }
}
