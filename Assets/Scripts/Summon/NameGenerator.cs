using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NameGenerator
{
    static string[] names = { "Li", "Zhang", "Wang", "Liu", "Zhao", "Han", "Wei", "Gao", "Shen", "Hu", 
        "Sun", "Zhu", "Qin", "Du", "Bai", "Wang", "Meng", "Fan", "Yang", "Wu" };
    static string[] surnames = { "Ming", "Wei", "Qiang", "Jianguo", "Junjie", "Xin", "Zhongxian", 
        "Tuan", "Jing", "Shizhi", "Wukong", "Xiu", "Qian", "Fu", "Juyi", "Zi", "Yangming" };

    public static string GenerateName()
    {
        return names[Random.Range(0, names.Length)] + " " + surnames[Random.Range(0, surnames.Length)];
    }
}
