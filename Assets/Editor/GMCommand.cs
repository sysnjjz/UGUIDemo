using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class GMCommand
{
    [MenuItem("CMCommand/�������")]
    public static void ReadTable()
    {
        HeroTable heroTable = Resources.Load<HeroTable>("Table/HeroTable");
        foreach(Hero hero in heroTable.HeroList)
        {
            Debug.Log(string.Format("��id����{0}����name��:{1}", hero.id, hero.name));
        }
    }

    [MenuItem("CMCommand/������̬����")]
    public static void CreateHeroLocalData()
    {
        HeroLocalData.Instance.LocalDataList = new List<HeroLocalItem>();
        for(int i=1;i<=5;i++)
        {
            HeroLocalItem HeroLocalItemtmp = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                level = 1,
                IsNew = true,
                IsDeploy = false,
                ATK = 100 * i,
                HP = 1000 * i,
                Defence = 100 * i
            };
            HeroLocalData.Instance.LocalDataList.Add(HeroLocalItemtmp);
        }
        for (int i = 6; i <= 10; i++)
        {
            HeroLocalItem HeroLocalItemtmp = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i-5,
                level = 1,
                IsNew = true,
                IsDeploy = false,
                ATK = 100 * i,
                HP = 1000 * i,
                Defence = 100 * i
            };
            HeroLocalData.Instance.LocalDataList.Add(HeroLocalItemtmp);
        }

        HeroLocalData.Instance.SaveData();
    }

    [MenuItem("CMCommand/��ȡ��̬����")]
    public static void ReadHeroLocalData()
    {
        List<HeroLocalItem> readDatas= HeroLocalData.Instance.LoadData();
        foreach(HeroLocalItem HeroLocalData in readDatas)
        {
            Debug.Log(HeroLocalData);
        }
    }

    [MenuItem("CMCommand/�����̬����")]
    public static void ClearData()
    {
        HeroLocalData.Instance.LocalDataList.Clear();
        HeroLocalData.Instance.SaveData();
    }

    [MenuItem("CMCommand/�򿪱���")]
    public static void OpenPackage()
    {
        UIManager.Instance.OpenPanel(UIConst.HeroBackPack);
    }

    [MenuItem("CMCommand/���л�������Ϸ")]
    public static void SeSave()
    {
        GameManager.Instance.SaveGame();
    }

    [MenuItem("CMCommand/���л���ȡ��Ϸ")]
    public static void SeLoad()
    {
        GameManager.Instance.LoadGame();
    }
}
