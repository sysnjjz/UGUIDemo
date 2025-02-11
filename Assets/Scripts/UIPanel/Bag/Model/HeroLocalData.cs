using System.Collections.Generic;
using UnityEngine;

///
/// Ӣ�۶�̬���ݼ����洢����е�Ӣ������
///
public class HeroLocalData
{
    //����
    private static HeroLocalData instance;

    public static HeroLocalData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HeroLocalData();
            }
            return instance;
        }
    }

    //��ȡ���
    public List<HeroLocalItem> LocalDataList;

    //�浽����
    public void SaveData()
    {
        string InventoryJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("HeroLocalData", InventoryJson);
        PlayerPrefs.Save();
    }

    //ȡ��������
    public List<HeroLocalItem> LoadData()
    {
        //�Ѿ��ù�
        if (LocalDataList != null)
        {
            return LocalDataList;
        }
        //�Ѿ�ȡ��
        if (PlayerPrefs.HasKey("HeroLocalData"))
        {
            string InventoryJson = PlayerPrefs.GetString("HeroLocalData");
            HeroLocalData HeroLocalData = JsonUtility.FromJson<HeroLocalData>(InventoryJson);
            LocalDataList = HeroLocalData.LocalDataList;
            return LocalDataList;
        }
        //��û��
        else
        {
            LocalDataList = new List<HeroLocalItem>();
            return LocalDataList;
        }
    }

    //����uid�õ�ָ����̬����
    public HeroLocalItem GetHeroLocalDataByUId(string uid)
    {
        List<HeroLocalItem> HeroLocalDataList = LoadData();
        foreach (HeroLocalItem HeroLocalData in HeroLocalDataList)
        {
            if (HeroLocalData.uid == uid)
            {
                return HeroLocalData;
            }
        }
        return null;
    }

    ///����ź��������
    public List<HeroLocalItem> GetSortHeroLocalData()
    {
        List<HeroLocalItem> HeroLocalDatas = HeroLocalData.Instance.LoadData();
        if (HeroLocalDatas.Count == 0)
        {
            Debug.Log("CANNOT GET DATA");
            return null;
        }
        HeroLocalDatas.Sort(new HeroItemComparer());
        return HeroLocalDatas;
    }
}

public class HeroItemComparer : IComparer<HeroLocalItem>
{
    public int Compare(HeroLocalItem a, HeroLocalItem b)
    {
        Hero x = HeroStaticData.Instance.GetHeroById(a.id);
        Hero y = HeroStaticData.Instance.GetHeroById(b.id);

        //����ϡ�ж�����
        int rarityComparision = y.rarity.CompareTo(x.rarity);

        //ͬ��Ӣ�۰�id����
        if (rarityComparision == 0)
        {
            int idComparision = y.id.CompareTo(x.id);
            return idComparision;
        }

        return rarityComparision;
    }
}

//Ӣ�۵Ļ�������
[System.Serializable]
public class HeroLocalItem
{
    public string uid;
    public int id;
    public int level;
    public bool IsNew;
    public bool IsDeploy;
    public int ATK;
    public int HP;
    public int Defence;

    //������
    public override string ToString()
    {
        return string.Format("��id����{0}����level����{1}",id,level);
    }
}
