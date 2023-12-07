using Godot;
using System.Collections.Generic;

public partial class TowerContainerItem : Control
{
    private static Dictionary<string, Texture2D> _iconTextureCache = new();
    private string _towerName;
    private int _towerCost;
    private TextureRect _towerBackground;
    private bool _tooexpensive=false;//wird auf true gesetzt wenn der akt. Geldbetrag nicht ausreicht den Turm zu platzieren

    /// <summary>
    /// Initializes the item with the tower which should be displayed
    /// </summary>
    /// <param name="towerName">The name of the tower</param>
    /// <param name="towerCost">The cost of the tower</param>
    public void Init(string towerName, int towerCost)
    {
        _towerName = towerName;
        _towerCost = towerCost;

        _towerBackground = GetNode<TextureRect>("ItemBackground");
        _towerBackground.Texture = GD.Load<Texture2D>($"res://assets/texture/tower/background/{towerName}.png");

        Label towerLabel = _towerBackground.GetNode<Label>("TowerCostLabel");
        towerLabel.Text = $"{towerCost}$";

        if (!_iconTextureCache.ContainsKey(towerName))
        {
            Texture2D texture = GD.Load<Texture2D>($"res://assets/texture/tower/icon/{towerName}.png");
            _iconTextureCache.Add(towerName, texture);
        }
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (!_tooexpensive)
        {
            SetDragPreview(CreateDragPreview());
            return _towerName;
        }
        else
            return "";

    }

    private Control CreateDragPreview()
    {
        if (!_tooexpensive)
        {
            TextureRect previewNode = new TextureRect
            {
                Size = new Vector2(64, 64),
                ExpandMode = TextureRect.ExpandModeEnum.FitWidth,
                Texture = _iconTextureCache[_towerName]
            };
            return previewNode;
        }
        else
            return null;//Preview wird nicht mehr erzeugt, es wird trotzdem geguckt ob man das Ding platzieren kann
    }

    private void check_if_money_empty(int _current_money)
    {
        if(_current_money <_towerCost)
        {
            string Name = "dark_" + _towerName;

            //Hide();//TODO: Felder sollen nicht verschwinden sondern ausgegarut werden
            if(!_iconTextureCache.ContainsKey(Name))
            {
                Texture2D texture = GD.Load<Texture2D>($"res://assets/texture/tower/background/{Name}.png");
                _iconTextureCache.Add(Name, texture);
                _towerBackground.Texture = texture;
            }
            else
            {
                _towerBackground.Texture = _iconTextureCache[Name];
            }
            _tooexpensive = true;
        }
    }
}
