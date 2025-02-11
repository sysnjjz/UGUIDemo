using System.Collections.Generic;
using UnityEngine;

public enum HeroType
{
    Force,InnerForce,Heal,Sword,Skill,All
}

public enum HeroGrade
{
    Normal=1, 
    General=2,
    Good=3, 
    Great=4, 
    Legendary=5
}


[System.Serializable]
public class Hero 
{
    public int id;
    public HeroGrade rarity;
    public string name;
    public HeroType type;
    public string keyword;
    public string ImgPath;
    public string PrefabPath;
}

/// <summary>
/// Ӣ�۵ľ�̬���� ��Ӣ�۵��趨��
/// </summary>
public class HeroStaticData
{
    //����
    private static HeroStaticData instance;

    public static HeroStaticData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HeroStaticData();
            }
            return instance;
        }
    }

    //Ӣ���趨��
    private HeroTable heroTable;

    //���ؾ�̬����
    public HeroTable GetHeroTable()
    {
        if (heroTable == null)
        {
            heroTable = Resources.Load<HeroTable>("Table/HeroTable");
        }
        return heroTable;
    }

    //����id�õ�ָ����̬����
    public Hero GetHeroById(int id)
    {
        List<Hero> HeroList = GetHeroTable().HeroList;
        foreach (Hero hero in HeroList)
        {
            if (hero.id == id)
            {
                return hero;
            }
        }
        return null;
    }
}
