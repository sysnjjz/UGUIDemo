using System;
using UnityEngine;

///
/// ����������������Ŀ���
///
public class BagController
{
    //Ԥ�Ƽ�������
    private GameObject HeroUIItemPrefab;

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

    public BagController(HeroWholePageWindow view)
    {
        WholePageView = view;
        view.controller = this;
        view.Init();
        Initial();
        RefreshScrollView(HeroType.All);
        RefreshDeployHero();
    }

    //��ʼ������
    private void Initial()
    {
        HeroUIItemPrefab = Resources.Load<GameObject>("UI/HeroDetail");
        if(HeroDeployedData.Instance.LoadDeployData().ContainsKey(1)&& HeroDeployedData.Instance.LoadDeployData()[1]!=null)
        {
            chooseUid = HeroDeployedData.Instance.LoadDeployData()[1].uid;
        }
        else if (HeroLocalData.Instance.LoadData().Count != 0)
        {
            chooseUid = HeroLocalData.Instance.LoadData()[0].uid;
        }
    }

    //ˢ������Ӣ��
    private void RefreshDeployHero()
    {
        if(HeroDeployedData.Instance.LoadDeployData().Count!=0)
        {
            for(int i=1;i<=5; i++)
            {
                if (HeroDeployedData.Instance.LoadDeployData().ContainsKey(i)&&HeroDeployedData.Instance.LoadDeployData()[i]!=null)
                {
                    WholePageView.UIDeployButtonArr[i - 1].Refresh(HeroDeployedData.Instance.LoadDeployData()[i]);
                }
            }
        }
    }

    //ˢ��Ӣ���б�
    private void RefreshScrollView(HeroType heroType)
    {
        //����������
        RectTransform scrollContent = WholePageView.UIHeroContent.content;
        for(int i=0;i<scrollContent.childCount;i++)
        {
            UnityEngine.Object.Destroy(scrollContent.GetChild(i).gameObject);
        }
        //��ʾ�ѻ��Ӣ��
        if (HeroLocalData.Instance.GetSortHeroLocalData() == null)  return;
        //������ʾ
        foreach(HeroLocalItem HeroLocalData in HeroLocalData.Instance.GetSortHeroLocalData())
        {
            switch (heroType)
            {
                case HeroType.All:
                    Transform HeroUIItem = UnityEngine.Object.Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                    HeroLeftSideWindow heroCell = HeroUIItem.GetComponent<HeroLeftSideWindow>();
                    heroCell.Refresh(HeroLocalData, this);
                    break;
                case HeroType.Force:
                    if(HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type==HeroType.Force)
                    {
                        Transform HeroUIItem1 = UnityEngine.Object.Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.InnerForce:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.InnerForce)
                    {
                        Transform HeroUIItem1 = UnityEngine.Object.Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Heal:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Heal)
                    {
                        Transform HeroUIItem1 = UnityEngine.Object.Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Sword:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Sword)
                    {
                        Transform HeroUIItem1 = UnityEngine.Object.Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Skill:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Skill)
                    {
                        Transform HeroUIItem1 = UnityEngine.Object.Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
            }
        }
    }

    //ˢ������ҳ
    private void RefreshDetail()
    {
        //�ҵ���Ӧ�Ķ�̬����
        HeroLocalItem heroLocalData = HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid);
        //ˢ��չʾҳ
        WholePageView.RefreshHeroDetail(heroLocalData);
    }

    //��������
    public void OnClickClose()
    {
        UIManager.Instance.ClosePanel(UIConst.HeroBackPack);
    }

    public void OnClickHero()
    {
        WholePageView.UICodex.gameObject.SetActive(false);
        WholePageView.UIHeroBag.gameObject.SetActive(true);
    }

    public void OnClickCodex()
    {
        WholePageView.UIHeroBag.gameObject.SetActive(false);
        WholePageView.UICodex.gameObject.SetActive(true);
    }

    public void OnClickPlus()
    {
        Debug.Log("plus capcity");
    }

    public void OnClickReferrer()
    {
        Debug.Log("show referrer");
    }

    public void OnClickAll()
    {
        Debug.Log("filter all");
        RefreshScrollView(HeroType.All);
    }

    public void OnClickForce()
    {
        Debug.Log("filter force");
        RefreshScrollView(HeroType.Force);
    }

    public void OnClickInnerForce()
    {
        Debug.Log("filter innerforce");
        RefreshScrollView(HeroType.InnerForce);
    }

    public void OnClickHeal()
    {
        Debug.Log("filter heal");
        RefreshScrollView(HeroType.Heal);
    }

    public void OnClickSword()
    {
        Debug.Log("filter sword");
        RefreshScrollView(HeroType.Sword);
    }

    public void OnClickSkill()
    {
        Debug.Log("filter skill");
        RefreshScrollView(HeroType.Skill);
    }

    public void UpdateDeployHero()
    {
        //���Ӣ��
        if(HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid) != null && HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid).IsDeploy!=true)
        {
            int oriID = HeroDeployedData.Instance.AddDeployHero(chooseBid, HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid));
            //�ҵ���Ӧ���� �滻��ʾҳ
            if(oriID==0)
            {
                WholePageView.UIDeployButtonArr[chooseBid - 1].Refresh(HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid));
            }
            else
            {
                WholePageView.UIDeployButtonArr[oriID - 1].Refresh(HeroLocalData.Instance.GetHeroLocalDataByUId(chooseUid));
                chooseBid = oriID;
            }
        }

    }
}
