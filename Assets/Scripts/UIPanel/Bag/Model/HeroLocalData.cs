///
/// Ӣ�۶�̬���ݼ����洢����е�Ӣ������
///
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
