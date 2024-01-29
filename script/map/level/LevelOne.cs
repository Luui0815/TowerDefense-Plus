using System.Collections.Generic;
using TowerDefense;

public partial class LevelOne : GameLevel
{
	public LevelOne()
	{
		_currentMoney = 500;
	}

	public override int LevelNumber
	{
		get;
	} = 1;

	protected override (int, EnemyType)[] SpawnConfig
	{
		get;
	} = new (int, EnemyType)[] { (30, EnemyType.Soldier), (10, EnemyType.Soldier), (6, EnemyType.Knight), (20, EnemyType.Knight), (1, EnemyType.Soldier), (2, EnemyType.Soldier), (2, EnemyType.Knight), (2, EnemyType.Knight), (2, EnemyType.Soldier), (2, EnemyType.Soldier), (12, EnemyType.Soldier), (1, EnemyType.Bandit), (20, EnemyType.Bandit), (1, EnemyType.Knight), (2, EnemyType.Knight), (2, EnemyType.Knight), (2, EnemyType.Soldier), (2, EnemyType.Soldier), (2, EnemyType.Soldier), (2, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight),(1, EnemyType.Soldier), (1, EnemyType.Soldier), (2, EnemyType.Soldier), (2, EnemyType.Soldier), (2, EnemyType.Soldier), (2, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Bandit), (1, EnemyType.Bandit), (30, EnemyType.Bandit), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight), (1, EnemyType.Knight), (3, EnemyType.Knight), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Soldier), (1, EnemyType.Bandit), (1, EnemyType.Bandit), (1, EnemyType.Bandit), (10, EnemyType.Giant)};

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
	} = "spearman";
}
