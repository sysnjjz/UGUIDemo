///
/// ��Ϸ������ ��Ҫ������ͳһ������Ϸ�е�����
///
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    //����
    private static GameManager instance;
    //Ӣ���趨��
    private HeroTable heroTable;

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

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UIManager.Instance.OpenPanel(UIConst.MainMenu);
    }

    /// ������
    //���ؾ�̬����
    public HeroTable GetHeroTable()
    {
        if (heroTable == null)
        {
            heroTable = Resources.Load<HeroTable>("Table/HeroTable");
        }
        return heroTable;
    }

    //���ض�̬����
    public List<HeroLocalItem> GetHeroLocalData()
    {
        //LoadGame();
        //return HeroLocalData.Instance.LocalDataList;
        return HeroLocalData.Instance.LoadData();

    }

    //����id�õ�ָ����̬����
    public Hero GetHeroById(int id)
    {
        List<Hero> HeroList = GetHeroTable().HeroList;
        foreach(Hero hero in HeroList)
        {
            if(hero.id==id)
            {
                return hero;
            }
        }
        return null;
    }

    //����uid�õ�ָ����̬����
    public HeroLocalItem GetHeroLocalDataByUId(string uid)
    {
        List<HeroLocalItem> HeroLocalDataList = GetHeroLocalData();
        foreach(HeroLocalItem HeroLocalData in HeroLocalDataList)
        {
            if(HeroLocalData.uid==uid)
            {
                return HeroLocalData;
            }
        }
        return null;
    }

    //�õ�����Ӣ������
    public Dictionary<int, HeroLocalItem> GetDeployHeroDic()
    {
        //LoadGame();
        //return DeployedHero.Instance.DeployHeroesDic;
        return DeployedHero.Instance.LoadDeployData();
    }

    //�������Ӣ��
    public int AddDeployHero(int DeployID, HeroLocalItem AddHero)
    {
        Dictionary<int, HeroLocalItem> DeployHeroes = GetDeployHeroDic();
        int OriDeployID = 0;
        bool replace = false;
        foreach (var DeployHero in DeployHeroes)
        {
            //����Ѿ���ͬһ��Ӣ�۳����У�����Ӣ�����滻����Ӣ�ۣ�������ֱ������
            if (DeployHero.Value.id == AddHero.id)
            {
                OriDeployID = DeployHero.Key;
                DeployHeroes[OriDeployID] = AddHero;
                DeployHero.Value.IsDeploy = false;
                replace = true;
                break;
            }
        }
        if (!replace)
        {
            if (DeployHeroes.ContainsKey(DeployID))
            {
                DeployHeroes[DeployID].IsDeploy = false;
                DeployHeroes[DeployID] = AddHero;
            }
            else
            {
                DeployHeroes.Add(DeployID, AddHero);
            }
        }
        return OriDeployID;
    }

    //�鿨�߼�
    //��һ��
    public HeroLocalItem GetRandomHero()
    {
        int index = UnityEngine.Random.Range(0, GetHeroTable().HeroList.Count);
        Hero newHero = GetHeroTable().HeroList[index];
        HeroLocalItem newHeroLocalData = new()
        {
            uid = Guid.NewGuid().ToString(),
            id = newHero.id,
            level = 1,
            IsNew = true,
            IsDeploy = false,
            //����Ӧ���ھ�̬������дһ��ԭʼ�������� ������
            ATK = UnityEngine.Random.Range(4, 10) * 100,
            HP = UnityEngine.Random.Range(4, 8) * 100,
            Defence = UnityEngine.Random.Range(2, 6) * 100
        };
        HeroLocalData.Instance.LoadData();
        HeroLocalData.Instance.LocalDataList.Add(newHeroLocalData);
        HeroLocalData.Instance.SaveData();
        return newHeroLocalData;
    }
    //��ʮ��
    public List<HeroLocalItem> GetRandomHero10()
    {
        List<HeroLocalItem> newHeroLocalData = new();
        for(int i=0;i<10;i++)
        {
            HeroLocalItem newHero = GetRandomHero();
            newHeroLocalData.Add(newHero);
        }
        return newHeroLocalData;
    }

    ///����ź�������ݣ���ʾ�ڹ������У������ǰ��Ǽ���id�������id������Ϊ���и���Ӣ����չ�õ�
    public List<HeroLocalItem> GetSortHeroLocalData()
    {
        List<HeroLocalItem> HeroLocalDatas = HeroLocalData.Instance.LoadData();
        if(HeroLocalDatas.Count==0)
        {
            Debug.Log("CANNOT GET DATA");
            return null;
        }
        HeroLocalDatas.Sort(new HeroItemComparer());
        return HeroLocalDatas;
    }

    //������Ϸ
    public Save CreateSave()
    {
        Save save = new Save();
        save.HeroLocalDataSave = HeroLocalData.Instance.LocalDataList;
        save.DeployHeroesSave = DeployedHero.Instance.DeployHeroesDic;

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
            DeployedHero.Instance.DeployHeroesDic = save.DeployHeroesSave;
        }
        else
        {
            Debug.Log("No game saved!");
        }
        Debug.Log(HeroLocalData.Instance.LocalDataList[1]);
    }
}

public class HeroItemComparer:IComparer<HeroLocalItem>
{
    public int Compare(HeroLocalItem a,HeroLocalItem b)
    {
        Hero x = GameManager.Instance.GetHeroById(a.id);
        Hero y = GameManager.Instance.GetHeroById(b.id);

        //����ϡ�ж�����
        int rarityComparision=y.rarity.CompareTo(x.rarity);

        //ͬ��Ӣ�۰�id����
        if(rarityComparision==0)
        {
            int idComparision=y.id.CompareTo(x.id);
            return idComparision;
        }

        return rarityComparision;
    }
}
