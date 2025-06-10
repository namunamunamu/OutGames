using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI RubbyCountText;

    public TextMeshProUGUI BuyHealthText;



    private void Start()
    {
        Refresh();

        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    private void Update()
    {
        // Bad Code => 매니저 혹은 그에 준하는 코드에서 수행해줘야함
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BuyHealth();
        }
    }

    public void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);
        var rubby = CurrencyManager.Instance.Get(ECurrencyType.Rubby);

        GoldCountText.text = $"Gold: {gold.Value}";
        DiamondCountText.text = $"Diamond: {diamond.Value}";
        RubbyCountText.text = $"Rubby: {rubby.Value}";

        BuyHealthText.color = gold.HaveEnough(300) ? Color.green : Color.red;
    }

    public void BuyHealth()
    {
        if (CurrencyManager.Instance.UseCurrency(ECurrencyType.Gold, 300))
        {
            var player = FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);

            // TODO
            // 성공 이펙트
        }
        else
        {
            // TODO
            // 알림의 띄운다. (토스 메세지)
        }
    }

}
