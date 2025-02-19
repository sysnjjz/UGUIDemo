///
/// �鿨������ӿؼ� ������ʾ�߼�
///
using UnityEngine;
using UnityEngine.UI;

public class CardDetailView : BasePanel
{
    //UI�ؼ�
    private Transform Star;
    private Image Type;
    private Text Name;
    private Image image;

    public CardDetailView(GameObject panel, Transform uiRoot)
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

    public override void ClosePanel()
    {
    }

    //��ʼ��
    private void Init()
    {
        Star = transform.Find("Star");
        Type = transform.Find("Type").GetComponent<Image>();
        Name = transform.Find("Name").GetComponent<Text>();
        image = transform.GetComponent<Image>();
    }
    
    //ˢ���߼�
    public void Refresh(HeroLocalItem heroLocalData,Hero hero)
    {
        //��ʾ���ͼƬ
        Texture2D pic = (Texture2D)Resources.Load("Icon/" + hero.type.ToString());
        Sprite tmp = Sprite.Create(pic, new Rect(0, 0, pic.width, pic.height), new Vector2(0, 0));
        Type.sprite = tmp;
        //��ʾ����ͼƬ
        Texture2D pic1 = (Texture2D)Resources.Load(hero.ImgPath);
        Sprite tmp1 = Sprite.Create(pic1, new Rect(0, 0, pic1.width, pic1.height), new Vector2(0, 0));
        image.sprite = tmp1;
        //��ʾ����
        Name.text = hero.name;
        //��������
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
}
