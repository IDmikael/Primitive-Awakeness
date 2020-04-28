using UnityEngine;

public class PrimitiveFigureSpawner : MonoBehaviour
{
    private GameObject[] figurePrefabs;

    private Vector3 offset = new Vector3(0, 0.5f, 0);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                if (mouseHit.transform.gameObject.layer != 8)
                    return;

                Collider[] hitColliders = Physics.OverlapSphere(mouseHit.point + offset, 0.5f);
                if (hitColliders.Length > 1)
                    return;

                GameObject spawnObject = GetRandomFigure();
                GeometryObjectData tempData = spawnObject.GetComponent<GeometryObjectModel>().Data;

                GameObject instance = Instantiate(spawnObject, mouseHit.point + offset, Quaternion.identity);
                instance.GetComponent<GeometryObjectModel>().Init(tempData);
            }
        }
    }

    /// <summary>
    /// Инициализация загруженных префабов
    /// </summary>
    public void SetupPrefabs(GameObject[] loadedPrefabs)
    {
        figurePrefabs = loadedPrefabs;
    }

    /// <summary>
    /// Возвращает рандомный объект из figurePrefabs
    /// </summary>
    private GameObject GetRandomFigure()
    {
        int index = Random.Range(0, figurePrefabs.Length);

        return figurePrefabs[index];
    }
}
