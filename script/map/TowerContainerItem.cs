using Godot;
using System.Collections.Generic;

public partial class TowerContainerItem : Control
{
    private static Dictionary<string, Texture2D> _iconTextureCache = new();
    private string _towerName;
    private int _towerCost;

    /// <summary>
    /// Initializes the item with the tower which should be displayed
    /// </summary>
    /// <param name="towerName">The name of the tower</param>
    /// <param name="towerCost">The cost of the tower</param>
    public void Init(string towerName, int towerCost)
    {
        _towerName = towerName;
        _towerCost = towerCost;

        TextureRect towerBackground = GetNode<TextureRect>("ItemBackground");
        towerBackground.Texture = GD.Load<Texture2D>($"res://assets/texture/tower/background/{towerName}.png");

        Label towerLabel = towerBackground.GetNode<Label>("TowerCostLabel");
        towerLabel.Text = $"{towerCost}$";

        if (!_iconTextureCache.ContainsKey(towerName))
        {
            Texture2D texture = GD.Load<Texture2D>($"res://assets/texture/tower/icon/{towerName}.png");
            _iconTextureCache.Add(towerName, texture);
        }
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        SetDragPreview(CreateDragPreview());
        return this;
    }

    private Control CreateDragPreview()
    {
        TextureRect previewNode = new TextureRect
        {
            Size = new Vector2(64, 64),
            ExpandMode = TextureRect.ExpandModeEnum.FitWidth,
            Texture = _iconTextureCache[_towerName]
        };
        return previewNode;
    }
}
