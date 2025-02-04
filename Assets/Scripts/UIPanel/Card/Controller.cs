///
/// �鿨�����߼�
///
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : BasePanel
{
    //UI�ؼ�
    private Transform CloseButton;
    private Transform OneButton;
    private Transform TenButton;
    private Transform CardList;

    public CardDetail cardDetail;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        CloseButton = transform.Find("Panel/CloseButton");
        OneButton = transform.Find("Panel/One");
        TenButton = transform.Find("Panel/Ten");
        CardList = transform.Find("Panel/Card");

        CloseButton.GetComponent<Button>().onClick.AddListener(OnClickClose);
        OneButton.GetComponent<Button>().onClick.AddListener(OneCard);
        TenButton.GetComponent<Button>().onClick.AddListener(TenCard);
    }

    private void OnClickClose()
    {
        UIManager.Instance.ClosePanel(UIConst.DrawCard);
    }

    //��һ�ſ�UI��ʾ�߼�
    private void OneCard()
    {
        //����ԭ�е�
        for(int i=0;i<CardList.childCount;i++)
        {
            Destroy(CardList.GetChild(i).gameObject);
        }
        //��һ�ſ�
        HeroLocalItem hero = GameManager.Instance.GetRandomHero();
        Transform newHeroDetail = Instantiate(cardDetail.transform, CardList) as Transform;
        newHeroDetail.transform.localScale = new Vector3(0.8f,0.8f, 0.8f);
        //��ʾ�߼�
        CardDetail cardInfo = newHeroDetail.GetComponent<CardDetail>();
        cardInfo.Refresh(hero,this);
    }

    //��ʮ�ſ�UI��ʾ�߼�
    private void TenCard()
    {
        //����ԭ�е�
        for (int i = 0; i < CardList.childCount; i++)
        {
            Destroy(CardList.GetChild(i).gameObject);
        }
        //��ʮ�ſ�
        List<HeroLocalItem> heroList= GameManager.Instance.GetRandomHero10();
        for(int i=0;i<heroList.Count;i++)
        {
            Transform newHeroDetail = Instantiate(cardDetail.transform, CardList) as Transform;
            newHeroDetail.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //��ʾ�߼�
            CardDetail cardInfo = newHeroDetail.GetComponent<CardDetail>();
            cardInfo.Refresh(heroList[i], this);
        }
    }
}
