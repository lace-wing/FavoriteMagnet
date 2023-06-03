using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteMagnet
{
    public static class Util
    {
        public static bool Within(this int i, int min, int max)
        {
            return i >= min && i <= max;
        }
        public static void Plus(this ref EnInt x, int y)
        {
            x = EnInt.Clone(x, x + y);
        }
        public static void Minus(this ref EnInt x, int y)
        {
            x = EnInt.Clone(x, x - y);
        }
        public static void Multiply(this ref EnInt x, int y)
        {
            x = EnInt.Clone(x, x * y);
        }
        public static void Divide(this ref EnInt x, int y)
        {
            x = EnInt.Clone(x, x / y);
        }
    }
    public struct EnInt
    {
        public enum Limit
        {
            Cap,
            Cycle,
            ReMin,
            ReMax
        }

        private int value;
        private int min;
        private int max;
        public int Value { get { return value; } private set { this.value = value; } }
        public int Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
                max = Math.Max(value, max);
                this.value = this + 0;
            }
        }
        public int Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
                min = Math.Min(value, min);
                this.value = this + 0;
            }
        }
        public Limit Limiting;

        public EnInt()
        {
            Value = 0;
            Min = int.MinValue;
            Max = int.MaxValue;
            Limiting = Limit.Cap;
        }
        public EnInt(int value)
        {
            Value = value;
            Min = int.MinValue;
            Max = int.MaxValue;
            Limiting = Limit.Cap;
        }
        public EnInt(int value, int min, int max, Limit limiting)
        {
            Value = value;
            Min = min;
            Max = max;
            Limiting = limiting;
        }
        public static int operator +(EnInt x, int y)
        {
            int result = x.value + y;
            if (result.Within(x.min, x.max))
            {
                return result;
            }
            switch (x.Limiting)
            {
                default:
                    return Math.Clamp(result, x.min, x.max);
                case Limit.Cycle:
                    if (result < x.min)
                    {
                        result = x.max + (result - x.min) % (x.min - x.max);
                    }
                    else
                    {
                        result = x.min + (result - x.max) % (x.max - x.min);
                    }
                    return result;
                case Limit.ReMin:
                    return x.min;
                case Limit.ReMax:
                    return x.max;
            }
        }
        public static int operator -(EnInt x, int y)
        {
            int result = x.value - y;
            if (result.Within(x.min, x.max))
            {
                return result;
            }
            switch (x.Limiting)
            {
                default:
                    return Math.Clamp(result, x.min, x.max);
                case Limit.Cycle:
                    if (result < x.min)
                    {
                        result = x.max + (result - x.min) % (x.min - x.max);
                    }
                    else
                    {
                        result = x.min + (result - x.max) % (x.max - x.min);
                    }
                    return result;
                case Limit.ReMin:
                    return x.min;
                case Limit.ReMax:
                    return x.max;
            }
        }
        public static int operator *(EnInt x, int y)
        {
            int result = x.value * y;
            if (result.Within(x.min, x.max))
            {
                return result;
            }
            switch (x.Limiting)
            {
                default:
                    return Math.Clamp(result, x.min, x.max);
                case Limit.Cycle:
                    if (result < x.min)
                    {
                        result = x.max + (result - x.min) % (x.min - x.max);
                    }
                    else
                    {
                        result = x.min + (result - x.max) % (x.max - x.min);
                    }
                    return result;
                case Limit.ReMin:
                    return x.min;
                case Limit.ReMax:
                    return x.max;
            }
        }
        public static int operator /(EnInt x, int y)
        {
            int result = x.value / y;
            if (result.Within(x.min, x.max))
            {
                return result;
            }
            switch (x.Limiting)
            {
                default:
                    return Math.Clamp(result, x.min, x.max);
                case Limit.Cycle:
                    if (result < x.min)
                    {
                        result = x.max + (result - x.min) % (x.min - x.max);
                    }
                    else
                    {
                        result = x.min + (result - x.max) % (x.max - x.min);
                    }
                    return result;
                case Limit.ReMin:
                    return x.min;
                case Limit.ReMax:
                    return x.max;
            }
        }
        public static int operator %(EnInt x, int y)
        {
            return x.value % y;
        }

        public static EnInt Clone(EnInt x)
        {
            return new EnInt(x.value, x.min, x.max, x.Limiting);
        }
        public static EnInt Clone(EnInt x, int value)
        {
            return new EnInt(x.value, x.min, x.max, x.Limiting);
        }
        public static EnInt Clone(EnInt x, int value, int min, int max, Limit limiting)
        {
            return new EnInt(x.value, min, max, limiting);
        }
        public static void Plus(ref EnInt x, int y)
        {
            x = Clone(x, x + y);
        }
        public static void Minus(ref EnInt x, int y)
        {
            x = Clone(x, x - y);
        }
        public static void Multiply(ref EnInt x, int y)
        {
            x = Clone(x, x * y);
        }
        public static void Divide(ref EnInt x, int y)
        {
            x = Clone(x, x / y);
        }
    }
}
