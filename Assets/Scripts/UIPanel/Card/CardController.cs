using System.Collections.Generic;
using UnityEngine;

///
/// �鿨�����߼�
///
public class CardController : MonoBehaviour
{
    //�ӿؼ�
    public CardDetailView cardDetail;
    private CardView cardView;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        cardView=GetComponent<CardView>();
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
            Destroy(cardView.CardList.GetChild(i).gameObject);
        }
        //��һ�ſ�
        HeroLocalItem hero = CardModel.Instance.GetRandomHero();
        Transform newHeroDetailWindow = Instantiate(cardDetail.transform, cardView.CardList) as Transform;
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
            Destroy(cardView.CardList.GetChild(i).gameObject);
        }
        //��ʮ�ſ�
        List<HeroLocalItem> heroList= CardModel.Instance.GetRandomHero10();
        for(int i=0;i<heroList.Count;i++)
        {
            Transform newHeroDetailWindow = Instantiate(cardDetail.transform, cardView.CardList) as Transform;
            newHeroDetailWindow.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            //��ʾ�߼�
            newHeroDetailWindow.GetComponent<CardDetailView>().Refresh(heroList[i]);
        }
    }
}
