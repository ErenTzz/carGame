using System;
using UnityEngine.Serialization;

namespace FastMesh
{
    [Serializable]
    public struct Ser_Guid
    {
        public string v;

        public Ser_Guid(Guid guid)
        {
            v = guid.ToString();
        }

        public readonly Guid ToGuid()
        {
            return new Guid(v);
        }
    }
}