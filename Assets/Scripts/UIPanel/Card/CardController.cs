using System.Collections.Generic;
using UnityEngine;

///
/// �鿨�����߼�
///
public class CardController
{
    //�ӿؼ�
    public CardDetailView cardDetail;
    private CardView cardView;

    public CardController(CardView view)
    {
        cardDetail = Resources.Load<GameObject>("UI/Image").GetComponent<CardDetailView>();
        cardView = view;
        view.controller = this;
        view.Init();
    }

    public void OnClickClose()
    {
        UIManager.Instance.ClosePanel(UIConst.DrawCard);
    }

    //��һ�ſ�UI��ʾ�߼�
    public void OneCard()
    {
        //����ԭ�е�
        for(int i=0;i<cardView.CardList.childCount;i++)
        {
            UnityEngine.Object.Destroy(cardView.CardList.GetChild(i).gameObject);
        }
        //��һ�ſ�
        HeroLocalItem hero = CardModel.Instance.GetRandomHero();
        Transform newHeroDetailWindow = UnityEngine.Object.Instantiate(cardDetail.transform, cardView.CardList) as Transform;
        newHeroDetailWindow.transform.localScale = new Vector3(0.8f,0.8f, 0.8f);
        //��ʾ�߼�
        newHeroDetailWindow.GetComponent<CardDetailView>().Refresh(hero);
    }

    //��ʮ�ſ�UI��ʾ�߼�
    public void TenCard()
    {
        //����ԭ�е�
        for (int i = 0; i < cardView.CardList.childCount; i++)
        {
            UnityEngine.Object.Destroy(cardView.CardList.GetChild(i).gameObject);
        }
        //��ʮ�ſ�
        List<HeroLocalItem> heroList= CardModel.Instance.GetRandomHero10();
        for(int i=0;i<heroList.Count;i++)
        {
            Transform newHeroDetailWindow = UnityEngine.Object.Instantiate(cardDetail.transform, cardView.CardList) as Transform;
            newHeroDetailWindow.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //��ʾ�߼�
            newHeroDetailWindow.GetComponent<CardDetailView>().Refresh(heroList[i]);
        }
    }
}
