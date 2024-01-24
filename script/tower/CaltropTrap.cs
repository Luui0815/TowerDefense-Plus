using Godot;

public partial class CaltropTrap : TrapDefence
{
    private Timer _pauseTimer;
    private Enemy _enemyInTrap;
    public CaltropTrap()
    {
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";
        Health = 5;
    }

    public override void _Ready()
    {
        _AttackArea = GetNode<Area2D>("AttackArea");
        _pauseTimer = GetNode<Timer>("PauseTimer");
        _pauseTimer.WaitTime = _delay;
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _animatedSprite.Play("open");
        _animatedSprite.AnimationLooped += OnAnimationLooped;
    }

    public override void _Process(double delta)
    {
        if(_enemyInTrap!=null && (_enemyInTrap.Health<=0 || !_enemyInTrap.IsFreezed()))//evtl. geht das, wenn nicht oben wieder einklammern!
        {
            _enemyInTrap = null;
            _animatedSprite.Play("opening");
            Health--;
        }
        if (Health <= 0)
        {
            OnDefenderDefeated();
        }

        TryToAttack();
    }

    private void TryToAttack()
    {
        if (_enemyInTrap == null)
        {
            foreach (Node2D area in _AttackArea.GetOverlappingAreas())
            {
                if (area.GetParent() is Enemy)
                {
                    Enemy enemy = (Enemy)area.GetParent();

                    if (area.Name == "HitboxArea" && enemy.EnemyName != "PyroEnemy" && _pauseTimer.IsStopped())//!_attackedEnemiess.Contains(enemy) &&
                    {
                        _enemyInTrap = (Enemy)area.GetParent();
                        _enemyInTrap.AddStatusEffect("caltrop");
                        _pauseTimer.Start();
                        TrapDeleted += (Name) => _enemyInTrap.DeleteTrap(Name);
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
            if (_enemyInTrap == null)
                return false;
            else
                return true;
        }
    }
}