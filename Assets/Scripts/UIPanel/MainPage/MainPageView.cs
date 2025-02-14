using UnityEngine.UI;

public class MainPageView : BasePanel
{
    //UI�ؼ�
    private Button BagButton;
    private Button CardButton;
    private Button ExitButton;

    //������
    public MainPageController controller;

    public void Init()
    {
        BagButton = transform.Find("Bag").GetComponent<Button>();
        CardButton = transform.Find("Card").GetComponent<Button>();
        ExitButton = transform.Find("Exit").GetComponent<Button>();

        BagButton.onClick.AddListener(() =>
        {
            controller.OnClickBag();
        });
        CardButton.onClick.AddListener(() =>
        {
            controller.OnClickCard();
        });
        ExitButton.onClick.AddListener(() =>
        {
            controller.OnClickExit();
        });
    }
}
