///
/// ����д����Ӣ�۱����е��ӿؼ� ��Ҫ�ǵ���Ӣ�ۿ����߼�
///
using UnityEngine;
using UnityEngine.UI;

public class HeroCtrl : MonoBehaviour
{
    //UI���
    private Transform Grade;
    private Transform Type;
    private Transform Star;
    private Transform IsNew;

    private HeroLocalItem HeroLocalData;
    private Hero HeroTableData;
    private BackPackCtrl uiParent;

    private void Awake()
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
        Grade = transform.Find("Grade");
        Type = transform.Find("Type");
        Star = transform.Find("Star");
        IsNew = transform.Find("IsNew");
    }

    //����Ԥ������Ϣ
    public void Refresh(HeroLocalItem HeroLocalData,BackPackCtrl uiParent)
    {
        //���ݳ�ʼ��
        this.HeroLocalData=HeroLocalData;
        this.HeroTableData = GameManager.Instance.GetHeroById(HeroLocalData.id);
        this.uiParent = uiParent;

        //ϡ�ж���Ϣ
        Grade.GetComponent<Text>().text = HeroTableData.rarity.ToString();
        //�Ƿ���ʾ�»��
        IsNew.gameObject.SetActive(this.HeroLocalData.IsNew);
        //��ʾ����ͼƬ
        Texture2D pic1 = (Texture2D)Resources.Load(this.HeroTableData.ImgPath);
        Sprite tmp1 = Sprite.Create(pic1, new Rect(0, 0, pic1.width, pic1.height), new Vector2(0, 0));
        this.GetComponent<Image>().sprite = tmp1;
        //��ʾ���ͼƬ
        Texture2D pic2 = (Texture2D)Resources.Load("Icon/"+HeroTableData.type.ToString());
        Sprite tmp2 = Sprite.Create(pic2, new Rect(0, 0, pic2.width, pic2.height), new Vector2(0, 0));
        Type.gameObject.GetComponent<Image>().sprite = tmp2;
        //��ʾ�Ǽ�
        RefreshStars();
    }

    public void RefreshStars()
    {
        for(int i=0;i<Star.childCount;i++)
        {
            Transform uistar=Star.GetChild(i);
            if((int)(this.HeroTableData.rarity)>i)
            {
                uistar.gameObject.SetActive(true);
            }
            else
            {
                uistar.gameObject.SetActive(false);
            }
        }
    }

    private void ClickInit()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClickShowHero);
    }

    //��һ�� �������﹥������
    private void OnClickShowHero()
    {
        print("show hero");
        //�ظ�������ı䶯��
        if (this.uiParent.chooseUid == this.HeroLocalData.uid) return;
        //���ݵ�����¶���
        this.uiParent.chooseUid = this.HeroLocalData.uid;
    }
}