using Godot;

namespace TowerDefense
{
	public enum FieldType
	{
		Normal
	}

	public partial class MapField : Control
	{
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
						_sprite.Texture = GD.Load<Texture2D>("res://assets/texture/field/Normal.png");
						break;
					}
			}
		}
	}


}