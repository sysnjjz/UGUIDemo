/// <summary>
/// Ӣ�۵ľ�̬���� ����õ� ��Ӣ�۵��趨��
/// </summary>

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
