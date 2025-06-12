using System.Text.RegularExpressions;
using UnityEngine;

public class AccountEmailSpecification : ISpecification<string>
{

    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMassage = "이메일은 비어있을 수 없습니다.";
            return false;
        }

        if (!EmailRegex.IsMatch(value))
        {
            ErrorMassage = "올바른 이메일 형식이 아닙니다.";
            return false;
        }

        return true;
    }

    public string ErrorMassage { get; private set; }
}
