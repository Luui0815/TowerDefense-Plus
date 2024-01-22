using TowerDefense;

public partial class LevelTwo : GameLevel
{
	public LevelTwo()
	{
		_currentMoney = 500;
	}

	protected override int LevelNumber
	{
		get;
	} = 2;

	protected override (int, EnemyType)[] SpawnConfig
	{
		get;
	} = new (int, EnemyType)[] { (40, EnemyType.Soldier), (10, EnemyType.Soldier), (5, EnemyType.Knight), (20, EnemyType.Knight), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Knight), (1, EnemyType.Soldier), (10, EnemyType.Soldier), (1, EnemyType.Bandit), (20, EnemyType.Bandit), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight), (2, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Bandit), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Bandit), (1, EnemyType.Bandit), (1, EnemyType.Bandit), (1, EnemyType.Giant)};

	protected override FieldType[,] FieldTypes
	{
		get;
	} = new FieldType[5, 10] {
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal }
	};

    protected override string UnlockedTower{
		get;
	} = "fire_trap";
}
