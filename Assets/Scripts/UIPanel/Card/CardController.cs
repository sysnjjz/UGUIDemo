using System.Collections.Generic;
using UnityEngine;

///
/// �鿨�����߼�
///
public class CardController
{
    private CardView cardView;

    public CardController()
    {
        CreatePanel();
        ShowPanel();
    }

    private void CreatePanel()
    {
        cardView = new CardView();
        GameObject cardPanelPrefab = null;
        if (!UIManager.Instance.prefabDict.TryGetValue(UIManager.Instance.pathDict[UIConst.DrawCard], out cardPanelPrefab))
        {
            cardView.BeforeInit(UIManager.Instance.pathDict[UIConst.DrawCard], "Prefab/Panel" + UIManager.Instance.pathDict[UIConst.DrawCard]);
        }
        else
        {
            cardView.gameObject = cardPanelPrefab;
        }
    }

    private void ShowPanel()
    {
        cardView.OpenPanel(UIManager.Instance.UIRoot);
        cardView.controller = this;
        cardView.Init();
    }

    public void OnClickClose()
    {
        UIManager.Instance.ClosePanel(UIConst.DrawCard);
    }

    //��һ�ſ�UI
    public void OneCard()
    {
        //��һ�ſ�
        HeroLocalItem hero = CardModel.Instance.GetRandomHero();
        //��ʾһ�ſ�
        cardView.RefreshOneCard(hero, HeroStaticData.Instance.GetHeroById(hero.id));
    }

    //��ʮ�ſ�UI��ʾ�߼�
    public void TenCard()
    {
        //��ʮ�ſ�
        List<HeroLocalItem> heroList = CardModel.Instance.GetRandomHero10();
        //��ʾʮ�ſ�
        cardView.TenCard(heroList);
    }
}
