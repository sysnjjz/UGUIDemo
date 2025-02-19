using System;
using UnityEngine;

///
/// ����������������Ŀ���
/// �Ҷ���MVCģʽ������ǣ�Controller�ǹ�ͨview��model�ģ�view��model֮�䲻ֱ�ӵ��öԷ��������ṩ������controller����
/// ���ݶ��ڷ�������⣬controller��Ҫ����view�Ĵ�������ʾ
/// ��������controller���д�������ʾview�ķ������Լ�ҳ��ĳ�ʼ����������Ҫ����model�����ݵķ���
///
public class BagController
{
    //ҳ��
    private HeroWholePageWindow WholePageView;

    //��ǰѡ�еĸ���
    private string _chooseUid = Convert.ToString(-1);
    public string chooseUid
    {
        get
        {
            return _chooseUid;
        }
        set
        {
            _chooseUid = value;
            RefreshDetail();
        }
    }
    //��ǰѡ�е�����Ӣ����
    private int _chooseBid = 1;
    public int chooseBid
    {
        get
        {
            return _chooseBid;
        }
        set
        {
            //ʹԭ���İ���������
            WholePageView.UIDeployButtonArr[_chooseBid - 1].StopLighting();
            _chooseBid = value;
            if (HeroDeployedData.Instance.LoadDeployData().ContainsKey(_chooseBid))
            {
                chooseUid = HeroDeployedData.Instance.LoadDeployData()[_chooseBid].uid;
            }
            //�ҵ���Ӧ�İ�����ʹ�䷢��
            WholePageView.UIDeployButtonArr[_chooseBid - 1].Lighting();
        }
    }

    public BagController()
    {
        //���漰��������
        CreatePanel();
        ShowPanel();
        //��ʼ��
        Init();
        //������model�����ݵĽ����ʼ��
        RefreshScrollView(HeroType.All);
        RefreshDeployHero();
        RefreshDetail();
    }

    //��������ʼ��
    public void Init()
    {
        if (HeroDeployedData.Instance.LoadDeployData().ContainsKey(1) && HeroDeployedData.Instance.LoadDeployData()[1] != null)
        {
            chooseUid = HeroDeployedData.Instance.LoadDeployData()[1].uid;
        }
        else if (HeroLocalData.Instance.LoadData().Count != 0)
        {
            chooseUid = HeroLocalData.Instance.LoadData()[0].uid;
        }
    }

    //�������
    public void CreatePanel()
    {
        WholePageView=new HeroWholePageWindow();
        GameObject bagPagePrefab = null;
        if (!UIManager.Instance.prefabDict.TryGetValue(UIManager.Instance.pathDict[UIConst.HeroBackPack], out bagPagePrefab))
        {
            WholePageView.BeforeInit(UIManager.Instance.pathDict[UIConst.HeroBackPack], "Prefab/Panel" + UIManager.Instance.pathDict[UIConst.HeroBackPack]);
        }
        else
        {
            WholePageView.gameObject = bagPagePrefab;
        }
    }

    //��������
    public void ShowPanel()
    {
        WholePageView.OpenPanel(UIManager.Instance.UIRoot);
        WholePageView.controller = this;
        WholePageView.Init();
    }

    //�رս���
    public void ClosePanel()
    {
        UIManager.Instance.ClosePanel(UIConst.HeroBackPack);
    }

    //ˢ������Ӣ��
    //ʹ��model�е�����ˢ������Ӣ�۰�ť��view��ʾ
    public void RefreshDeployHero()
    {
        //�ҵ���Ӧ��ť
        if (HeroDeployedData.Instance.LoadDeployData().Count!=0)
        {
            for(int i=1;i<=5; i++)
            {
                if (HeroDeployedData.Instance.LoadDeployData().ContainsKey(i)&&HeroDeployedData.Instance.LoadDeployData()[i]!=null)
                {
                    //ˢ�½���
                    WholePageView.UIDeployButtonArr[i - 1].Refresh(HeroDeployedData.Instance.LoadDeployData()[i], HeroStaticData.Instance.GetHeroById(HeroDeployedData.Instance.LoadDeployData()[i].id));
                }
            }
        }
    }

    //ˢ������Ӣ��
    //ʹ��model�е������滻����Ӣ�۵�view��ʾ
    public void UpdateDeployHero()
    {
        //���Ӣ��
        if (HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid) != null && HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid).IsDeploy != true)
        {
            int oriID = HeroDeployedData.Instance.AddDeployHero(chooseBid, HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid));
            //�ҵ���Ӧ���� �滻��ʾҳ
            if (oriID == 0)
            {
                WholePageView.UIDeployButtonArr[chooseBid - 1].Refresh(HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid), HeroStaticData.Instance.GetHeroById(HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid).id));
            }
            else
            {
                WholePageView.UIDeployButtonArr[oriID - 1].Refresh(HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid), HeroStaticData.Instance.GetHeroById(HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid).id));
                chooseBid = oriID;
            }
        }
    }

    //ˢ������ҳ
    //ʹ��model�е�����ˢ������Ӣ������ҳ��view��ʾ
    public void RefreshDetail()
    {
        //�ҵ���Ӧ�Ķ�̬����
        HeroLocalItem heroLocalData = HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid);
        //Ϊ�շ���
        if (heroLocalData == null) return;
        //ˢ��չʾҳ
        WholePageView.RefreshHeroDetail(heroLocalData, HeroStaticData.Instance.GetHeroById(heroLocalData.id));
    }

    //ˢ��Ӣ���б�(������Ϊmodel��view�ĵ��ö��� ��̫�� �Եúܸ��� ��֪��Ҫ��Ҫ����һ��)
    //ʹ��model�е�����ˢ��Ӣ���б��view��ʾ
    public void RefreshScrollView(HeroType heroType)
    {
        //����������
        RectTransform scrollContent = WholePageView.UIHeroContent.content;
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            UnityEngine.Object.Destroy(scrollContent.GetChild(i).gameObject);
        }
        //Ϊ�շ���
        if (HeroLocalData.Instance.GetSortHeroLocalData() == null) return;
        //������ʾ
        foreach (HeroLocalItem HeroLocalData in HeroLocalData.Instance.GetSortHeroLocalData())
        {
            switch (heroType)
            {
                case HeroType.All:
                    HeroLeftSideWindow heroCell = new HeroLeftSideWindow(WholePageView.HeroUIItemPrefab, scrollContent);
                    heroCell.Refresh(HeroLocalData, HeroStaticData.Instance.GetHeroById(HeroLocalData.id),this);
                    break;
                case HeroType.Force:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Force)
                    {
                        HeroLeftSideWindow heroCellForce = new HeroLeftSideWindow(WholePageView.HeroUIItemPrefab, scrollContent);
                        heroCellForce.Refresh(HeroLocalData, HeroStaticData.Instance.GetHeroById(HeroLocalData.id), this);
                    }
                    break;
                case HeroType.InnerForce:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.InnerForce)
                    {
                        HeroLeftSideWindow heroCellInner = new HeroLeftSideWindow(WholePageView.HeroUIItemPrefab, scrollContent);
                        heroCellInner.Refresh(HeroLocalData, HeroStaticData.Instance.GetHeroById(HeroLocalData.id), this);
                    }
                    break;
                case HeroType.Heal:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Heal)
                    {
                        HeroLeftSideWindow heroCellHeal = new HeroLeftSideWindow(WholePageView.HeroUIItemPrefab, scrollContent);
                        heroCellHeal.Refresh(HeroLocalData, HeroStaticData.Instance.GetHeroById(HeroLocalData.id), this);
                    }
                    break;
                case HeroType.Sword:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Sword)
                    {
                        HeroLeftSideWindow heroCellSword = new HeroLeftSideWindow(WholePageView.HeroUIItemPrefab, scrollContent);
                        heroCellSword.Refresh(HeroLocalData, HeroStaticData.Instance.GetHeroById(HeroLocalData.id), this);
                    }
                    break;
                case HeroType.Skill:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Skill)
                    {
                        HeroLeftSideWindow heroCellSkill = new HeroLeftSideWindow(WholePageView.HeroUIItemPrefab, scrollContent);
                        heroCellSkill.Refresh(HeroLocalData, HeroStaticData.Instance.GetHeroById(HeroLocalData.id), this);
                    }
                    break;
            }
        }
    }
}
