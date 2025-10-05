using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem.ShapeModule shapeModule;
    public float CircleSpeed;
    public float ShrinkTime;
    public float ExpansionTime;
    // Start is called before the first frame update
    void Start()
    {
        // パーティクルシステムとShapeモジュールを取得
        particleSystem = GetComponent<ParticleSystem>();
        shapeModule = particleSystem.shape;
    }

    // Update is called once per frame
    void Update()
    {
        // 時間に応じてRadiusを変更
        //float t = Mathf.PingPong(Time.time / duration, 1f);  // 0から1の間で変化
        //shapeModule.radius = Mathf.Lerp(radiusStart, radiusEnd, duration); // 線形補間でRadiusを変更
        
        if(ShrinkTime >= 0)
        {
            ShrinkTime -= Time.deltaTime;
            shapeModule.radius -= CircleSpeed * Time.deltaTime;
        }
        else
        {
            ExpansionTime -= Time.deltaTime;
            shapeModule.radius += CircleSpeed * Time.deltaTime;
        }
        
        if(ExpansionTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
