
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum CrownImage
{
    Gold,
    Silver,
    Bronze,
    Iron,
    Wood
}

[Serializable]
public class RankBar_UI : MonoBehaviour
{
    public TextMeshProUGUI Nickname_Text;
    public TextMeshProUGUI Score_Text;
    public TextMeshProUGUI RankNumber_Text;
    public Image RankIcon_Image;

    private Crown[] conditions = new Crown[]
    {
        new GoldCrown(),
        new SilverCrown(),
        new BronzeCrown(),
        new IronCrown(),
        new WoodCrown()
    };
    
    [SerializeField]
    private Sprite[] rankSprites;
    
    public void Refresh(string nickname, int score, int rankNumber)
    {
        Nickname_Text.text = nickname;
        Score_Text.text = score.ToString();
        RankNumber_Text.text = rankNumber.ToString();

        CrownImage crownImage;

        foreach (var conditon in conditions)
        {
            if (conditon.IsSatisfy(rankNumber))
            {
                RankIcon_Image.sprite = rankSprites[(int)conditon.GetCrown()];
                break;
            }
        }


    }
}

public abstract class Crown
{
    public abstract bool IsSatisfy(int rankNumber);
    public abstract CrownImage GetCrown();

}

public class GoldCrown : Crown
{
    public override bool  IsSatisfy(int rankNumber)
    {
        return (rankNumber == 1);
    }
    
    public override CrownImage GetCrown()
    {
        return CrownImage.Gold;
    }
}

public class SilverCrown : Crown
{
    public override bool  IsSatisfy(int rankNumber)
    {
        return (rankNumber == 2);
    }
    
    public override CrownImage GetCrown()
    {
        return CrownImage.Silver;
    }
}

public class BronzeCrown : Crown
{
    public override bool  IsSatisfy(int rankNumber)
    {
        return (rankNumber == 3);
    }
    
    public override CrownImage GetCrown()
    {
        return CrownImage.Bronze;
    }
}

public class IronCrown : Crown
{
    public override bool  IsSatisfy(int rankNumber)
    {
        return (rankNumber <=20 && rankNumber >3);
    }
    
    public override CrownImage GetCrown()
    {
        return CrownImage.Iron;
    }
}

public class WoodCrown : Crown
{
    public override bool  IsSatisfy(int rankNumber)
    {
        return (rankNumber >20);
    }
    
    public override CrownImage GetCrown()
    {
        return CrownImage.Wood;
    }
}