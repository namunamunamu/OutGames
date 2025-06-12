using UnityEngine;


public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    private Account _myAccount;
    public AccountDTO CurrentAccount => _myAccount.ToDTO();

    private AccountRepository _repository;
    private AccountPasswardSpecification _passwardSpecification;

    private const string SALT = "qwer1234";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        _repository = new AccountRepository();
        _passwardSpecification = new AccountPasswardSpecification();
    }

    public bool TryRegister(string email, string nickname, string password)
    {
        if (_repository.Find(email) != null)
        {
            Debug.LogError($"{email}은 이미 있는 계정입니다!");
            return false;
        }

        if (!_passwardSpecification.IsSatisfiedBy(password))
        {
            Debug.LogError(_passwardSpecification.ErrorMassage);
            return false;
        }

        string encryptedPassward = CryptoUtil.Encryption(password, SALT);
        Account account = new Account(email, nickname, encryptedPassward);
        _repository.Save(account.ToDTO());

        return true;
    }

    public bool TryLogin(string email, string password)
    {
        AccountSaveData accountSaveData = _repository.Find(email);
        if (accountSaveData == null || CryptoUtil.Verify(password, accountSaveData.Password))
        {
            Debug.LogError($"이메일 혹은 패스워드가 올바르지 않습니다");
            return false;
        }

        _myAccount = new Account(accountSaveData.Email, accountSaveData.Nickname, accountSaveData.Password);
        return true;
    }
    
    
}
