using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeometryObjectModel : MonoBehaviour, IPointerDownHandler
{
    private GeometryObjectData data;
    public GeometryObjectData Data { get => data; private set { } }

    private int clickCount = 0;
    private Color primitiveColor;

    private CompositeDisposable disposables;

    private void OnEnable()
    {
        disposables = new CompositeDisposable(); // создаем disposable

        clickCount = 0;
    }

    private void OnDisable() 
    { 
        if (disposables != null)
        {
            disposables.Dispose(); // уничтожаем подписки
        }
    }

    private void Start()
    {
        if (primitiveColor == Color.clear)
            SetRandomColor();

        Observable.Timer(System.TimeSpan.FromSeconds(GameManager.gameData.ObservableTime)) // создаем timer Observable
            .Repeat() // делает таймер циклическим
            .Subscribe(_ =>
            { // подписываемся
                SetRandomColor();
            }).AddTo(disposables); // привязываем подписку к disposable
    }

    /// <summary>
    /// Инициалиация объекта
    /// </summary>
    public void Init(GeometryObjectData _data)
    {
        data = _data;

        CheckClicksDiapason();
    }

    /// <summary>
    /// Обработка клика по фигуре
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        clickCount++;
        CheckClicksDiapason();
    }

    /// <summary>
    /// Проверка, входит ли текущее количество кликов по фигуре в диапазон [MaxClicksCount, MinClicksCount]
    /// </summary>
    public void CheckClicksDiapason()
    {
        foreach(ClickColorData colorData in data.ClicksData)
        {
            if (clickCount >= colorData.MinClicksCount && clickCount <= colorData.MaxClicksCount)
                SetColor(colorData.Color);
        }
    }

    /// <summary>
    /// Устанавливаем конкретный цвет фигуре
    /// </summary>
    private void SetColor(Color color)
    {
        if (primitiveColor == color)
            return;

        primitiveColor = color;
        GetComponent<MeshRenderer>().material.color = color;
    }

    /// <summary>
    /// Устанавливаем рандомный цвет фигуре
    /// </summary>
    private void SetRandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float a = Random.Range(0f, 1f);

        Color randomColor = new Color(r, g, b, a);

        SetColor(randomColor);
    }
}
