using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

///
/// ��Ϸ������ ��Ϸ��� ��ʱ�Ѵ洢Ҳд������
///
public class GameManager : MonoBehaviour
{
    //����
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    //����
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UIManager.Instance.OpenPanel(UIConst.MainMenu);
    }

    //������Ϸ
    public Save CreateSave()
    {
        Save save = new Save();
        save.HeroLocalDataSave = HeroLocalData.Instance.LocalDataList;
        save.DeployHeroesSave = HeroDeployedData.Instance.DeployHeroesDic;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSave();

        // д�ļ�
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log(save.HeroLocalDataSave[1]);
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //���ļ�
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();


            //��ֵ
            HeroLocalData.Instance.LocalDataList = save.HeroLocalDataSave;
            HeroDeployedData.Instance.DeployHeroesDic = save.DeployHeroesSave;
        }
        else
        {
            Debug.Log("No game saved!");
        }
        Debug.Log(HeroLocalData.Instance.LocalDataList[1]);
    }
}
