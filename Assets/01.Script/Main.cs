using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        // 도메인(콘텐츠): 해결하고자 하는 문제 영역, 지식 자체를 의미하며,
        //  도메인 모델링: 
        int gold = 100;
        int diamond = 34;

        Currency Gold = new Currency(ECurrencyType.Gold, gold);
        Currency Diamond = new Currency(ECurrencyType.Diamond, diamond);
    }
}
