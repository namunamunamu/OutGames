using System;

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class UI_InputField
{
    public TextMeshProUGUI resultText;
    public TMP_InputField EmailInputField;
    public TMP_InputField NicknameInputField;
    public TMP_InputField PWInputField;
    public TMP_InputField PWConfirmInputField;
    public Button ConfirmButton;
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;


    [Header("로그인")] public UI_InputField LoginInputFields;


    [Header("회원가입")] public UI_InputField RegisterInputFields;

    private AccountEmailSpecification _emailSpecification;
    private AccountNicknameSpecification _nicknameSpecification;
    private AccountPasswardSpecification _passwardSpecification;

    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
        // LoginCheck();

        _emailSpecification = new AccountEmailSpecification();
        _nicknameSpecification = new AccountNicknameSpecification();
        _passwardSpecification = new AccountPasswardSpecification();
    }

    public void OnClickResterButton()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void OnClickBackButton()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }

    public void Register()
    {
        string email = RegisterInputFields.EmailInputField.text;
        if (!_emailSpecification.IsSatisfiedBy(email))
        {
            RegisterInputFields.resultText.text = _emailSpecification.ErrorMassage;    
            RegisterInputFields.resultText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        string nickname = RegisterInputFields.NicknameInputField.text;
        if (!_nicknameSpecification.IsSatisfiedBy(nickname))
        {
            RegisterInputFields.resultText.text = _nicknameSpecification.ErrorMassage;
            RegisterInputFields.resultText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        // 비밀번호 입력을 확인
        string pw = RegisterInputFields.PWInputField.text;
        if(!_passwardSpecification.IsSatisfiedBy(pw))
        {
            RegisterInputFields.resultText.text = _passwardSpecification.ErrorMassage;
            RegisterInputFields.resultText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        // 2차 비밀번호 입력을 확인하고, 1차 비밀번호 입력과 같은지 확인
        string pwConfirm = RegisterInputFields.PWConfirmInputField.text;
        if(string.IsNullOrEmpty(pwConfirm) || pwConfirm != pw)
        {
            RegisterInputFields.resultText.text = "패스워드와 패스워드 확인이 같지 않습니다.";
            RegisterInputFields.resultText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        if (AccountManager.Instance.TryRegister(email, nickname, pw))
        {
            OnClickBackButton();
            Debug.Log("계정 생성 성공");
            LoginInputFields.EmailInputField.text = email;
        }
    }

    public void Login()
    {
        // 아이디 입력을 확인
        string email = LoginInputFields.EmailInputField.text;
        if (!_emailSpecification.IsSatisfiedBy(email))
        {
            LoginInputFields.resultText.text = _emailSpecification.ErrorMassage;
            LoginInputFields.resultText.rectTransform.DOShakeScale(0.2f);
            return;
        }

        // 비밀번호 입력을 확인
        string pw = LoginInputFields.PWInputField.text;
        if (!_passwardSpecification.IsSatisfiedBy(pw))
        {
            LoginInputFields.resultText.text = _passwardSpecification.ErrorMassage;
            LoginInputFields.resultText.rectTransform.DOShakeScale(0.2f);
            return;
        }


        if (!AccountManager.Instance.TryLogin(email, pw))
        {
            LoginInputFields.resultText.text = "아이디 혹은 패스워드가 틀렸습니다!";
            LoginInputFields.resultText.rectTransform.DOShakeScale(0.2f);
            return;
        }
        ;

        // 맞다면 로그인
        LoginInputFields.resultText.text = $"로그인 성공. {email}님 환영합니다.";
        LoginInputFields.resultText.rectTransform.DOShakeScale(0.2f);
        SceneManager.LoadScene(1);
    }

    public void LoginCheck()
    {
        string email = LoginInputFields.EmailInputField.text;
        string pw = LoginInputFields.PWInputField.text;

        LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(email)&&!string.IsNullOrEmpty(pw);
    }
}
