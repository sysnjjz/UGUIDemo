using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView:BasePanel
{
    //UI�ؼ�
    private Transform CloseButton;
    private Transform OneButton;
    private Transform TenButton;
    public Transform CardList;

    //�ӿؼ�
    public GameObject cardDetail;

    //������
    public CardController controller;

    //���߼�
    public override void OpenPanel(Transform uiRoot)
    {
        base.OpenPanel(uiRoot);
        UIManager.Instance.panelDict.Add(UIConst.DrawCard, this);
    }

    //��ʼ��
    public void Init()
    {
        CloseButton = transform.Find("Panel/CloseButton");
        OneButton = transform.Find("Panel/One");
        TenButton = transform.Find("Panel/Ten");
        CardList = transform.Find("Panel/Card");

        cardDetail = Resources.Load<GameObject>("UI/Image");

        CloseButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.OnClickClose();
        });
        OneButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.OneCard();
        }); 
        TenButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.TenCard();
        }); 
    }

    //��һ�ſ�UI��ʾ�߼�
    public void RefreshOneCard(HeroLocalItem heroLocalItem,Hero hero)
    {
        //����ԭ�е�
        for (int i = 0; i < CardList.childCount; i++)
        {
            UnityEngine.Object.Destroy(CardList.GetChild(i).gameObject);
        }
        //��һ�ſ�
        CardDetailView newCardDetailView = new CardDetailView(cardDetail,CardList);
        newCardDetailView.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        //��ʾ�߼�
        newCardDetailView.Refresh(heroLocalItem,hero);
    }

    //��ʮ�ſ�UI��ʾ�߼�
    public void TenCard(List<HeroLocalItem> heroList)
    {
        //����ԭ�е�
        for (int i = 0; i < CardList.childCount; i++)
        {
            UnityEngine.Object.Destroy(CardList.GetChild(i).gameObject);
        }
        //��ʮ�ſ�
        for (int i = 0; i < heroList.Count; i++)
        {
            CardDetailView newCardDetailView = new CardDetailView(cardDetail, CardList);
            newCardDetailView.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //��ʾ�߼�
            newCardDetailView.Refresh(heroList[i], HeroStaticData.Instance.GetHeroById(heroList[i].id));
        }
    }
}
