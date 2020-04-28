using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PrimitiveFigureSpawner figureSpawner;

    private GameObject[] prefabs;

    public static GameData gameData;
    private GeometryObjectData[] geometryObjectDatas;
    private ClickColorData[] clickColorDatas;

    private string[] flatFiguresNames;
    private string[] roundedFiguresNames;
    private string json;

    private void Awake()
    {
        figureSpawner = GetComponent<PrimitiveFigureSpawner>();

        // Загрузка Scriptable Objects из ресурсов
        gameData = Resources.Load<GameData>("GameData");
        geometryObjectDatas = Resources.LoadAll<GeometryObjectData>("");
        clickColorDatas = Resources.LoadAll<ClickColorData>("ClickColorDatas");

        // Loading data
        try
        {
            LoadFiguresNames();
            LoadPrefabs();
        }catch (Exception e)
        {
            Debug.Log("Failed to load data. Error: " + e.Message);
            return;
        }

        // Setupping data
        SetupGeometryObjectDatas();
        SetupPrefabsDatas();

        figureSpawner.SetupPrefabs(prefabs);
    }

    /// <summary>
    /// Загрузка названий префабов из джсонки
    /// </summary>
    private void LoadFiguresNames()
    {
        json = File.ReadAllText(Application.streamingAssetsPath + PathsNames.FIGURES_NAMES);
        if (json == null)
        {
            Debug.Log("Failed to load FiguresNames!");
            throw new FileLoadException();
        } 

        flatFiguresNames = JsonUtility.FromJson<FiguresNames>(json).flat_figures_names;
        roundedFiguresNames = JsonUtility.FromJson<FiguresNames>(json).rounded_figures_names;
    }

    /// <summary>
    /// Загрузка префабов из ассет бандлов и их запись в массив prefabs
    /// </summary>
    private void LoadPrefabs()
    {
        // Loading prefabs from AssetBundles
        var flatFiguresAssetBundle
            = AssetBundle.LoadFromFile(Application.streamingAssetsPath + PathsNames.ASSET_BUNDLES_FIGURES_FLAT);
        var roundedFiguresAssetBundle
            = AssetBundle.LoadFromFile(Application.streamingAssetsPath + PathsNames.ASSET_BUNDLES_FIGURES_ROUNDED);
        if (flatFiguresAssetBundle == null || roundedFiguresAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            throw new FileLoadException();
        }

        // Writing prefabs into prefabs array
        prefabs = new GameObject[flatFiguresNames.Length + roundedFiguresNames.Length];
        int currentIndex = 0;
        foreach (string name in flatFiguresNames)
        {
            prefabs[currentIndex] = flatFiguresAssetBundle.LoadAsset<GameObject>(name);
            currentIndex++;
        }
        foreach (string name in roundedFiguresNames)
        {
            prefabs[currentIndex] = roundedFiguresAssetBundle.LoadAsset<GameObject>(name);
            currentIndex++;
        }
    }

    /// <summary>
    /// Отсортировывает все загруженные ClickColorData по типу фигуры и запихивает в нужный GeometryObjectData
    /// </summary>
    private void SetupGeometryObjectDatas()
    {
        foreach (GeometryObjectData data in geometryObjectDatas)
        {
            List<ClickColorData> colorDataList = new List<ClickColorData>();
            foreach (ClickColorData colorData in clickColorDatas)
            {
                if (data.name == colorData.ObjectType)
                {
                    colorDataList.Add(colorData);
                }
            }

            data.InitClickDataList(colorDataList);
        }
    }

    /// <summary>
    /// Добавляет собранные GeometryObjectData в каждый префаб по имени фигуры
    /// </summary>
    private void SetupPrefabsDatas()
    {
        foreach (GameObject prefab in prefabs)
        {
            foreach (GeometryObjectData data in geometryObjectDatas)
            {
                if (prefab.name == data.name)
                {
                    prefab.GetComponent<GeometryObjectModel>().Init(data);
                }
            }
        }
    }
}

public struct FiguresNames
{
    public string[] flat_figures_names;
    public string[] rounded_figures_names;
}
