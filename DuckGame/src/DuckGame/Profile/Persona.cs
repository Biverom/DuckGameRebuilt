﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DuckGame
{
    public static class Persona
    {
        private static List<DuckPersona> _personasOriginalOrder = new List<DuckPersona>()
        {
          new DuckPersona(new Vec3( byte.MaxValue,  byte.MaxValue,  byte.MaxValue),0),
          new DuckPersona(new Vec3(125f, 125f, 125f),1),
          new DuckPersona(new Vec3(247f, 224f, 90f),2),
          new DuckPersona(new Vec3(205f, 107f, 29f),3),
          new DuckPersona(new Vec3(0f, 133f, 74f), new Vec3(0f, 102f, 57f), new Vec3(0f, 173f, 97f),4),
          new DuckPersona(new Vec3( byte.MaxValue, 105f, 117f), new Vec3(207f, 84f, 94f), new Vec3( byte.MaxValue, 158f, 166f),5),
          new DuckPersona(new Vec3(49f, 162f, 242f), new Vec3(13f, 123f, 181f), new Vec3(148f, 207f, 245f),6),
          new DuckPersona(new Vec3(175f, 85f, 221f), new Vec3(141f, 36f, 194f), new Vec3(213f, 165f, 238f),7),
          new DuckPersona(new Vec3(128f,0f,0f)), 
          new DuckPersona(new Vec3(0f,95f,0f)), 
          new DuckPersona(new Vec3(255f,255f,0f)),
          new DuckPersona(new Vec3(127, 255, 0)),
          new DuckPersona(new Vec3(16,16,16)), 
          new DuckPersona(new Vec3(0f,0f,128)), 
          new DuckPersona(new Vec3(74f,0f,43f)),
          new DuckPersona(new Vec3(255f,0f,0f)),
          new DuckPersona(new Vec3(212f,175f,55f)), 
          new DuckPersona(new Vec3(255f,0f,255f)), 
          new DuckPersona(new Vec3(0f,30f,0f)),
          new DuckPersona(new Vec3(145f, 255f, 212f)),
          new DuckPersona(new Vec3(255f,120f,0f)),
          new DuckPersona(new Vec3(255f,121f,102f)),
          new DuckPersona(new Vec3(210f,245f,60f)),
          new DuckPersona(new Vec3(127f,0f,255f)),
          new DuckPersona(new Vec3(130f,0f,130f)),
          new DuckPersona(new Vec3(0f,128f,128f)),
          new DuckPersona(new Vec3(255f,26f,88f)),
          new DuckPersona(new Vec3(0f,255f,255f)),
          new DuckPersona(new Vec3(0f,0f,255f)),
          new DuckPersona(new Vec3(212f,175f,55f)),
          new DuckPersona(new Vec3(213f,212f,185f)),
          new DuckPersona(new Vec3(56f,69f,66f)),
          new DuckPersona(new Vec3(128f,128f,0)),
          new DuckPersona(new Vec3(99f,51f,0f)),
          new DuckPersona(new Vec3(255f,159f,255f)),
          new DuckPersona(new Vec3(137f,175f,255f)),
          new DuckPersona(new Vec3(255f,190f,141f)),
          new DuckPersona(new Vec3(239f,84f,48f)),
          new DuckPersona(new Vec3(154f,205f,50f)),
          new DuckPersona(new Vec3(0f,250f,127f)),
          new DuckPersona(new Vec3(29f,129f,70f)),
          new DuckPersona(new Vec3(255f,85f,214f)),
          new DuckPersona(new Vec3(85f,53f,116f)),
          new DuckPersona(new Vec3(246f,34f,0f)),
          new DuckPersona(new Vec3(46f,62f,126f)),
          new DuckPersona(new Vec3(40f,138f,184f)),
          new DuckPersona(new Vec3(106f,47f,77f)),
          new DuckPersona(new Vec3(118f,87f,0f)),
          new DuckPersona(new Vec3(0f,255f,66f)),
          new DuckPersona(new Vec3(0f,0f,46f)),
          new DuckPersona(new Vec3(157f,169f,179f))
        };
        private static List<DuckPersona> _personasShuffled;
        public static int seed;

        private static List<DuckPersona> _personas
        {
            get
            {
                if (_personasShuffled == null)
                    Shuffle();
                return _personasShuffled;
            }
        }

        public static DuckPersona Duck1 => _personas[0];

        public static DuckPersona Duck2 => _personas[1];

        public static DuckPersona Duck3 => _personas[2];

        public static DuckPersona Duck4 => _personas[3];

        public static DuckPersona Duck5 => _personas[4];

        public static DuckPersona Duck6 => _personas[5];

        public static DuckPersona Duck7 => _personas[6];

        public static DuckPersona Duck8 => _personas[7];

        public static IEnumerable<DuckPersona> all => _personas;

        public static List<DuckPersona> alllist => _personas;
        public static void Initialize()
        {
        }

        public static void Shuffle(int pSeed = -1)
        {
            seed = pSeed >= 0 ? pSeed : Rando.Int(2147483646);
            Random generator = Rando.generator;
            Rando.generator = new Random(seed);
            _personasShuffled = _personasOriginalOrder.ToList();
            Rando.generator = generator;
        }

        public static int Number(DuckPersona p) => _personas.IndexOf(p);
    }
}
