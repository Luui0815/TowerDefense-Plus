using TowerDefense;

namespace TowerDefense
{
    public enum PlayerRole
    {
        Attacker,
        Defender
    }
}
public class PlayerInfo
{
    public string Name;
    public int Id;
    public PlayerRole Role;

    public PlayerInfo(string name, int id, PlayerRole role)
    {
        Name = name;
        Id = id;
        Role = role;  
    }
}