using System;
using System.Text.RegularExpressions;

public class AccountNicknameSpecification : ISpecification<string>
{
    public string ErrorMassage { get; private set; }
    private int _minCharCount = 2;
    private int _maxCharCount = 7;

    // 부적절한 닉네임 필터
    private static readonly string[] ForbiddenNicknames = { "바보", "멍청이", "운영자", "김홍일" };
    private static readonly Regex _nicknameRegex = new Regex(@"^[가-힣a-zA-Z]+$");

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMassage = "닉네임은 비어있을 수 없습니다.";
            return false;
        }

        if (value.Length < _minCharCount || value.Length > _maxCharCount)
        {
            ErrorMassage = $"닉네임은 {_minCharCount}자 이상 {_maxCharCount}자 이하이어야 합니다.";
            return false;
        }

        if (!_nicknameRegex.IsMatch(value))
        {
            ErrorMassage = "닉네임은 한글 또는 영문만 사용할 수 있습니다.";
            return false;
        }

        foreach (var word in ForbiddenNicknames)
        {
            if (value.Contains(word))
            {
                ErrorMassage = $"'{word}'은(는) 사용할 수 없는 단어입니다.";
                return false;
            }
        }

        return true;
    }

}
