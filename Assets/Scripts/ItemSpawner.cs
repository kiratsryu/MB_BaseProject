using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject itemPrefab;
    public int count = 20; // 7주차에 이 숫자를 크게 늘려 성능 실험
    public float range = 8f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-range, range), 0.5f, Random.Range(-range, range));
            Instantiate(itemPrefab, pos, Quaternion.identity);
        }
    }
}
