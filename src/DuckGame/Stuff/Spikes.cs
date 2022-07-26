﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Spikes
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    [EditorGroup("Stuff|Spikes")]
    [BaggedProperty("isInDemo", true)]
    [BaggedProperty("previewPriority", true)]
    public class Spikes : MaterialThing, IDontMove
    {
        private SpriteMap _sprite;
        public bool up = true;
        protected ImpactedFrom _killImpact;

        public Spikes(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._sprite = new SpriteMap("spikes", 16, 19);
            this._sprite.speed = 0.1f;
            this.graphic = (Sprite)this._sprite;
            this.center = new Vec2(8f, 14f);
            this.collisionOffset = new Vec2(-6f, -3f);
            this.collisionSize = new Vec2(13f, 5f);
            this.depth = (Depth)0.28f;
            this._editorName = "Spikes Up";
            this.editorTooltip = "Pointy and dangerous, unless you're wearing the right boots.";
            this.editorCycleType = typeof(SpikesRight);
            this.thickness = 3f;
            this.physicsMaterial = PhysicsMaterial.Metal;
            this.editorOffset = new Vec2(0.0f, 6f);
            this.hugWalls = WallHug.Floor;
            this._editorImageCenter = true;
            this._killImpact = ImpactedFrom.Top;
        }

        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            if (with is TV || with is Hat || !with.isServerForObject)
                return;
            Duck duck = with as Duck;
            if (this._killImpact == ImpactedFrom.Top && duck != null && duck.holdObject is Sword && (duck.holdObject as Sword)._slamStance)
                return;
            float num = 1f;
            if (from != this._killImpact)
                return;
            if (from == ImpactedFrom.Left && (double)with.hSpeed > (double)num)
                with.Destroy((DestroyType)new DTImpale((Thing)this));
            if (from == ImpactedFrom.Right && (double)with.hSpeed < -(double)num)
                with.Destroy((DestroyType)new DTImpale((Thing)this));
            if (from == ImpactedFrom.Top && (double)with.vSpeed > (double)num && (duck == null || !duck.HasEquipment(typeof(Boots))))
            {
                bool flag = true;
                if (with is PhysicsObject)
                {
                    PhysicsObject physicsObject = with as PhysicsObject;
                    Vec2 bottomRight = with.bottomRight;
                    Vec2 bottomLeft = with.bottomLeft;
                    Vec2 vec2_1;
                    Vec2 vec2_2 = vec2_1 = new Vec2(with.x, with.bottom);
                    Vec2 vec2_3 = physicsObject.lastPosition - physicsObject.position;
                    Vec2 p1_1 = bottomRight + vec2_3;
                    Vec2 p1_2 = bottomLeft + vec2_3;
                    Vec2 vec2_4 = vec2_3;
                    Vec2 p1_3 = vec2_1 + vec2_4;
                    flag = false;
                    Vec2 p2 = vec2_2;
                    Vec2 topLeft = this.topLeft;
                    Vec2 topRight = this.topRight;
                    if (Collision.LineIntersect(p1_3, p2, topLeft, topRight) || Collision.LineIntersect(p1_2, with.bottomLeft, this.topLeft, this.topRight) || Collision.LineIntersect(p1_1, with.bottomRight, this.topLeft, this.topRight))
                        flag = true;
                }
                if (flag)
                    with.Destroy((DestroyType)new DTImpale((Thing)this));
            }
            if (from != ImpactedFrom.Bottom || (double)with.vSpeed >= -(double)num || duck != null && duck.HasEquipment(typeof(Helmet)))
                return;
            with.Destroy((DestroyType)new DTImpale((Thing)this));
        }

        public override void Update() => base.Update();

        public override void Draw() => base.Draw();
    }
}
