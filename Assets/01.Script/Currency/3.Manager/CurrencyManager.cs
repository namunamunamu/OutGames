using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class CurrencyManager : MonoBehaviour
{
    // 매니저는 관리, 창구 역할
    // 도메인 규칙 로직이 아예 안들어 갈 순 없으나, 최소화해야함
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;

    private CurrencyRepository _repository;
    

    // 도메인에 변화가 있을 때, 호출되는 액션 
    public event Action OnDataChanged;



    // 미리 하는 성능 최적화의 90%는 의미가 없다. from. Martin C.
    // 과도하게 세분화할 경우 오히려 복잡도만 올릴 수 있다.
    // public event Action OnGoldChanged;
    // public event Action OnDiamondChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance);
        }

        Init();
    }

    private void Init()
    {
        _repository = new CurrencyRepository();

        _currencies = new Dictionary<ECurrencyType, Currency>();

        List<CurrencyDTO> loadedCurrencies = _repository.LoadCurrencies();
        if (loadedCurrencies == null)
        {
            for (int i = 0; i < (int)ECurrencyType.Count; ++i)
            {
                ECurrencyType type = (ECurrencyType)i;

                Currency currency = new Currency(type, 0);
                _currencies.Add(type, currency);
            }
        }
        else
        {
            for (int i = 0; i < (int)ECurrencyType.Count; ++i)
            {
                if (loadedCurrencies.Count - 1 < i)
                {
                    Currency newCurrency = new Currency((ECurrencyType)i, 0);
                    _currencies.Add(newCurrency.Type, newCurrency);
                    continue;
                }

                Currency currency = new Currency(loadedCurrencies[i].Type, loadedCurrencies[i].Value);
                _currencies.Add(currency.Type, currency);
            }
        }
    }

    private List<CurrencyDTO> ToDTOList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    public void AddCurrency(ECurrencyType type, int value)
    {
        _currencies[type].AddCurrency(value);

        if (type == ECurrencyType.Gold)
        {
            AchievementManager.Instance.Increase(EAchievementCondition.GoldCollect, value);
        }

        _repository.SaveCurrencies(ToDTOList());

        OnDataChanged?.Invoke();
    }

    public bool UseCurrency(ECurrencyType type, int value)
    {
        if (!_currencies[type].UseCurrency(value))
        {
            return false;
        }

        _repository.SaveCurrencies(ToDTOList());

        OnDataChanged?.Invoke();
        return true;
    }
}
