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
		private TowerContainerItem Tower;
		private int _fieldNr;
		private Sprite2D _sprite;

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
			GD.Print("ja");
			if(Tower==null)
				return true; 
			else
				return false;
			
		}

		public override void _DropData(Vector2 atPosition, Variant data)
		{
			GD.Print("ja2");
			Tower = (TowerContainerItem) data;
			AddChild(Tower);
		}


	}


}