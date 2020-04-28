using UnityEngine;

[CreateAssetMenu(menuName = "Game Data", fileName = "Game Data")]
public class GameData : ScriptableObject
{
    [Tooltip("Время для таймера, через которое фигура будет менять цвет на рандомный")]
    [SerializeField] private int observableTime = 1;

    public int ObservableTime { get => observableTime; private set { } }
}
