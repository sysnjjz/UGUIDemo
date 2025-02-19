using System.Collections.Generic;
using UnityEngine;

///
/// UI��������ͳһ������Ϸ�л���ֵĸ���UI
///
public class UIManager
{
    //����
    private static UIManager instance;

    private Transform uiRoot;

    //·�������ֵ�
    public Dictionary<string, string> pathDict;
    //Ԥ�Ƽ������ֵ�
    public Dictionary<string, GameObject> prefabDict;
    //�Ѵ򿪽��滺��
    public Dictionary<string, BasePanel> panelDict;
    //�Ѵ򿪽����ջ
    public Stack<BasePanel> panelStack;

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
    public void OpenPanel(string name)
    {
        //�Ѿ��򿪵Ľ��治��
        BasePanel panel = null;
        if (panelDict.TryGetValue(name, out panel))
        {
            return;
        }

        //�Ҳ�������Ҳ����
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            return;
        }

        //�����²���治�ɼ�
        if (panelStack.Count != 0)
        {
            panelStack.Peek().SetOnShow(false);
        }

        //���ɶ�Ӧ������
        UIFactory factory = new UIFactory();
        factory.CreateController(name);
    }

    //�رս���
    public bool ClosePanel(string name)
    {
        //û�򿪵Ĳ���
        BasePanel panel = null;
        if (!panelDict.TryGetValue(name, out panel))
        {
            return false;
        }

        //�رն�Ӧ����
        BasePanel nowPanel= panelStack.Pop();
        nowPanel.ClosePanel();

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
    public void CreateController(string name)
    {
        switch (name)
        {
            case UIConst.MainMenu:
                MainPageController mainPageController = new MainPageController();
                break;

            case UIConst.HeroBackPack:
                BagController bagController = new BagController();
                break;

            case UIConst.DrawCard:
                CardController cardController = new CardController();
                break;
            default:
                break;
        }
    }
}
