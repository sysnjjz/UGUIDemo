///
/// ������Ӣ��չʾҳ����߼�
///
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class HeroDetail : MonoBehaviour
{
    //UI�ؼ�
    private Transform UIHero;
    private Transform UIHeroInfo_Grade;
    private Transform UIHeroInfo_Name;
    private Transform UIHeroInfo_KeyWord;
    private Transform UIATK;
    private Transform Star;

    private HeroLocalItem HeroLocalData;
    private Hero HeroTableData;
    private BackPackCtrl uiParent;

    private GameObject NowPrefab;

    private void Awake()
    {
        InitUI();
        test();
    }

    //��ʼ�������õ�
    private void test()
    {
        if (GameManager.Instance.GetHeroLocalData().Count!=0)
        {
            Refresh(GameManager.Instance.GetHeroLocalData()[0], null);
            transform.parent.GetComponent<BackPackCtrl>().chooseUid = GameManager.Instance.GetHeroLocalData()[0].uid;
        }
    }

    private void InitUI()
    {
        UIHero = transform.Find("Hero");
        UIHeroInfo_Grade = transform.Find("HeroInfo/Grade");
        UIHeroInfo_Name = transform.Find("HeroInfo/Name");
        UIHeroInfo_KeyWord = transform.Find("HeroInfo/KeyWord");
        UIATK = transform.Find("ATK");
        Star = transform.Find("HeroInfo/Star");
    }

    public void Refresh(HeroLocalItem HeroLocalData, BackPackCtrl uiParent)
    {
        //���ݳ�ʼ��
        this.HeroLocalData = HeroLocalData;
        this.HeroTableData = GameManager.Instance.GetHeroById(HeroLocalData.id);
        this.uiParent = uiParent;

        //ϡ�ж���Ϣ
        UIHeroInfo_Grade.GetComponent<Text>().text = HeroTableData.rarity.ToString();
        //������
        UIATK.GetComponent<Text>().text="ATK:"+HeroLocalData.ATK.ToString();
        //��ʾ����
        UIHeroInfo_Name.GetComponent<Text>().text= HeroTableData.name.ToString();
        //��ʾ�ؼ���
        UIHeroInfo_KeyWord.GetComponent<Text>().text=HeroTableData.keyword.ToString();
        ///��ʾԤ����
        //���ԭ��Ԥ����
        for (int i = 0; i < UIHero.childCount; i++)
        {
            GameObject.Destroy(UIHero.GetChild(i).gameObject);
        }
        //����µ�Ԥ����
        NowPrefab = (GameObject)Resources.Load(HeroTableData.PrefabPath);
        Vector3 ChildPosition = UIHero.gameObject.GetComponent<RectTransform>().transform.position;
        quaternion ChildRotation = new quaternion(0, 180, 0, 0);
        GameObject ShowHero = Instantiate(NowPrefab, ChildPosition, ChildRotation, UIHero);
        ShowHero.transform.localScale = new Vector3(1100, 550, 550);
        ShowHero.transform.SetSiblingIndex(UIHero.childCount - 1);
        //��ʾ�Ǽ�
        RefreshStars();
    }

    public void RefreshStars()
    {
        for (int i = 0; i < Star.childCount; i++)
        {
            Transform uistar = Star.GetChild(i);
            if ((int)(this.HeroTableData.rarity) > i)
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
        PlayerObj HeroPrefab=this.transform.Find("Hero").GetChild(0).GetComponent<PlayerObj>();
        HeroPrefab.SetStateAnimationIndex(PlayerState.ATTACK);
        HeroPrefab.PlayStateAnimation(PlayerState.ATTACK);
    }

    public HeroLocalItem GetHeroLocalData()
    {
        return this.HeroLocalData;
    }
}
