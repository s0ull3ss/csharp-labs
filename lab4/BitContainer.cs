using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class BitContainer : IEnumerable<bool>
    {
        public List<byte> list;
        private int length { get; set; }

        public int Length => length;

        public BitContainer()
        {
            list = new List<byte>();
            length = 0;
        }

        private void setBit(int position, int bit)
        {
            if (position >= length || position < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if(bit != 1 && bit != 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            int place = position / 8;
            int offset = position % 8;
            int curByte = list[place];
            curByte &= ~(1 << offset);
            list[place] = Convert.ToByte((curByte | (bit << offset)) & 0xff);
        }

        private void setBit(int position, bool bit)
        {
            int _bit = (bit) ? 1 : 0;
            setBit(position, _bit);
        }

        public void pushBit(int bit)
        {
            if (bit != 1 && bit != 0)
            {
                throw new ArgumentOutOfRangeException("bit must be 1 or 0");
            }
            int offset = (++length - 1) % 8;
            if (offset == 0)
            {
                list.Add(0);
            }
            setBit(length - 1, bit);
        }

        public void pushBit(bool bit)
        {
            int _bit = (bit) ? 1 : 0;
            pushBit(_bit);
        }

        public void Clear()
        {
            length = 0;
            list.Clear();
        }

        private int getBit(int position)
        {
            if (position >= length || position < 0)
            {
                throw new IndexOutOfRangeException();
            }
            int place = position / 8;
            int offset = position % 8;
            int value = list[place] & (1 << offset);
            return (value > 0) ? 1 : 0;
        }

        public int this[int position] {
            get {
                return getBit(position);
            }
            set {
                if (value == 1 || value == 0)
                {
                    setBit(position, value);
                }
            }
        }

        public void Insert(int place, int bit)
        {
            if (place >= length || place < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if(bit != 1 && bit != 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            pushBit(this[length-1]);
            for (int i = length - 2; i > place; --i)
            {
                this[i] = this[i - 1];
            }
            this[place] = bit;
        }

        public void Insert(int place, bool bit)
        {
            int _bit = (bit) ? 1 : 0;
            Insert(place, _bit);
        }

        public void Remove(int place)
        {
            if (place >= length || place < 0)
            {
                throw new IndexOutOfRangeException();
            }
            for (int i = place; i < length - 1; ++i)
            {
                this[i] = this[i + 1];
            }
            --length;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int bytes = (length - 1) / 8;
            int restInLast = (length - 1) % 8;
            for (int i = 0; i <= bytes - 1; ++i)
            {
                for (int j = 7; j >= 0; --j)
                {
                    sb.Append(this[i * 8 + j]);
                }
                sb.Append(" ");
            }
            for (int i = restInLast; i >= 0; --i)
            {
                sb.Append(this[bytes * 8 + i]);
            }
            return sb.ToString();
        }

        public IEnumerator<bool> GetEnumerator()
        {
            return new BitEnum(this);
        }



        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
}
