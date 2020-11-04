using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public struct Gtin14 : IEquatable<Gtin14>
    {
        [Column(TypeName = "char(14)")]
        public string Value { get; }

        internal Gtin14(string code)
        {
            Value = code;
        }

        /// <summary>
        /// Try to construct a valid GTIN-14 from any of the recognized standard barcode formats, such as EAN-13.
        /// </summary>
        /// <param name="code">A barcode in some standard format.</param>
        /// <exception cref="ArgumentException">Throw when the code supplied is not valid in any known standard format.</exception>
        /// <returns>A valid GTIN-14.</returns>
        public static bool TryFromStandardBarcode(string code, out Gtin14? gtin14)
        {
            switch (code.Length)
            {
                case 8:                     // EAN-8
                    {
                        code = "000" + "000" + code; break;
                    }
                case 12:                    // UPC, UCC-12, UPC-A, UPC-E
                    {
                        code = "00" + code; break;
                    }
                case 13:                    // EAN, JAN, EAN-13
                    {
                        code = "0" + code; break;
                    }
                case 14:                    // GTIN-14, UCC-14
                    break;
                default:
                    {
                        gtin14 = null;
                        return false;
                    }
            }

            gtin14 = new Gtin14(code);
            return true;
        }

        public override bool Equals(object? other) => other is Gtin14 gtin && Equals(gtin);

        public bool Equals(Gtin14 other) => Value == other.Value;

        public static bool operator ==(Gtin14 lhs, Gtin14 rhs) => lhs.Equals(rhs);
        public static bool operator !=(Gtin14 lhs, Gtin14 rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
