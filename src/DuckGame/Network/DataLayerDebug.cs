﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.DataLayerDebug
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System.Collections.Generic;

namespace DuckGame
{
    public class DataLayerDebug : DataLayer
    {
        private bool sendingDuplicate;

        public DataLayerDebug(NCNetworkImplementation pImpl)
          : base(pImpl)
        {
        }

        public override NCError SendPacket(BitBuffer sendData, NetworkConnection connection)
        {
            if (!this.sendingDuplicate && (double)Rando.Float(1f) < (double)connection.debuggerContext.duplicate)
            {
                this.sendingDuplicate = true;
                this.SendPacket(sendData, connection);
                if ((double)connection.debuggerContext.duplicate > 0.400000005960464 && (double)Rando.Float(1f) < (double)connection.debuggerContext.duplicate)
                    this.SendPacket(sendData, connection);
                if ((double)connection.debuggerContext.duplicate > 0.800000011920929 && (double)Rando.Float(1f) < (double)connection.debuggerContext.duplicate)
                    this.SendPacket(sendData, connection);
                this.sendingDuplicate = false;
            }
            float num = connection.debuggerContext.CalculateLatency();
            if (connection.debuggerContext.lagSpike > 0)
                num = 1f / 1000f;
            if ((double)num == 3.40282346638529E+38)
                return (NCError)null;
            if ((double)num <= 0.0)
                return this._impl.OnSendPacket(sendData.buffer, sendData.lengthInBytes, connection.data);
            connection.debuggerContext.packets.Add(new DataLayerDebug.BadConnection.DelayedPacket()
            {
                data = sendData,
                time = num
            });
            return (NCError)null;
        }

        public override void Update()
        {
        }

        public class BadConnection
        {
            public int lagSpike;
            private float _latency;
            private float _jitter;
            private float _loss;
            private float _duplicate;
            public NetworkConnection connection;
            public List<DataLayerDebug.BadConnection.DelayedPacket> packets = new List<DataLayerDebug.BadConnection.DelayedPacket>();
            private int i;

            public float latency
            {
                get => (double)this._latency == 0.0 ? DuckNetwork.localConnection.debuggerContext._latency : this._latency;
                set => this._latency = value;
            }

            public float jitter
            {
                get => (double)this._jitter == 0.0 ? DuckNetwork.localConnection.debuggerContext._jitter : this._jitter;
                set => this._jitter = value;
            }

            public float loss
            {
                get => (double)this._loss == 0.0 ? DuckNetwork.localConnection.debuggerContext._loss : this._loss;
                set => this._loss = value;
            }

            public float duplicate
            {
                get => (double)this._duplicate == 0.0 ? DuckNetwork.localConnection.debuggerContext._duplicate : this._duplicate;
                set => this._duplicate = value;
            }

            public BadConnection(NetworkConnection pContext) => this.connection = pContext;

            public float CalculateLatency()
            {
                float num = 0.0f;
                if (this.CalculateLoss())
                {
                    if (Rando.Int(3) != 0)
                        return float.MaxValue;
                    num += Rando.Float(2f, 4f);
                }
                return (float)((double)this.latency + 0.0 - 0.0160000007599592) + Rando.Float(-this.jitter, this.jitter) + num;
            }

            public bool CalculateLoss() => (double)this.loss != 0.0 && (double)Rando.Float(1f) < (double)this.loss;

            public bool Update(NCNetworkImplementation pNetwork)
            {
                List<DataLayerDebug.BadConnection.DelayedPacket> delayedPacketList = new List<DataLayerDebug.BadConnection.DelayedPacket>();
                foreach (DataLayerDebug.BadConnection.DelayedPacket packet in this.packets)
                {
                    packet.time -= Maths.IncFrameTimer();
                    if ((double)packet.time <= 0.0 && this.connection.debuggerContext.lagSpike <= 0)
                    {
                        pNetwork.OnSendPacket(packet.data.buffer, packet.data.lengthInBytes, this.connection.data);
                        delayedPacketList.Add(packet);
                    }
                }
                foreach (DataLayerDebug.BadConnection.DelayedPacket delayedPacket in delayedPacketList)
                {
                    if (Rando.Int(15) == 0)
                        delayedPacket.time = Rando.Float(2f, 5f);
                    else
                        this.packets.Remove(delayedPacket);
                }
                if (this.lagSpike > 0)
                    this.lagSpike -= 9;
                return this.packets.Count == 0;
            }

            public void Reset() => this.packets.Clear();

            public class DelayedPacket
            {
                public BitBuffer data;
                public float time;
            }
        }
    }
}
