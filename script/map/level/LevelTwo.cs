using TowerDefense;

public partial class LevelTwo : GameLevel
{
	public LevelTwo()
	{
		_currentMoney = 500;
	}

	public override int LevelNumber
	{
		get;
	} = 2;

	protected override (int, EnemyType)[] SpawnConfig
	{
		get;
	} = new (int, EnemyType)[] { (40, EnemyType.Soldier), (10, EnemyType.Soldier), (5, EnemyType.Knight), (20, EnemyType.Knight), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Knight), (2, EnemyType.Soldier), (10, EnemyType.Soldier), (1, EnemyType.Bandit), (20, EnemyType.Bandit), (1, EnemyType.Knight), (1, EnemyType.Knight), (2, EnemyType.Knight), (2, EnemyType.Soldier), (1, EnemyType.Soldier), (2, EnemyType.Soldier), (2, EnemyType.Bandit), (1, EnemyType.Knight), (1, EnemyType.Knight), (2, EnemyType.Knight), (2, EnemyType.Soldier), (2, EnemyType.Soldier), (1, EnemyType.Soldier), (2, EnemyType.Soldier), (2, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Bandit), (3, EnemyType.Pyro), (1, EnemyType.Pyro), (3, EnemyType.Pyro), (1, EnemyType.Bandit), (1, EnemyType.Bandit), (10, EnemyType.Giant),(3, EnemyType.Giant) };

	protected override FieldType[,] FieldTypes
	{
		get;
	} = new FieldType[5, 10] {
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Disabled, FieldType.Normal, FieldType.Disabled, FieldType.Disabled, FieldType.Normal, FieldType.Normal, FieldType.Disabled },
		{ FieldType.Normal, FieldType.Normal, FieldType.Disabled, FieldType.Normal, FieldType.Disabled, FieldType.Normal, FieldType.Normal, FieldType.Disabled, FieldType.Normal, FieldType.Disabled },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Disabled, FieldType.Disabled, FieldType.Normal, FieldType.Normal, FieldType.Disabled, FieldType.Normal, FieldType.Disabled },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Disabled, FieldType.Disabled, FieldType.Normal, FieldType.Disabled, FieldType.Normal, FieldType.Disabled },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Disabled, FieldType.Disabled, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Disabled, FieldType.Disabled }
	};

    protected override string UnlockedTower{
		get;
	} = "fire_trap";
}
