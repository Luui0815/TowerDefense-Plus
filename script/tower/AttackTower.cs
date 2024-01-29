using Godot;
public abstract partial class AttackTower : Defender
{
	protected int _damage;
    protected Timer _AttackTimer;
    protected Area2D _AttackArea;

    protected void Attack(Enemy target, int damage)
    {
        if(target != null)
        {
            target.Health -= damage;
            GD.Print(target.Name + " HP: " + target.Health);
        }

    }
}