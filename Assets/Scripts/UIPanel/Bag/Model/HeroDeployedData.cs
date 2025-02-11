using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ӣ������
/// </summary>
public class HeroDeployedData
{
    //����
    private static HeroDeployedData instance;

    public static HeroDeployedData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HeroDeployedData();
            }
            return instance;
        }
    }

    //��ȡ���
    public Dictionary<int, HeroLocalItem> DeployHeroesDic;

    //�浽����
    public void SaveDeployData()
    {
        string InventoryJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("DeployHeroData", InventoryJson);
        PlayerPrefs.Save();
    }

    //ȡ��������
    public Dictionary<int, HeroLocalItem> LoadDeployData()
    {
        //�Ѿ��ù�
        if (DeployHeroesDic != null)
        {
            return DeployHeroesDic;
        }
        //�Ѿ�ȡ��
        if (PlayerPrefs.HasKey("DeployHeroData"))
        {
            string InventoryJson = PlayerPrefs.GetString("DeployHeroData");
            HeroDeployedData depolyedHero= JsonUtility.FromJson<HeroDeployedData>(InventoryJson);
            DeployHeroesDic=depolyedHero.DeployHeroesDic;
            return DeployHeroesDic;
        }
        //��û��
        else
        {
            DeployHeroesDic=new Dictionary<int, HeroLocalItem>();
            return DeployHeroesDic;
        }
    }

    //�������Ӣ��
    public int AddDeployHero(int DeployID, HeroLocalItem AddHero)
    {
        Dictionary<int, HeroLocalItem> DeployHeroes = LoadDeployData();
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
}
