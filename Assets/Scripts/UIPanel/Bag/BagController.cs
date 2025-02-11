using System;
using UnityEngine;

///
/// ����������������Ŀ���
///
public class BagController : MonoBehaviour
{
    //Ԥ�Ƽ�������
    public GameObject HeroUIItemPrefab;
    public GameObject HeroAnimationPrefab;

    //ҳ��
    private HeroWholePageWindow WholePageView;

    //��ǰѡ�еĸ���
    private string _chooseUid=Convert.ToString(-1);
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

    private void Awake()
    {
        Init();
        Initial();
    }

    private void Init()
    {
        WholePageView = GetComponent<HeroWholePageWindow>();
    }

    //��ʼ������
    private void Initial()
    {
        if(HeroDeployedData.Instance.LoadDeployData().ContainsKey(1)&& HeroDeployedData.Instance.LoadDeployData()[1]!=null)
        {
            chooseUid = HeroDeployedData.Instance.LoadDeployData()[1].uid;
        }
        else if (HeroLocalData.Instance.LoadData().Count != 0)
        {
            chooseUid = HeroLocalData.Instance.LoadData()[0].uid;
        }
    }

    //����
    private void Start()
    {
        RefreshScrollView(HeroType.All);
        RefreshDeployHero();
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
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        //��ʾ�ѻ��Ӣ��
        if (HeroLocalData.Instance.GetSortHeroLocalData() == null) return;
        //������ʾ
        foreach(HeroLocalItem HeroLocalData in HeroLocalData.Instance.GetSortHeroLocalData())
        {
            switch (heroType)
            {
                case HeroType.All:
                    Transform HeroUIItem = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                    HeroLeftSideWindow heroCell = HeroUIItem.GetComponent<HeroLeftSideWindow>();
                    heroCell.Refresh(HeroLocalData, this);
                    break;
                case HeroType.Force:
                    if(HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type==HeroType.Force)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.InnerForce:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.InnerForce)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Heal:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Heal)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Sword:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Sword)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroLeftSideWindow heroCell1 = HeroUIItem1.GetComponent<HeroLeftSideWindow>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Skill:
                    if (HeroStaticData.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Skill)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
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
        print("plus capcity");
    }

    public void OnClickReferrer()
    {
        print("show referrer");
    }

    public void OnClickAll()
    {
        print("filter all");
        RefreshScrollView(HeroType.All);
    }

    public void OnClickForce()
    {
        print("filter force");
        RefreshScrollView(HeroType.Force);
    }

    public void OnClickInnerForce()
    {
        print("filter innerforce");
        RefreshScrollView(HeroType.InnerForce);
    }

    public void OnClickHeal()
    {
        print("filter heal");
        RefreshScrollView(HeroType.Heal);
    }

    public void OnClickSword()
    {
        print("filter sword");
        RefreshScrollView(HeroType.Sword);
    }

    public void OnClickSkill()
    {
        print("filter skill");
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
