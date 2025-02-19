using UnityEngine;
using UnityEngine.UI;

///
/// �ӿؼ�������һ��Ӣ�۱������е�Ӣ����ͼ�߼�
///
public class HeroLeftSideWindow : BasePanel
{
    //UI���
    private Text Grade;
    private Image Type;
    private Transform Star;
    private Transform IsNew;
    private Image image;

    HeroLocalItem heroLocalData;
    BagController bagController;

    //���캯��
    public HeroLeftSideWindow(GameObject panel,Transform uiRoot)
    {
        gameObject = panel;
        OpenPanel(uiRoot);
        Init();
    }

    //���߼�
    public override void OpenPanel(Transform uiRoot)
    {
        gameObject = GameObject.Instantiate(gameObject, uiRoot, false);
        transform = gameObject.transform;
    }

    //��ʼ��
    private void Init()
    {
        Grade = transform.Find("Grade").GetComponent<Text>();
        Type = transform.Find("Type").gameObject.GetComponent<Image>();
        Star = transform.Find("Star");
        IsNew = transform.Find("IsNew");
        image = transform.GetComponent<Image>();

        transform.GetComponent<Button>().onClick.AddListener(OnClickShowHero);
    }

    //����Ԥ������Ϣ
    public void Refresh(HeroLocalItem heroLocalData,Hero hero,BagController bagController)
    {
        //��ʾ��Ӣ��
        this.heroLocalData = heroLocalData;
        this.bagController = bagController;

        //ϡ�ж���Ϣ
        Grade.text = hero.rarity.ToString();
        //�Ƿ���ʾ�»��
        IsNew.gameObject.SetActive(heroLocalData.IsNew);
        //��ʾ����ͼƬ
        Texture2D pic1 = (Texture2D)Resources.Load(hero.ImgPath);
        Sprite tmp1 = Sprite.Create(pic1, new Rect(0, 0, pic1.width, pic1.height), new Vector2(0, 0));
        image.sprite = tmp1;
        //��ʾ���ͼƬ
        Texture2D pic2 = (Texture2D)Resources.Load("Icon/"+hero.type.ToString());
        Sprite tmp2 = Sprite.Create(pic2, new Rect(0, 0, pic2.width, pic2.height), new Vector2(0, 0));
        Type.sprite = tmp2;
        //��ʾ�Ǽ�
        RefreshStars(hero);
    }

    public void RefreshStars(Hero hero)
    {
        for(int i=0;i<Star.childCount;i++)
        {
            Transform uistar=Star.GetChild(i);
            if((int)(hero.rarity)>i)
            {
                uistar.gameObject.SetActive(true);
            }
            else
            {
                uistar.gameObject.SetActive(false);
            }
        }
    }

    //����չʾ������
    private void OnClickShowHero()
    {
        if (bagController == null || this.heroLocalData == null) return;
        //�ظ����������
        if (bagController.chooseUid == this.heroLocalData.uid) return;
        //��ͬ�����
        bagController.chooseUid = this.heroLocalData.uid;
    }
}