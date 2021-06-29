namespace Chapter7.PhoneNumbers
{
    public class PhoneNumber
    {
        public PhoneNumber(string number, CountryCode countryCode, NumberType numberType)
        {
            NumberType = numberType;
            CountryCode = countryCode;
            Number = number;
        }

        public override string ToString()
        {
            return $"Number Type = {NumberType}; Country Code = {CountryCode}; Number = {Number}";
        }

        public NumberType NumberType { get; set; }
        public CountryCode CountryCode { get; set; }
        public string Number { get; set; }
    }
}
