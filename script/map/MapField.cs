using Godot;
using System.Collections.Generic;

namespace TowerDefense
{
	public enum FieldType
	{
		Normal
	}

	public partial class MapField : Control
	{
		private static Dictionary<FieldType, Texture2D> _fieldTextureCache = new();
		private int _fieldNr;
		private Sprite2D _sprite;
		private Sprite2D _provTower;
		private bool _Towerset;

		public void Init(FieldType fieldType, int fieldNumber)
		{
			_sprite = GetNode<Sprite2D>("Background");
			_fieldNr = fieldNumber;

			switch (fieldType)
			{
				case FieldType.Normal:
					{
						if(!_fieldTextureCache.ContainsKey(fieldType))
						{
							_sprite.Texture = GD.Load<Texture2D>("res://assets/texture/field/Normal.png");
							_fieldTextureCache.Add(fieldType,_sprite.Texture);
						}
						else 
						{
							_sprite.Texture = _fieldTextureCache[fieldType];
						}
						break;
					}
			}
		}

		public override bool _CanDropData(Vector2 atPosition, Variant data)
		{
			if(!_Towerset)
				return true; 
			else
				return false;
		}

		public override void _DropData(Vector2 atPosition, Variant data)
		{
			string towerName=(string) data;

			Sprite2D _provTower=new Sprite2D();
			//_provTower.Transform=new Vector2(98,134);
			_provTower.Texture = GD.Load<Texture2D>($"res://assets/texture/tower/background/{towerName}.png");
			AddChild(_provTower);
			_Towerset=true;
		}


	}


}