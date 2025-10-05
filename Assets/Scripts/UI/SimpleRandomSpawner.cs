using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRandomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> shapePrefabs; // 図形のPrefabリスト
    [SerializeField] private int spawnCount = 100; // 生成する図形の数
    [SerializeField] private float minDistanceBetweenShapes = 5f; // 図形同士の最小距離

    private Bounds deskBounds; // Deskオブジェクトの範囲
    private List<GameObject> spawnedShapes = new List<GameObject>();

    void Start()
    {
        // タグがDeskのオブジェクトを取得
        GameObject deskObject = GameObject.FindWithTag("Desk");
        if (deskObject == null)
        {
            Debug.LogError("Tag 'Desk' のオブジェクトが見つかりません！");
            return;
        }

        // Deskの範囲を取得
        Renderer deskRenderer = deskObject.GetComponent<Renderer>();
        if (deskRenderer == null)
        {
            Debug.LogError("DeskオブジェクトにRendererがありません！");
            return;
        }
        deskBounds = deskRenderer.bounds;

        // Deskの範囲内に図形を生成
        GenerateShapesInDeskBounds();
    }

    private void GenerateShapesInDeskBounds()
    {
        int maxAttempts = 10; // ランダム生成の試行回数

        for (int i = 0; i < spawnCount; i++)
        {
            int attempts = 0;
            Vector3 randomPosition;

            do
            {
                // Deskの範囲内でランダムな位置を取得
                randomPosition = GetRandomPositionInDeskBounds(shapePrefabs[i % shapePrefabs.Count]);
                attempts++;
            } while (!IsPositionValid(randomPosition) && attempts < maxAttempts);

            // 試行回数を超えた場合は生成をスキップ
            if (attempts >= maxAttempts) continue;

            // 図形を生成
            GameObject newShape = Instantiate(shapePrefabs[Random.Range(0, shapePrefabs.Count)], randomPosition, Quaternion.identity);
            spawnedShapes.Add(newShape);
        }
    }

    private Vector3 GetRandomPositionInDeskBounds(GameObject shapePrefab)
    {
        // 図形のサイズ（Bounds）を取得
        Renderer shapeRenderer = shapePrefab.GetComponent<Renderer>();
        if (shapeRenderer == null)
        {
            Debug.LogError("図形PrefabにRendererがありません！");
            return Vector3.zero;
        }
        Bounds shapeBounds = shapeRenderer.bounds;

        // Deskの範囲内で、図形がはみ出さないように生成位置を計算
        float randomX = Random.Range(
            deskBounds.min.x + shapeBounds.extents.x,
            deskBounds.max.x - shapeBounds.extents.x
        );
        float randomY = Random.Range(
            deskBounds.min.y + shapeBounds.extents.y,
            deskBounds.max.y - shapeBounds.extents.y
        );
        float randomZ = Random.Range(
            deskBounds.min.z + shapeBounds.extents.z,
            deskBounds.max.z - shapeBounds.extents.z
        );

        return new Vector3(randomX, randomY, randomZ);
    }

    private bool IsPositionValid(Vector3 position)
    {
        // 既存の図形との距離をチェック
        foreach (GameObject shape in spawnedShapes)
        {
            if (Vector3.Distance(shape.transform.position, position) < minDistanceBetweenShapes)
            {
                return false;
            }
        }
        return true;
    }
}
