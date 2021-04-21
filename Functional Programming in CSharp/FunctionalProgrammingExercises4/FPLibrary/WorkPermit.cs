using System;

namespace FPLibrary
{
    public static partial class FP
    {
        public struct WorkPermit
        {
            public string Number { get; set; }
            public DateTime Expiry { get; set; }
        }
    }
}
