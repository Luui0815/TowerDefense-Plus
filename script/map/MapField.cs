using Godot;
using System.Collections.Generic;


namespace TowerDefense
{
	public enum FieldType
	{
		Normal,
		Disabled
	}

	public partial class MapField : Control
	{
		[Signal]
		public delegate void DefenderPlacedEventHandler(int cost);
		private int _fieldNr;
		private Sprite2D _sprite;
		private FieldType _fieldType;

        public Defender Tower { get; set; }

        public void Init(FieldType fieldType, int fieldNumber)
		{
			_sprite = GetNode<Sprite2D>("Background");
			_fieldNr = fieldNumber;
			_fieldType = fieldType;
		}

		public override bool _CanDropData(Vector2 atPosition, Variant data)
		{
			if(_fieldType==FieldType.Normal)
			{
                string info = (string)data;
                if (info == "hammer" && Tower != null) return true;
                else if (info != "" && info != "hammer" && Tower == null) return true;
                else return false;
            }
			else
				return false;
		}

		public override void _DropData(Vector2 atPosition, Variant data)
		{
			string towerName = (string)data;

			if(towerName == "hammer")
			{
				if(Tower is TrapDefence)
				{
					caltrop_trap caltrop = (caltrop_trap)Tower;
					if(caltrop.IsEnemyInTrap)
						Tower.EmitSignal(TrapDefence.SignalName.TrapDeleted,Tower.Name);//muss so, da sonst Methode vom enemy in Trap aufgerufen wird, 
                }                                                                       //wenn der aber nicht drin, baehm Null Pointer

                Tower.QueueFree();
				Tower = null;
			}
			else
			{
                Tower = (Defender)GD.Load<PackedScene>($"res://scene/tower/{towerName}.tscn").Instantiate();
                Tower.Init(towerName);
                Tower.TopLevel = true;
                Tower.Position = GlobalPosition;

                if (towerName == "goldmine")
                {
                    GameLevel level = (GameLevel)GetParent().GetParent();
                    ((Goldmine)Tower).MoneyGenerated += level.AddMoney;
                }

                AddChild(Tower);
                EmitSignal(SignalName.DefenderPlaced, Tower.GetTowerCost());
            }
		}

		private void  _on_arrow_spear_ground_area_entered(Area2D area)
		{
			if(area.Name== "ArrowHitboxArea" || area.Name== "SpearHitboxArea")
			{
				area.GetParent().QueueFree();
			}
		}
    }


}