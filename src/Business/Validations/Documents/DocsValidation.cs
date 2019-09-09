using System;

namespace Business.Validations.Documents
{
    public abstract class CpfValidation
    {
        public const int CpfLength = 11;
        public static bool Validate(string cpf)
        {
            var cpfNumbers = Utils.OnlyNumber(cpf);
            if (!ValidLength(cpfNumbers))
                return false;

            return !HasRepeatedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
        }

        private static bool HasValidDigits(string cpfNumbers)
        {
            return true;
        }

        private static bool HasRepeatedDigits(string cpfNumbers)
        {
            return false;
        }

        private static bool ValidLength(string text)
        {
            return text.Length == CpfLength;
        }
    }

    public class CnpjValidation
    {
        public const int CpfLength = 14;
        public static bool Validate(string cnpj)
        {
            return true;
        }
    }

    public static class Utils
    {
        public static string OnlyNumber(string text)
        {
            return "0";
        }
    }
}
