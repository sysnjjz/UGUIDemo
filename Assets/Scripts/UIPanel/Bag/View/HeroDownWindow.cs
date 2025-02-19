using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ӿؼ�������һ������Ӣ�۸��ӵ���ͼ�߼�
/// </summary>
public class HeroDownWindow : BasePanel
{
    //��������
    public int ButtonID;
    //UI�ؼ�
    public Image UIImage;
    private Button UIButton;
    private Image Image;
    private Text ATK;

    //�������ű�
    public BagController bagController;

    //���캯��
    public HeroDownWindow(GameObject panel,Transform uiRoot,int buttonID)
    {
        gameObject = panel;
        OpenPanel(uiRoot);
        Init(buttonID);
    }

    //�򿪽���
    public override void OpenPanel(Transform uiRoot)
    {
        gameObject = GameObject.Instantiate(gameObject, uiRoot, false);
        transform = gameObject.transform;
    }

    //��ʼ��
    private void Init(int buttonID)
    {
        ButtonID = buttonID;

        UIImage = transform .GetComponent<Image>();
        UIButton = transform.GetComponent<Button>();
        ATK = transform.Find("ATK").GetComponent<Text>();
        Image = transform.Find("Image").GetComponent<Image>();

        UIButton.onClick.AddListener(OnClickButton);
    }

    //���ĵ�ǰѡ������
    public void OnClickButton()
    {
        bagController.chooseBid = ButtonID;
    }

    //������������
    public void StopLighting()
    {
        UIImage.color = new Color(0, 0, 0);
    }
    //����������
    public void Lighting()
    {
        UIImage.color = new Color(1, 1, 1);
    }

    //������ʾ����
    public void Refresh(HeroLocalItem heroLocalData,Hero hero)
    {
        //��ʾͼƬ
        Texture2D pic = (Texture2D)Resources.Load(hero.ImgPath);
        Sprite tmp = Sprite.Create(pic, new Rect(0, 0, pic.width, pic.height), new Vector2(0, 0));
        Image.sprite = tmp;
        Color ncolor = Image.color;
        ncolor.a = 1;
        Image.color = ncolor;
        //��ʾս��
        ATK.text = heroLocalData.ATK.ToString();
    }
}
