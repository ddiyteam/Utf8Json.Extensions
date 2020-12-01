using System;
using System.Collections.Generic;
using System.Text;

namespace Utf8Json.Extensions.Internal
{
    public class TComparer<T>
    {
        readonly Dictionary<ulong, T> buckets;
        static readonly bool Is32Bit = (IntPtr.Size == 4);

        public TComparer(int length)
        {
            buckets = new Dictionary<ulong, T>();
        }

        public void Add(string key, T value)
        {
            if (!TryAddInternal(Encoding.UTF8.GetBytes(key), value))
            {
                throw new ArgumentException("Key was already exists. Key:" + key);
            }
        }

        public void Add(byte[] key, T value)
        {
            if (!TryAddInternal(key, value))
            {
                throw new ArgumentException("Key was already exists. Key:" + key);
            }
        }


        bool TryAddInternal(byte[] key, T value)
        {
            var hash = GetHashCode(key);

            if(buckets.ContainsKey(hash))
            {
                return false;
            }

            buckets.Add(hash, value);
            return true;
        }

        public bool TryGetValue(ArraySegment<byte> key, out T value)
        {
            var hash = GetHashCode(key);

            if(buckets.TryGetValue(hash, out T target))
            {
                value = target;
                return true;
            }

            value = default;
            return false;
        }

        private ulong GetHashCode(ArraySegment<byte> data)
        {
            if (data == null || data.Array.Length == 0 || data.Count == 0)
            {
                return 0;
            }

            return Is32Bit
                ? (ulong)FarmHash.Hash32(data.Array, data.Offset, data.Count)
                : FarmHash.Hash64(data.Array, data.Offset, data.Count);
        }

        private ulong GetHashCode(byte[] data)
        {
            var dt = new ArraySegment<byte>(data);
            return GetHashCode(dt);
        }
    }
}