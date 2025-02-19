using UnityEngine;
using UnityEngine.UI;

public class MainPageView : BasePanel
{
    //UI�ؼ�
    private Button BagButton;
    private Button CardButton;
    private Button ExitButton;

    //������
    public MainPageController controller;

    //���߼�
    public override void OpenPanel(Transform uiRoot)
    {
        base.OpenPanel(uiRoot);
        UIManager.Instance.panelDict.Add(UIConst.MainMenu, this);
    }

    //��ʼ��
    public void Init()
    {
        BagButton = transform.Find("Bag").GetComponent<Button>();
        CardButton = transform.Find("Card").GetComponent<Button>();
        ExitButton = transform.Find("Exit").GetComponent<Button>();

        BagButton.onClick.AddListener(OnClickBag);
        CardButton.onClick.AddListener(OnClickCard);
        ExitButton.onClick.AddListener(OnClickExit);
    }

    //��������
    public void OnClickBag()
    {
        UIManager.Instance.OpenPanel(UIConst.HeroBackPack);
    }

    public void OnClickCard()
    {
        UIManager.Instance.OpenPanel(UIConst.DrawCard);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
