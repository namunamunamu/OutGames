
public class AccountPasswardSpecification : ISpecification<string>
{
    public string ErrorMassage { get; private set; }
    private int _minCharCount = 6;
    private int _maxCharCount = 12;

    public bool IsSatisfiedBy(string value)
    {
        if (value.Length < _minCharCount || value.Length > _maxCharCount)
        {
            ErrorMassage = $"비밀번호는 {_minCharCount}자 이상 {_maxCharCount}자 이하이어야 합니다. {value.Length} || {value}";
            return false;
        }

        return true;
    }
}
