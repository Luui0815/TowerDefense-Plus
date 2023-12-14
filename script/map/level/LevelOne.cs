using System.Runtime.CompilerServices;
using TowerDefense;

public partial class LevelOne : GameLevel
{
    public LevelOne()
    {
        //TODO: Change start money
        _currentMoney = 999;
    }

    protected override int LevelNumber
    {
        get;
    } = 1;

    //TODO: Configure spawns
    protected override string[] SpawnConfigs
    {
        get;
    } = new string[3] {
        "", "", ""
    };

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

    protected override void addmoney_from_mine(int newmoney)
    {
        ChangeMoney(CurrentMoney + newmoney);
        EmitSignal(SignalName.MoneyChanged, _currentMoney);
    }
}
