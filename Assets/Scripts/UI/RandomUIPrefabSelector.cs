using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomUIPrefabSelector : MonoBehaviour
{
    // 12��Prefab��o�^���郊�X�g
    [SerializeField] private List<GameObject> uiPrefabs;

    // UI�\���ʒu���w�肷��Transform�i3�p�Ӂj
    [SerializeField] private List<Image> displayPositions;

    [SerializeField] private List<GameObject> checkUI;

    public static List<GameObject> selectedPrefabs = new List<GameObject>();
    public static bool success01 = false;
    public static bool success02 = false;
    public static bool success03 = false;

    private void Awake()
    {
        success01 = false; success02 = false; success03 = false;
        if (uiPrefabs.Count < 3 || displayPositions.Count < 3)
        {
            Debug.LogError("Prefab�܂���Display Position������܂���");
            return;
        }

        // �����_����3�I��
        selectedPrefabs = SelectRandomPrefabs(3);
    }

    // �����_����Prefab��UI�ɕ\�����鏈��
    void Start()
    {
        

        // �I��Prefab���w��ʒu�ɕ\��
        for (int i = 0; i < selectedPrefabs.Count; i++)
        {
            //Instantiate(selectedPrefabs[i], displayPositions[i].position, displayPositions[i].rotation);
            displayPositions[i].sprite = selectedPrefabs[i].GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void Update()
    {
        if (success01) checkUI[0].SetActive(true);
        if (success02) checkUI[1].SetActive(true);
        if (success03) checkUI[2].SetActive(true);
    }

    // �����_���Ɏw�萔��Prefab��I�ԃ��\�b�h
    private List<GameObject> SelectRandomPrefabs(int count)
    {
        List<GameObject> selected = new List<GameObject>();
        List<GameObject> prefabsCopy = new List<GameObject>(uiPrefabs);
        System.Random random = new System.Random();

        for (int i = 0; i < count; i++)
        {
            if (prefabsCopy.Count == 0) break;

            int index = random.Next(prefabsCopy.Count);
            selected.Add(prefabsCopy[index]);
            prefabsCopy.RemoveAt(index);
        }

        return selected;
    }
}
