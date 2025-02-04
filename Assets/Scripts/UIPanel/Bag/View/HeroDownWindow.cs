using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployButtonView : MonoBehaviour
{
    //��������
    public int ButtonID;
    //UI�ؼ�
    public Image UIImage;
    private Button UIButton;
    private Transform Image;
    private Transform ATK;

    //���ݴ洢
    private HeroLocalItem HeroLocalData;
    private Hero HeroTableData;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        UIImage = this.GetComponent<Image>();
        UIButton = this.GetComponent<Button>();
        ATK = transform.Find("ATK");
        Image = transform.Find("Image");

        UIButton.onClick.AddListener(OnClickButton);
    }

    //���ĵ�ǰѡ������
    private void OnClickButton()
    {
        transform.parent.parent.parent.GetComponent<BackPackCtrl>().chooseBid = ButtonID;
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
    public void Refresh(HeroLocalItem HeroLocalData)
    {
        this.HeroLocalData = HeroLocalData;
        this.HeroTableData = GameManager.Instance.GetHeroById(HeroLocalData.id);
        //��ʾͼƬ
        Texture2D pic = (Texture2D)Resources.Load(this.HeroTableData.ImgPath);
        Sprite tmp = Sprite.Create(pic, new Rect(0, 0, pic.width, pic.height), new Vector2(0, 0));
        transform.Find("Image").GetComponent<Image>().sprite = tmp;
        Color ncolor = transform.Find("Image").GetComponent<Image>().color;
        ncolor.a = 1;
        transform.Find("Image").GetComponent<Image>().color = ncolor;
        //��ʾս��
        transform.Find("ATK").GetComponent<Text>().text = this.HeroLocalData.ATK.ToString();
    }
}
