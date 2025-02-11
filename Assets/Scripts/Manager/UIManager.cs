using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///
/// UI��������ͳһ������Ϸ�л���ֵĸ���UI
///
public class UIManager
{
    //����
    private static UIManager instance;

    private Transform uiRoot;

    //·�������ֵ�
    private Dictionary<string, string> pathDict;
    //Ԥ�Ƽ������ֵ�
    private Dictionary<string, GameObject> prefabDict;
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
            if(instance==null)
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
            if(uiRoot==null)
            {
                if(GameObject.Find("Canvas"))
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
        //�Ѿ��򿪵Ľ���
        if(panelDict.TryGetValue(name,out panel))
        {
            return null;
        }

        //�Ҳ�������
        string path = "";
        if(!pathDict.TryGetValue(name,out path))
        {
            return null;
        }

        //ʹ��Ԥ���崴�����
        GameObject panelPrefab = null;
        if(!prefabDict.TryGetValue(name,out panelPrefab))
        {
            string realPath = "Prefab/Panel" + path;
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
            prefabDict.Add(name, panelPrefab );
        }

        //�򿪽���
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        //����UI����ģʽ
        if (panelObject.GetComponent<CanvasScaler>()==null)
        {
            panelObject.AddComponent<CanvasScaler>();
        }
        panelObject.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        panelObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
        panelObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel );

        //�����²���治�ɼ�
        if (panelStack.Count != 0)
        {
            panelStack.Peek().SetActive(false);
        }
        panelStack.Push(panel);

        return panel;
    }

    //�رս���
    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        //û�򿪵Ĳ���
        if(!panelDict.TryGetValue(name,out panel))
        {
            return false;
        }

        panelStack.Pop();
        //�����²����ɼ�
        if (panelStack.Count != 0)
        {
            panelStack.Peek().SetActive(true);
        }
        panel.ClosePanel(name);

        return true;
    }
}

public class UIConst
{
    public const string HeroBackPack = "HeroBackPack";
    public const string DrawCard = "DrawCard";
    public const string MainMenu = "MainMenu";
}



