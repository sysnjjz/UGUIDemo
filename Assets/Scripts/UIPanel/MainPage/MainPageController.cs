using UnityEngine;

///
/// �������߼�
///
public class MainPageController
{
    private MainPageView mainPageView;

    public MainPageController()
    {
        CreatePanel();
        ShowPanel();
    }

    //������Ƶ���ͼ
    private void CreatePanel()
    {
        mainPageView = new MainPageView();
        GameObject mainpagepanelPrefab = null;
        if (!UIManager.Instance.prefabDict.TryGetValue(UIManager.Instance.pathDict[UIConst.MainMenu], out mainpagepanelPrefab))
        {
            mainPageView.BeforeInit(UIManager.Instance.pathDict[UIConst.MainMenu], "Prefab/Panel" + UIManager.Instance.pathDict[UIConst.MainMenu]);
        }
        else
        {
            mainPageView.gameObject = mainpagepanelPrefab;
        }
    }

    //��ʾ���Ƶ���ͼ
    private void ShowPanel()
    {
        mainPageView.OpenPanel(UIManager.Instance.UIRoot);
        mainPageView.controller = this;
        mainPageView.Init();
    }
}
