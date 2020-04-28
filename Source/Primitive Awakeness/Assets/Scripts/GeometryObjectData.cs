using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Primitives/Primitive Figure", fileName ="New Figure")]
public class GeometryObjectData : ScriptableObject
{
    [Tooltip("Object's click data: type, min/max clicks, color")]
    [SerializeField] private List<ClickColorData> clicksData;
    public List<ClickColorData> ClicksData { get => clicksData; private set { } }

    /// <summary>
    /// Инициализация списка ClickColorData
    /// </summary>
    /// <param name="_clicksData">отсортированный по типу фигуры список</param>
    public void InitClickDataList(List<ClickColorData> _clicksData)
    {
        clicksData = _clicksData;
    }
}
