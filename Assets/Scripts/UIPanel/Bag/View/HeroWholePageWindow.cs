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
    private HeroDownWindow UIDeployHero1;
    private HeroDownWindow UIDeployHero2;
    private HeroDownWindow UIDeployHero3;
    private HeroDownWindow UIDeployHero4;
    private HeroDownWindow UIDeployHero5;
    public HeroDownWindow[] UIDeployButtonArr;

    //������
    BagController bagController;

    //����
    override protected void Awake()
    {
        Init();
    }

    private void Init()
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
        UIDeployHero1 = transform.Find("Deploy/DeployList/DeployHero1").GetComponent<HeroDownWindow>();
        UIDeployHero2 = transform.Find("Deploy/DeployList/DeployHero2").GetComponent<HeroDownWindow>();
        UIDeployHero3 = transform.Find("Deploy/DeployList/DeployHero3").GetComponent<HeroDownWindow>();
        UIDeployHero4 = transform.Find("Deploy/DeployList/DeployHero4").GetComponent<HeroDownWindow>();
        UIDeployHero5 = transform.Find("Deploy/DeployList/DeployHero5").GetComponent<HeroDownWindow>();
        //��ӵ������з������
        UIDeployButtonArr = new HeroDownWindow[5];
        UIDeployButtonArr[0] = UIDeployHero1;
        UIDeployButtonArr[1] = UIDeployHero2;
        UIDeployButtonArr[2] = UIDeployHero3;
        UIDeployButtonArr[3] = UIDeployHero4;
        UIDeployButtonArr[4] = UIDeployHero5;

        //ҳ���ʼ��
        UICodex.gameObject.SetActive(false);
        UIHeroBag.gameObject.SetActive(true);

        //������
        bagController=GetComponent<BagController>();
    }

    private void ClickInit()
    {
        //ע�ᰴ���¼�
        UICloseButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickClose();
        }); 
        UIHeroButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickHero();
        }); 
        UICodexButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickCodex();
        }); 
        UIShowHero.onClick.AddListener(() =>
        {
            Attack();
        }); 
        UIPlusButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickPlus();
        }); 
        UIReferrerButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickReferrer();
        }); 
        UIAllButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickAll();
        }); 
        UIForceButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickForce();
        });
        UIInnerForceButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickInnerForce();
        }); 
        UIHealButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickHeal();
        }); 
        UISwordButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickSword();
        }); 
        UISkillButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.OnClickSkill();
        }); 
        UIUPButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            bagController.UpdateDeployHero();
        }); 
    }

    public void RefreshHeroDetail(HeroLocalItem heroLocalItem)
    {
        //��ʾӢ����Ϣ
        Hero hero = HeroStaticData.Instance.GetHeroById(heroLocalItem.id);
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
        GameObject ShowHero = Instantiate(NowPrefab, ChildPosition, ChildRotation, UIHero);
        ShowHero.transform.localScale = new Vector3(1100, 550, 550);
        ShowHero.transform.SetSiblingIndex(UIHero.childCount - 1);
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
        PlayerObj HeroPrefab = UIHero.GetChild(0).GetComponent<PlayerObj>();
        HeroPrefab.SetStateAnimationIndex(PlayerState.ATTACK);
        HeroPrefab.PlayStateAnimation(PlayerState.ATTACK);
    }
}
