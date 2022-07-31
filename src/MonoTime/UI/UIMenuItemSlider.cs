﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.UIMenuItemSlider
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class UIMenuItemSlider : UIMenuItem
    {
        private FieldBinding _field;
        private float _step;

        public UIMenuItemSlider(
          string text,
          UIMenuAction action = null,
          FieldBinding field = null,
          float step = 0.1f,
          Color c = default(Color))
          : base(action)
        {
            if (c == new Color())
                c = Colors.MenuOption;
            UIDivider component1 = new UIDivider(true, 0f);
            UIText component2 = new UIText(text, c)
            {
                align = UIAlign.Left
            };
            component1.leftSection.Add(component2, true);
            UIProgressBar component3 = new UIProgressBar(step < 0.0526315793395042 ? 26f : 30f, 7f, field, step)
            {
                align = UIAlign.Right
            };
            component1.rightSection.Add(component3, true);
            this.rightSection.Add(component1, true);
            this._arrow = new UIImage("contextArrowRight")
            {
                align = UIAlign.Right,
                visible = false
            };
            this.leftSection.Add(_arrow, true);
            this._field = field;
            this._step = step;
            this.controlString = "@CANCEL@BACK @WASD@ADJUST";
        }

        public override void Activate(string trigger)
        {
            float num;
            if (trigger == "MENULEFT")
            {
                num = Maths.Clamp((float)this._field.value - this._step, this._field.min, this._field.max);
            }
            else
            {
                if (!(trigger == "MENURIGHT"))
                    return;
                num = Maths.Clamp((float)this._field.value + this._step, this._field.min, this._field.max);
            }
            if (num != (float)this._field.value)
                SFX.Play("textLetter", 0.7f);
            this._field.value = num;
        }
    }
}
