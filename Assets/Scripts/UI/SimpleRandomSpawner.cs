using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRandomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> shapePrefabs; // �}�`��Prefab���X�g
    [SerializeField] private int spawnCount = 100; // ��������}�`�̐�
    [SerializeField] private float minDistanceBetweenShapes = 5f; // �}�`���m�̍ŏ�����

    private Bounds deskBounds; // Desk�I�u�W�F�N�g�͈̔�
    private List<GameObject> spawnedShapes = new List<GameObject>();

    void Start()
    {
        // �^�O��Desk�̃I�u�W�F�N�g���擾
        GameObject deskObject = GameObject.FindWithTag("Desk");
        if (deskObject == null)
        {
            Debug.LogError("Tag 'Desk' �̃I�u�W�F�N�g��������܂���I");
            return;
        }

        // Desk�͈̔͂��擾
        Renderer deskRenderer = deskObject.GetComponent<Renderer>();
        if (deskRenderer == null)
        {
            Debug.LogError("Desk�I�u�W�F�N�g��Renderer������܂���I");
            return;
        }
        deskBounds = deskRenderer.bounds;

        // Desk�͈͓̔��ɐ}�`�𐶐�
        GenerateShapesInDeskBounds();
    }

    private void GenerateShapesInDeskBounds()
    {
        int maxAttempts = 10; // �����_�������̎��s��

        for (int i = 0; i < spawnCount; i++)
        {
            int attempts = 0;
            Vector3 randomPosition;

            do
            {
                // Desk�͈͓̔��Ń����_���Ȉʒu���擾
                randomPosition = GetRandomPositionInDeskBounds(shapePrefabs[i % shapePrefabs.Count]);
                attempts++;
            } while (!IsPositionValid(randomPosition) && attempts < maxAttempts);

            // ���s�񐔂𒴂����ꍇ�͐������X�L�b�v
            if (attempts >= maxAttempts) continue;

            // �}�`�𐶐�
            GameObject newShape = Instantiate(shapePrefabs[Random.Range(0, shapePrefabs.Count)], randomPosition, Quaternion.identity);
            spawnedShapes.Add(newShape);
        }
    }

    private Vector3 GetRandomPositionInDeskBounds(GameObject shapePrefab)
    {
        // �}�`�̃T�C�Y�iBounds�j���擾
        Renderer shapeRenderer = shapePrefab.GetComponent<Renderer>();
        if (shapeRenderer == null)
        {
            Debug.LogError("�}�`Prefab��Renderer������܂���I");
            return Vector3.zero;
        }
        Bounds shapeBounds = shapeRenderer.bounds;

        // Desk�͈͓̔��ŁA�}�`���͂ݏo���Ȃ��悤�ɐ����ʒu���v�Z
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
        // �����̐}�`�Ƃ̋������`�F�b�N
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
