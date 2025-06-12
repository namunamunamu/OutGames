using UnityEngine;

public class AccountDTO : MonoBehaviour
{
    public string Email;
    public string Nickname;
    public string Password;

    public AccountDTO(Account account)
    {
        Email = account.Email;
        Nickname = account.Nickname;
        Password = account.Password;
    }
}
