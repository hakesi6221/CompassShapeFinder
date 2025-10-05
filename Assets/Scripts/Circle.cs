using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField]
    private AudioClip success;

    [SerializeField]
    private GameObject CircleEffect;
    [SerializeField]
    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(RandomUIPrefabSelector.selectedPrefabs[0].gameObject.tag))
        {
            SEManager.instance.SE(success);
            RandomUIPrefabSelector.success01 = true;
            Instantiate(CircleEffect, parent);
        }
        if (collision.gameObject.CompareTag(RandomUIPrefabSelector.selectedPrefabs[1].gameObject.tag))
        {
            SEManager.instance.SE(success);
            RandomUIPrefabSelector.success02 = true;
            Instantiate(CircleEffect, parent);
        }
        if (collision.gameObject.CompareTag(RandomUIPrefabSelector.selectedPrefabs[2].gameObject.tag))
        {
            SEManager.instance.SE(success);
            RandomUIPrefabSelector.success03 = true;
            Instantiate(CircleEffect, parent);
        }
    }
}
