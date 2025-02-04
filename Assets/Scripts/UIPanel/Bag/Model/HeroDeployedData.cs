using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployedHero
{
    //����
    private static DeployedHero instance;

    public static DeployedHero Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DeployedHero();
            }
            return instance;
        }
    }

    //��ȡ���
    public Dictionary<int, HeroLocalItem> DeployHeroesDic;

    //�Ƿ���Ҫ���ش�ȡ�߼���
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
            DeployedHero depolyedHero= JsonUtility.FromJson<DeployedHero>(InventoryJson);
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
}
