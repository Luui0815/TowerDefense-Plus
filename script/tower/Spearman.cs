using Godot;
using TowerDefense;

public partial class Spearman : MeleeDefender //so halber Nahkampf
{
    private Enemy _targetEnemy;
    private Area2D _AttackArea2;
    private float _SpearVelocity = 5f;
    private bool _Mode; //false = Melee Mode, true = Distance Mode
    public Spearman()
    {
        _delay = 2;
        _animationDelay = 1;
        _actionAnimation = "idle";
        _damage = 3;
        Health = 10;
    }
    public override void _Ready()
    {
        _AttackArea = GetNode<Area2D>("AttackArea1");
        _AttackArea2 = GetNode<Area2D>("AttackArea2");

        _HitboxArea = GetNode<Area2D>("HitboxArea");
        _AttackTimer = GetNode<Timer>("AttackTimer");
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _AttackTimer.WaitTime = _delay; ;
        _animatedSprite.Play(_actionAnimation);
        _animatedSprite.AnimationLooped += OnAnimationLooped;
    }

    public override void _Process(double delta)
    {
        if (!DefenderDefeated)
        {
            if (CanAttack() && _Mode==false)
            {
                if(_AttackTimer.IsStopped())
                    _animatedSprite.Play("attack");
                Attack(_targetEnemy, 2);
                _AttackTimer.Start();
            }
            else if(CanAttack() && _Mode==true)
            {
                if (_AttackTimer.IsStopped())
                    _animatedSprite.Play("spear_attack");
                _AttackTimer.Start();
            }
            else
            {
                if (_AttackTimer.IsStopped())
                    _animatedSprite.Play("idle");
            }

            if (Health <= 0)
            {
                OnDefenderDefeated();
            }
        }
    }

    protected Enemy SelectTarget()
    {
        _Mode = false;
        Enemy closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Node2D body in _AttackArea.GetOverlappingAreas())//Nahkampf
        {
            if (body.Name == "HitboxArea")
            {
                Node2D parent = (Node2D)body.GetParent();
                if (parent is Enemy)
                {
                    float distance = Position.DistanceTo(parent.Position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = parent as Enemy;
                    }
                }
            }
        }
        if(closestTarget == null)//Fernkampf
        {
            foreach (Node2D body in _AttackArea2.GetOverlappingAreas())
            {
                if (body.Name == "HitboxArea")
                {
                    Node2D parent = (Node2D)body.GetParent();
                    if (parent is Enemy)
                    {
                        float distance = Position.DistanceTo(parent.Position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestTarget = parent as Enemy;
                        }
                    }
                }
            }
            _Mode= true;
        }
        return (Enemy)closestTarget;
    }

    private bool CanAttack()
    {
        if (_AttackTimer.IsStopped())
        {
            _targetEnemy = SelectTarget();
            if (_targetEnemy!=null)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private void OnAnimationLooped()
    {
        if (_animatedSprite.Animation == "death")
        {
            Destroy();
        }
        if(_animatedSprite.Animation == "spear_attack")
            SpawnSpear();
    }

    private void SpawnSpear()
    {
        TowerProjectile spear = (TowerProjectile)GD.Load<PackedScene>("res://scene/tower/TowerProjectile.tscn").Instantiate();
        if (_targetEnemy != null && _targetEnemy.Health > 0)
        {
            spear.Init(_targetEnemy, _SpearVelocity, ProjectileType.Spear, this);
            spear.TargetHit += ArrowHit;
            spear.Position = new Vector2(GlobalPosition.X - 10, GlobalPosition.Y + 20);
            AddChild(spear);
        }
    }

    private void ArrowHit()
    {
        if (_targetEnemy != null && _targetEnemy.Health > 0)
            Attack(_targetEnemy, _damage);
    }

    private void _on_attack_area_2_area_exited(Area2D area)
    {
        if (area.Name == "SpearHitboxArea")
        {
            TowerProjectile projectile = (TowerProjectile)area.GetParent();
            if (projectile.Shooter == this)
            {
                projectile.ShouldFall = true;
            }
        }
    }
}
