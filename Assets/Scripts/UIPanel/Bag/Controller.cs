///
/// �˴�������������������Ŀ���
///
using System;
using UnityEngine;
using UnityEngine.UI;

public class BackPackCtrl : BasePanel
{
    ///UI�ؼ� �������ť�͹رհ�ť
    private Transform UICloseButton;
    private Transform UIHeroButton;
    private Transform UICodexButton;
    //UI�ؼ� Ӣ��չʾ����
    private Transform UIShowHero;
    private Transform UIHeroInfo_Grade;
    private Transform UIHeroInfo_Name;
    private Transform UIHeroInfo_Keyword;
    //UI�ؼ� Ӣ�۱�������
    private Transform UIHeroBag;
    private Transform UIHeroContent;
    //����
    private Transform UIPlusButton;
    private Transform UIReferrerButton;
    //����
    private Transform UIAllButton;
    private Transform UIForceButton;
    private Transform UIInnerForceButton;
    private Transform UIHealButton;
    private Transform UISwordButton;
    private Transform UISkillButton;
    //Ӣ��������
    private Transform UIUPButton;
    private Transform UIDeployHero1;
    private Transform UIDeployHero2;
    private Transform UIDeployHero3;
    private Transform UIDeployHero4;
    private Transform UIDeployHero5;
    private Transform[] UIDeployButtonArr;
    //ͼ��
    private Transform UICodex;
    //Ԥ�Ƽ�������
    public GameObject HeroUIItemPrefab;
    public GameObject HeroAnimationPrefab;
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
            UIDeployButtonArr[_chooseBid - 1].GetComponent<DeployButtonView>().StopLighting();
            _chooseBid = value;
            if (GameManager.Instance.GetDeployHeroDic().ContainsKey(_chooseBid))
            {
                chooseUid = GameManager.Instance.GetDeployHeroDic()[_chooseBid].uid;
            }
            //�ҵ���Ӧ�İ�����ʹ�䷢��
            UIDeployButtonArr[_chooseBid - 1].GetComponent<DeployButtonView>().Lighting();
        }
    }

    //����
    override protected void Awake()
    {
        base.Awake();
        Init();
    }

    private void Start()
    {
        RefreshUI();
        RefreshDeployHero();
    }

    private void Init()
    {
        UIInit();
        ClickInit();
    }

    private void UIInit()
    {
        ///UI�ؼ� �������ť�͹رհ�ť
        UICloseButton = transform.Find("CloseButton");
        UIHeroButton = transform.Find("ButtonList/HeroButton");
        UICodexButton = transform.Find("ButtonList/CodexButton");
        //UI�ؼ� Ӣ��չʾ����
        UIShowHero = transform.Find("ShowHero");
        UIHeroInfo_Grade = transform.Find("ShowHero/HeroInfo/Grade");
        UIHeroInfo_Name = transform.Find("ShowHero/HeroInfo/Name");
        UIHeroInfo_Keyword = transform.Find("ShowHero/HeroInfo/KeyWord");
        //UI�ؼ� Ӣ�۱�������
        UIHeroBag = transform.Find("HeroBag");
        UIHeroContent = transform.Find("HeroBag/HeroContent");
        //����
        UIPlusButton = transform.Find("HeroBag/UpperWindow/Capcity/PlusButton");
        UIReferrerButton = transform.Find("HeroBag/UpperWindow/ReferrerButton");
        //����
        UIAllButton = transform.Find("HeroBag/DownWindow/All");
        UIForceButton = transform.Find("HeroBag/DownWindow/Force");
        UIInnerForceButton = transform.Find("HeroBag/DownWindow/InnerForce");
        UIHealButton = transform.Find("HeroBag/DownWindow/Heal");
        UISwordButton = transform.Find("HeroBag/DownWindow/Sword");
        UISkillButton = transform.Find("HeroBag/DownWindow/Skill");
        //ͼ��
        UICodex = transform.Find("Codex");
        //����Ӣ����
        UIUPButton = transform.Find("Deploy/DeployButton");
        UIDeployHero1 = transform.Find("Deploy/DeployList/DeployHero1");
        UIDeployHero2 = transform.Find("Deploy/DeployList/DeployHero2");
        UIDeployHero3 = transform.Find("Deploy/DeployList/DeployHero3");
        UIDeployHero4 = transform.Find("Deploy/DeployList/DeployHero4");
        UIDeployHero5 = transform.Find("Deploy/DeployList/DeployHero5");
        //��ӵ������з������
        UIDeployButtonArr = new Transform[5];
        UIDeployButtonArr[0] = UIDeployHero1;
        UIDeployButtonArr[1] = UIDeployHero2;
        UIDeployButtonArr[2] = UIDeployHero3;
        UIDeployButtonArr[3] = UIDeployHero4;
        UIDeployButtonArr[4] = UIDeployHero5;
        //ҳ���ʼ��
        UICodex.gameObject.SetActive(false);
        UIHeroBag.gameObject.SetActive(true);
    }

    //����ʾȫ������ʼ��
    private void RefreshUI()
    {
        RefreshScrollView(HeroType.All);
    }

    private void RefreshDeployHero()
    {
        if(GameManager.Instance.GetDeployHeroDic().Count!=0)
        {
            for(int i=1;i<=5; i++)
            {
                if (GameManager.Instance.GetDeployHeroDic()[i]!=null)
                {
                    UIDeployButtonArr[i - 1].GetComponent<DeployButtonView>().Refresh(GameManager.Instance.GetDeployHeroDic()[i]);
                }
            }
        }
    }

    //ˢ��Ӣ���б�
    private void RefreshScrollView(HeroType heroType)
    {
        //����������
        RectTransform scrollContent = UIHeroContent.GetComponent<ScrollRect>().content;
        for(int i=0;i<scrollContent.childCount;i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        //��ʾ�ѻ��Ӣ��
        if (GameManager.Instance.GetSortHeroLocalData() == null) return;
        //������ʾ
        foreach(HeroLocalItem HeroLocalData in GameManager.Instance.GetSortHeroLocalData())
        {
            switch (heroType)
            {
                case HeroType.All:
                    Transform HeroUIItem = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                    HeroCtrl heroCell = HeroUIItem.GetComponent<HeroCtrl>();
                    heroCell.Refresh(HeroLocalData, this);
                    break;
                case HeroType.Force:
                    if(GameManager.Instance.GetHeroById(HeroLocalData.id).type==HeroType.Force)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroCtrl heroCell1 = HeroUIItem1.GetComponent<HeroCtrl>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.InnerForce:
                    if (GameManager.Instance.GetHeroById(HeroLocalData.id).type == HeroType.InnerForce)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroCtrl heroCell1 = HeroUIItem1.GetComponent<HeroCtrl>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Heal:
                    if (GameManager.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Heal)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroCtrl heroCell1 = HeroUIItem1.GetComponent<HeroCtrl>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Sword:
                    if (GameManager.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Sword)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroCtrl heroCell1 = HeroUIItem1.GetComponent<HeroCtrl>();
                        heroCell1.Refresh(HeroLocalData, this);
                    }
                    break;
                case HeroType.Skill:
                    if (GameManager.Instance.GetHeroById(HeroLocalData.id).type == HeroType.Skill)
                    {
                        Transform HeroUIItem1 = Instantiate(HeroUIItemPrefab.transform, scrollContent) as Transform;
                        HeroCtrl heroCell1 = HeroUIItem1.GetComponent<HeroCtrl>();
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
        HeroLocalItem HeroLocalData = GameManager.Instance.GetHeroLocalDataByUId(chooseUid);
        //ˢ��չʾҳ
        UIShowHero.GetComponent<HeroDetail>().Refresh(HeroLocalData, this);
    }

    private void ClickInit()
    {
        //ע�ᰴ���¼�
        UICloseButton.GetComponent<Button>().onClick.AddListener(OnClickClose);
        UIHeroButton.GetComponent<Button>().onClick.AddListener(OnClickHero);
        UICodexButton.GetComponent<Button>().onClick.AddListener(OnClickCodex);
        UIShowHero.GetComponent<Button>().onClick.AddListener(OnClickAttack);
        UIPlusButton.GetComponent<Button>().onClick.AddListener(OnClickPlus);
        UIReferrerButton.GetComponent<Button>().onClick.AddListener(OnClickReferrer);
        UIAllButton.GetComponent<Button>().onClick.AddListener(OnClickAll);
        UIForceButton.GetComponent<Button>().onClick.AddListener(OnClickForce);
        UIInnerForceButton.GetComponent<Button>().onClick.AddListener(OnClickInnerForce);
        UIHealButton.GetComponent<Button>().onClick.AddListener(OnClickHeal);
        UISwordButton.GetComponent<Button>().onClick.AddListener(OnClickSword);
        UISkillButton.GetComponent<Button>().onClick.AddListener(OnClickSkill);
        UIUPButton.GetComponent<Button>().onClick.AddListener(UpdateDeployHero);
    }

    private void OnClickClose()
    {
        UIManager.Instance.ClosePanel(UIConst.HeroBackPack);
    }

    private void OnClickHero()
    {
        UICodex.gameObject.SetActive(false);
        UIHeroBag.gameObject.SetActive(true);
    }

    private void OnClickCodex()
    {
        UIHeroBag.gameObject.SetActive(false);
        UICodex.gameObject.SetActive(true);
    }

    private void OnClickAttack()
    {
        print("animation->attack");
        UIShowHero.GetComponent<HeroDetail>().Attack();
    }

    private void OnClickPlus()
    {
        print("plus capcity");
    }

    private void OnClickReferrer()
    {
        print("show referrer");
    }

    private void OnClickAll()
    {
        print("filter all");
        RefreshScrollView(HeroType.All);
    }

    private void OnClickForce()
    {
        print("filter force");
        RefreshScrollView(HeroType.Force);
    }

    private void OnClickInnerForce()
    {
        print("filter innerforce");
        RefreshScrollView(HeroType.InnerForce);
    }

    private void OnClickHeal()
    {
        print("filter heal");
        RefreshScrollView(HeroType.Heal);
    }

    private void OnClickSword()
    {
        print("filter sword");
        RefreshScrollView(HeroType.Sword);
    }

    private void OnClickSkill()
    {
        print("filter skill");
        RefreshScrollView(HeroType.Skill);
    }

    private void UpdateDeployHero()
    {
        //���Ӣ��
        if(GameManager.Instance.GetHeroLocalDataByUId(chooseUid) != null && GameManager.Instance.GetHeroLocalDataByUId(chooseUid).IsDeploy!=true)
        {
            GameManager.Instance.GetHeroLocalDataByUId(chooseUid).IsDeploy = true;
            int oriID = GameManager.Instance.AddDeployHero(chooseBid, GameManager.Instance.GetHeroLocalDataByUId(chooseUid));
            //�ҵ���Ӧ���� �滻��ʾҳ
            if(oriID==0)
            {
                UIDeployButtonArr[chooseBid - 1].GetComponent<DeployButtonView>().Refresh(GameManager.Instance.GetHeroLocalDataByUId(chooseUid));
            }
            else
            {
                UIDeployButtonArr[oriID - 1].GetComponent<DeployButtonView>().Refresh(GameManager.Instance.GetHeroLocalDataByUId(chooseUid));
                chooseBid = oriID;
            }
        }

    }
}
