using System;

namespace FastMesh
{
    [Serializable]
    public struct Ser_Bytes
    {
        public byte[] bytes;
        public Ser_Bytes(byte[] bytes) => this.bytes = bytes;
    }
}