using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ӣ�۱�������Ҫ��ͼ�߼�
/// </summary>
public class HeroWholePageWindow : BasePanel
{
    //����ఴ������
    //UI�ؼ� �������ť�͹رհ�ť
    private Transform UICloseButton;
    private Transform UIHeroButton;
    private Transform UICodexButton;

    //�����ͼ���򱳰�����
    //ͼ��
    public Transform UICodex;
    //Ӣ�۱�������
    public Transform UIHeroBag;
    //����
    private Transform UIPlusButton;
    private Transform UIReferrerButton;
    //�м� Ӣ�۱�������
    public ScrollRect UIHeroContent;
    //����
    private Transform UIAllButton;
    private Transform UIForceButton;
    private Transform UIInnerForceButton;
    private Transform UIHealButton;
    private Transform UISwordButton;
    private Transform UISkillButton;

    //���ϲ�Ӣ��չʾҳ��
    public Button UIShowHero;
    public Transform UIHero;
    private Text UIHeroInfo_GradeText;
    private Text UIHeroInfo_NameText;
    private Text UIHeroInfo_KeyWordText;
    private Text UIATKText;
    private Transform Star;
    //�ӿؼ�����λ��
    Vector3 ChildPosition;
    //��ǰ��ʾԤ����
    private GameObject NowPrefab;
    //���²�Ӣ��������
    private Transform UIUPButton;
    public Transform UIDeployList;

    public HeroDownWindow[] UIDeployButtonArr;

    //Ԥ�Ƽ�������
    public GameObject HeroUIItemPrefab;
    public GameObject HeroDeployItem;

    //������
    public BagController controller;
    
    //�򿪽���
    public override void OpenPanel(Transform uiRoot)
    {
        base.OpenPanel(uiRoot);
        UIManager.Instance.panelDict.Add(UIConst.HeroBackPack, this);
    }

    //��ʼ��
    public void Init()
    {
        UIInit();
        ClickInit();
    }

    private void UIInit()
    {
        //����ఴ������
        //UI�ؼ� �������ť�͹رհ�ť
        UICloseButton = transform.Find("CloseButton");
        UIHeroButton = transform.Find("ButtonList/HeroButton");
        UICodexButton = transform.Find("ButtonList/CodexButton");

        //�����ͼ���򱳰�����
        //ͼ��
        UICodex = transform.Find("Codex");
        //Ӣ�۱�������
        UIHeroBag = transform.Find("HeroBag");
        //����
        UIPlusButton = transform.Find("HeroBag/UpperWindow/Capcity/PlusButton");
        UIReferrerButton = transform.Find("HeroBag/UpperWindow/ReferrerButton");
        //�м� Ӣ�۱�������
        UIHeroContent = transform.Find("HeroBag/HeroContent").GetComponent<ScrollRect>();
        //����
        UIAllButton = transform.Find("HeroBag/DownWindow/All");
        UIForceButton = transform.Find("HeroBag/DownWindow/Force");
        UIInnerForceButton = transform.Find("HeroBag/DownWindow/InnerForce");
        UIHealButton = transform.Find("HeroBag/DownWindow/Heal");
        UISwordButton = transform.Find("HeroBag/DownWindow/Sword");
        UISkillButton = transform.Find("HeroBag/DownWindow/Skill");

        //���ϲ�Ӣ��չʾҳ��
        UIShowHero = transform.Find("ShowHero").GetComponent<Button>();
        UIHero = transform.Find("ShowHero/Hero");
        UIHeroInfo_GradeText = transform.Find("ShowHero/HeroInfo/Grade").GetComponent<Text>();
        UIHeroInfo_NameText = transform.Find("ShowHero/HeroInfo/Name").GetComponent<Text>();
        UIHeroInfo_KeyWordText = transform.Find("ShowHero/HeroInfo/KeyWord").GetComponent<Text>();
        Star = transform.Find("ShowHero/HeroInfo/Star");
        UIATKText = transform.Find("ShowHero/ATK").GetComponent<Text>();
        ChildPosition = UIHero.gameObject.GetComponent<RectTransform>().transform.position;

        //���²�Ӣ��������
        UIUPButton = transform.Find("Deploy/DeployButton");
        UIDeployList= transform.Find("Deploy/DeployList");

        //Ԥ�Ƽ���ʼ��
        HeroUIItemPrefab = Resources.Load<GameObject>("UI/HeroDetail");
        HeroDeployItem = Resources.Load<GameObject>("UI/DeployHero");

        //��ʼ������������
        UIDeployButtonArr = new HeroDownWindow[5];
        //���������ť���������
        for (int i = 0; i < 5; i++)
        {
            HeroDownWindow heroDownWindow = new HeroDownWindow(HeroDeployItem, UIDeployList, i + 1);
            heroDownWindow.bagController = controller;
            UIDeployButtonArr[i] = heroDownWindow;
        }

        //ҳ���ʼ��
        UICodex.gameObject.SetActive(false);
        UIHeroBag.gameObject.SetActive(true);
    }

    private void ClickInit()
    {
        //ע�ᰴ���¼�
        //�ر�����ҳ��
        UICloseButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.ClosePanel();
        });

        //�ڲ���ʾ�߼�
        UIHeroButton.GetComponent<Button>().onClick.AddListener(OnClickHero);
        UICodexButton.GetComponent<Button>().onClick.AddListener(OnClickCodex);

        //�ڲ������߼�
        //����չʾ
        UIShowHero.onClick.AddListener(Attack);
        //�ڲ�������ʾ�߼�
        UIPlusButton.GetComponent<Button>().onClick.AddListener(OnClickPlus);
        UIReferrerButton.GetComponent<Button>().onClick.AddListener(OnClickReferrer);
        //�ڲ����������߼�
        UIAllButton.GetComponent<Button>().onClick.AddListener(OnClickAll);
        UIForceButton.GetComponent<Button>().onClick.AddListener(OnClickForce);
        UIInnerForceButton.GetComponent<Button>().onClick.AddListener(OnClickInnerForce);
        UIHealButton.GetComponent<Button>().onClick.AddListener(OnClickHeal);
        UISwordButton.GetComponent<Button>().onClick.AddListener(OnClickSword);
        UISkillButton.GetComponent<Button>().onClick.AddListener(OnClickSkill);
        //ʹ��model������ˢ��Ӣ��
        UIUPButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.UpdateDeployHero();
        });
    }

    public void RefreshHeroDetail(HeroLocalItem heroLocalItem,Hero hero)
    {
        //ϡ�ж���Ϣ
        UIHeroInfo_GradeText.text = hero.rarity.ToString();
        //������
        UIATKText.text = "ATK:" + heroLocalItem.ATK.ToString();
        //��ʾ����
        UIHeroInfo_NameText.text = hero.name.ToString();
        //��ʾ�ؼ���
        UIHeroInfo_KeyWordText.text = hero.keyword.ToString();

        ///��ʾԤ����
        //���ԭ��Ԥ����
        for (int i = 0; i < UIHero.childCount; i++)
        {
            GameObject.Destroy(UIHero.GetChild(i).gameObject);
        }
        //����µ�Ԥ����
        NowPrefab = (GameObject)Resources.Load(hero.PrefabPath);
        quaternion ChildRotation = new quaternion(0, 180, 0, 0);
        GameObject ShowHero = UnityEngine.Object.Instantiate(NowPrefab, ChildPosition, ChildRotation, UIHero);
        ShowHero.transform.position=new Vector3 (0, 0, 0);
        ShowHero.transform.localScale = new Vector3(900, 500, 450);
        //��ʾ�Ǽ�
        RefreshStars(hero);
    }

    public void RefreshStars(Hero hero)
    {
        for (int i = 0; i < Star.childCount; i++)
        {
            Transform uistar = Star.GetChild(i);
            if ((int)(hero.rarity) > i)
            {
                uistar.gameObject.SetActive(true);
            }
            else
            {
                uistar.gameObject.SetActive(false);
            }
        }
    }

    //��������
    public void Attack()
    {
        if (UIHero.childCount == 0) return;
        PlayerObj HeroPrefab = UIHero.GetChild(0).GetComponent<PlayerObj>();
        HeroPrefab.SetStateAnimationIndex(PlayerState.ATTACK);
        HeroPrefab.PlayStateAnimation(PlayerState.ATTACK);
    }

    public void OnClickHero()
    {
        UICodex.gameObject.SetActive(false);
        UIHeroBag.gameObject.SetActive(true);
    }

    public void OnClickCodex()
    {
        UIHeroBag.gameObject.SetActive(false);
        UICodex.gameObject.SetActive(true);
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
        controller.RefreshScrollView(HeroType.All);
    }

    public void OnClickForce()
    {
        Debug.Log("filter force");
        controller.RefreshScrollView(HeroType.Force);
    }

    public void OnClickInnerForce()
    {
        Debug.Log("filter innerforce");
        controller.RefreshScrollView(HeroType.InnerForce);
    }

    public void OnClickHeal()
    {
        Debug.Log("filter heal");
        controller.RefreshScrollView(HeroType.Heal);
    }

    public void OnClickSword()
    {
        Debug.Log("filter sword");
        controller.RefreshScrollView(HeroType.Sword);
    }

    public void OnClickSkill()
    {
        Debug.Log("filter skill");
        controller.RefreshScrollView(HeroType.Skill);
    }

}
