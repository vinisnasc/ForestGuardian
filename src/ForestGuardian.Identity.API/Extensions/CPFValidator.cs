using System.Text.RegularExpressions;

namespace ForestGuardian.Identity.API.Extensions;

public class CPFValidator
{
    public static bool IsValid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = Regex.Replace(cpf, @"[^\d]", "");

        if (cpf.Length != 11)
            return false;

        if (new string(cpf[0], 11) == cpf)
            return false;

        return ValidateCpfDigits(cpf);
    }

    private static bool ValidateCpfDigits(string cpf)
    {
        int[] multipliers1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multipliers2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string cpfWithoutDigits = cpf.Substring(0, 9);
        string calculatedDigits = CalculateCpfDigit(cpfWithoutDigits, multipliers1);
        calculatedDigits += CalculateCpfDigit(cpfWithoutDigits + calculatedDigits, multipliers2);

        return cpf.EndsWith(calculatedDigits);
    }

    private static string CalculateCpfDigit(string baseCpf, int[] multipliers)
    {
        int sum = baseCpf.Select((digit, index) => (digit - '0') * multipliers[index]).Sum();
        int remainder = sum % 11;
        return remainder < 2 ? "0" : (11 - remainder).ToString();
    }
}