﻿using System;

namespace XnaToFna.ProxyDrawing
{
    [Serializable]
    public struct Point
    {
        public static readonly Point Empty;
        private int x;
        private int y;

        public bool IsEmpty => this == Empty;

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

        public Point(int dw)
          : this(dw & ushort.MaxValue, dw >> 16)
        {
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Offset(Point p) => Offset(p.X, p.Y);

        public void Offset(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public static bool operator !=(Point left, Point right) => !(left == right);

        public static bool operator ==(Point left, Point right) => left.x == right.x && left.y == right.y;

        public override bool Equals(object obj) => obj is Point point && this == point;

        public override int GetHashCode() => x ^ y;

        public override string ToString() => string.Format("{{X={0},Y={1}}}", x, y);
    }
}
