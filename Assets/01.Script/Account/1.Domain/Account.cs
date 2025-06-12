using System;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {
        // 도메인 규칙을 객체로 캡슐해서 분리한다.
        // 도메인과 UI 모두 이 규칙을 만족하는지 검사하면 된다.
        // 캡슐화한 규칙: 명세(specification)

        // 이메일 검증
        AccountEmailSpecification emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMassage);
        }

        // 닉네임 검증
        AccountNicknameSpecification nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMassage);
        }

        // 비밀번호 검증
        if (string.IsNullOrEmpty(password))
            throw new Exception("비밀번호는 비어있을 수 없습니다.");

        // 통과 시 할당
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(this);
    }
}
