using Godot;
using System;
using System.Collections.Generic;

public partial class caltrop_trap : TrapDefence
{
    //private List<Enemy> _attackableEnemiess = new List<Enemy>();
    //private List<Enemy> _attackedEnemiess = new List<Enemy>();
    private Timer _PauseTimer;
    private Enemy _EnemyInTrap;
    public caltrop_trap()
    {
        //TODO: Change values and add action animation
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";
        Health = 5;
    }

    public override void Action()
    {

    }

    public override void _Ready()
    {
        _AttackArea = GetNode<Area2D>("AttackArea");
        _PauseTimer = GetNode<Timer>("PauseTimer");
        _PauseTimer.WaitTime = _delay;
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _animatedSprite.Play("open");
        _animatedSprite.AnimationLooped += OnAnimationLooped;
    }

    public override void _Process(double delta)
    {
        
        /*
        if(_EnemyInTrap!=null && _EnemyInTrap.Health<=0)//Gegner in Falle gestorben
        {
            _EnemyInTrap = null;
            _animatedSprite.Play("opening");
            Health--;
        }

        if(_EnemyInTrap!= null && !_EnemyInTrap.IsFreezed())//Falle oeffnet sich wieder
        {                                                   //ich weiss, das ist Mist, aber geht denke ich nicht anders
            _EnemyInTrap = null;
            _animatedSprite.Play("opening");
            Health--;
        }
        */
        if(_EnemyInTrap!=null && (_EnemyInTrap.Health<=0 || !_EnemyInTrap.IsFreezed()))//evtl. geht das, wenn nicht oben wieder einklammern!
        {
            _EnemyInTrap = null;
            _animatedSprite.Play("opening");
            Health--;
        }
        if (Health <= 0)
        {
            _animatedSprite.Play("death");
        }

        TryToAttack();
    }
    /*
    private void _on_attack_area_area_entered(Area2D area)
    {
        if(area.GetParent() is Enemy)
        {
            Enemy enemy = (Enemy)area.GetParent();

            if (area.Name == "HitboxArea" && _EnemyInTrap == null && enemy.EnemyName != "PyroEnemy" && _PauseTimer.IsStopped())//!_attackedEnemiess.Contains(enemy) &&
            {
                _EnemyInTrap = (Enemy)area.GetParent();
                _EnemyInTrap.AddStatusEffect("caltrop");
                _PauseTimer.Start();
                //_attackedEnemiess.Add(_EnemyInTrap);
                TrapDeleted += (Name) => _EnemyInTrap.DeleteTrap(Name);
                _animatedSprite.Play("closing");
            }
        }
    }*/

    private void TryToAttack()
    {
        if (_EnemyInTrap == null)
        {
            foreach (Node2D area in _AttackArea.GetOverlappingAreas())
            {
                if (area.GetParent() is Enemy)
                {
                    Enemy enemy = (Enemy)area.GetParent();

                    if (area.Name == "HitboxArea" && enemy.EnemyName != "PyroEnemy" && _PauseTimer.IsStopped())//!_attackedEnemiess.Contains(enemy) &&
                    {
                        _EnemyInTrap = (Enemy)area.GetParent();
                        _EnemyInTrap.AddStatusEffect("caltrop");
                        _PauseTimer.Start();
                        //_attackedEnemiess.Add(_EnemyInTrap);
                        TrapDeleted += (Name) => _EnemyInTrap.DeleteTrap(Name);
                        _animatedSprite.Play("closing");
                        break;
                    }
                }
            }
        }
    }

    private void OnAnimationLooped()
    {
        if (_animatedSprite.Animation == "closing")
            _animatedSprite.Play("closed");
        if (_animatedSprite.Animation == "opening")
            _animatedSprite.Play("open");
        if (_animatedSprite.Animation == "death")
            Destroy();
    }

    public bool IsEnemyInTrap
    {
        get
        {
            if (_EnemyInTrap == null)
                return false;
            else
                return true;
        }
    }
}