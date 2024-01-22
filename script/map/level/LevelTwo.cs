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
	} = new (int, EnemyType)[] { (30, EnemyType.Soldier), (10, EnemyType.Soldier), (5, EnemyType.Knight), (20, EnemyType.Knight), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (10, EnemyType.Soldier), (1, EnemyType.Bandit), (20, EnemyType.Bandit), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight), (2, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Pyro), (20, EnemyType.Pyro), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Bandit), (1, EnemyType.Bandit), (1, EnemyType.Bandit), (1, EnemyType.Pyro), (25, EnemyType.Pyro), (5, EnemyType.Giant), (4, EnemyType.Giant)};

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
}
