using UnityEngine;

[CreateAssetMenu(menuName = "Primitives/Click Color Data", fileName = "New Color Data")]
public class ClickColorData : ScriptableObject
{
    [Tooltip("Tип создаваемого объекта (куб, сфера, капсула)")]
    [SerializeField] private string objectType = "";
    public string ObjectType{ get => objectType; private set { } }

    [Tooltip("Минимальное количество кликов на объект для смены цвета")]
    [SerializeField]
    private int minClicksCount = 0;
    public int MinClicksCount { get => minClicksCount; private set { } }

    [Tooltip("Мaxимальное количество кликов на объект для смены цвета")]
    [SerializeField]
    private int maxClicksCount = 0;
    public int MaxClicksCount { get => maxClicksCount; private set { } }

    [Tooltip("Цвет фигуры")]
    [SerializeField]
    private Color color = Color.white;
    public Color Color { get => color; private set { } }
}
