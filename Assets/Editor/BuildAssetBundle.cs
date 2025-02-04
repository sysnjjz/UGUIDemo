using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// AssetBundle �������
/// </summary>
public class BuildAssetBundle
{
    /// <summary>
    /// ����������е�AssetBundles������
    /// </summary>
    [MenuItem("AssetBundleTools/BuildAllAssetBundles")]
    public static void BuildAllAB()
    {
        // ���AB���·��
        string strABOutPAthDir = string.Empty;

        // ��ȡ��StreamingAssets���ļ���·��
        strABOutPAthDir = Application.streamingAssetsPath;

        // �ж��ļ����Ƿ���ڣ����������½�
        if (Directory.Exists(strABOutPAthDir) == false)
        {
            Directory.CreateDirectory(strABOutPAthDir);
        }

        // �������AB��
        BuildPipeline.BuildAssetBundles(strABOutPAthDir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

    }
}