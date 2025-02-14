using System.Collections.Generic;
using UnityEngine;

///
/// UI��������ͳһ������Ϸ�л���ֵĸ���UI
///
public class UIManager
{
    //����
    private static UIManager instance;

    public Transform uiRoot;

    //·�������ֵ�
    private Dictionary<string, string> pathDict;
    //Ԥ�Ƽ������ֵ�
    public Dictionary<string, GameObject> prefabDict;
    //�Ѵ򿪽��滺��
    public Dictionary<string, BasePanel> panelDict;
    //�Ѵ򿪽����ջ
    private Stack<BasePanel> panelStack;

    private UIManager()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
        pathDict = new Dictionary<string, string>()
        {
            { UIConst.HeroBackPack,"/HeroBackPack"},
            { UIConst.DrawCard,"/DrawCard"},
            { UIConst.MainMenu,"/MainMenu"}
        };
        panelStack = new Stack<BasePanel>();

    }
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    public Transform UIRoot
    {
        get
        {
            if (uiRoot == null)
            {
                if (GameObject.Find("Canvas"))
                {
                    uiRoot = GameObject.Find("Canvas").transform;
                }
                else
                {
                    uiRoot = new GameObject("Canvas").transform;
                }

            }
            return uiRoot;
        }
    }

    //�򿪽���
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;

        //�Ѿ��򿪵Ľ��治��
        if (panelDict.TryGetValue(name, out panel))
        {
            return null;
        }
        //�Ҳ�������Ҳ����
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            return null;
        }

        //���ɽ���
        UIFactory factory = new UIFactory();
        panel = factory.CreatePanel(name, path);

        //�����²���治�ɼ�
        if (panelStack.Count != 0)
        {
            panelStack.Peek().SetOnShow(false);
        }
        panelStack.Push(panel);

        return panel;
    }

    //�رս���
    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        //û�򿪵Ĳ���
        if (!panelDict.TryGetValue(name, out panel))
        {
            return false;
        }

        panelStack.Peek().IsRemove = true;
        panelStack.Peek().SetOnShow(false);
        UnityEngine.Object.Destroy(panelStack.Pop().gameObject);

        //�����²����ɼ�
        if (panelStack.Count != 0)
        {
            panelStack.Peek().SetOnShow(true);
        }

        //�Ƴ�����
        if (panelDict.ContainsKey(name))
        {
            panelDict.Remove(name);
        }

        return true;
    }
}

public class UIConst
{
    public const string HeroBackPack = "HeroBackPack";
    public const string DrawCard = "DrawCard";
    public const string MainMenu = "MainMenu";
} 

public class UIFactory
{
    public BasePanel CreatePanel(string name,string path)
    {
        switch (name)
        {
            case UIConst.MainMenu:
                MainPageView mainpagepanel = new MainPageView();

                GameObject mainpagepanelPrefab = null;
                if (!UIManager.Instance.prefabDict.TryGetValue(name, out mainpagepanelPrefab))
                {
                    mainpagepanel.BeforeInit(name, "Prefab/Panel" + path);
                }
                else
                {
                    mainpagepanel.gameObject = mainpagepanelPrefab;
                }

                //�򿪽���
                mainpagepanel.Initial(name, UIManager.Instance.UIRoot);

                MainPageController mainPageController = new MainPageController(mainpagepanel);

                return mainpagepanel;

            case UIConst.HeroBackPack:
                HeroWholePageWindow bagPanel = new HeroWholePageWindow();

                GameObject bagPanelPrefab = null;
                if (!UIManager.Instance.prefabDict.TryGetValue(name, out bagPanelPrefab))
                {
                    bagPanel.BeforeInit(name, "Prefab/Panel" + path);
                }
                else
                {
                    bagPanel.gameObject = bagPanelPrefab;
                }

                //�򿪽���
                bagPanel.Initial(name, UIManager.Instance.UIRoot);

                BagController bagController = new BagController(bagPanel);

                return bagPanel;

            case UIConst.DrawCard:
                CardView cardPanel = new CardView();

                GameObject cardPanelPrefab = null;
                if (!UIManager.Instance.prefabDict.TryGetValue(name, out cardPanelPrefab))
                {
                    cardPanel.BeforeInit(name, "Prefab/Panel" + path);
                }
                else
                {
                    cardPanel.gameObject = cardPanelPrefab;
                }

                //�򿪽���
                cardPanel.Initial(name, UIManager.Instance.UIRoot);

                CardController cardController = new CardController(cardPanel);

                return cardPanel;
            default:
                return null;
        }
    }
}
