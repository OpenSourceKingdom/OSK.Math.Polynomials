using Fractions;
using OSK.Math.Polynomials.Exceptions;
using OSK.Math.Polynomials.Models;
using System;
using System.Numerics;

namespace OSK.Math.Polynomials
{
    public static class MathUtilities
    {
        public static EuclideanResult<BigInteger> EGcd(BigInteger a, BigInteger b)
        {
            BigInteger oldS = 1;
            BigInteger s = 0;
            BigInteger oldT = 0;
            BigInteger t = 1;
            while (b != 0)
            {
                var quotient = a / b;
                var temp = a;

                a = b;
                b = temp - quotient * b;

                temp = oldS;
                oldS = s;
                s = temp - quotient * s;

                temp = oldT;
                oldT = t;
                t = temp - quotient * t;
            }

            return new EuclideanResult<BigInteger>()
            {
                Gcd = a,
                U = oldS,
                V = oldT
            };
        }

        public static Fraction ModInverse(Fraction fraction, int modulo)
        {
            var result = EGcd(fraction.Denominator, modulo);
            if (result.Gcd != 1)
            {
                throw new ModInverseException($"The modular inverse between {fraction.Denominator} and {modulo} does not exist.");
            }

            var mod = result.U % modulo;
            var inverseResult = mod * fraction.Numerator % modulo;
            return inverseResult < 0 ? inverseResult + modulo : inverseResult;
        }

        public static bool TryModInverse(Fraction fraction, int modulo, out Fraction result)
        {
            var gcdResult = EGcd(fraction.Denominator, modulo);
            result = default;
            if (gcdResult.Gcd == 1)
            {
                var mod = gcdResult.U % modulo;
                var inverseResult = mod * fraction.Numerator % modulo;
                result = inverseResult < 0 ? inverseResult + modulo : inverseResult;
            }

            return gcdResult.Gcd == 1;
        }
    }
}
