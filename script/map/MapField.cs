using Godot;
using System;

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

		public void Init(FieldType FT, int Fieldnumber)
        {
			_sprite = GetNode <Sprite2D> ("Background");
			_fieldNr = Fieldnumber;

			switch (FT)
			{
				case FieldType.Normal:
					{
						_sprite.Texture= GD.Load<Texture2D>("res://assets/texture/Fields/normal.png");
						break;
                    }
			}
        }
    }


}