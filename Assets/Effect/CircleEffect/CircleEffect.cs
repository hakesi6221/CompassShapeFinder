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
        // �p�[�e�B�N���V�X�e����Shape���W���[�����擾
        particleSystem = GetComponent<ParticleSystem>();
        shapeModule = particleSystem.shape;
    }

    // Update is called once per frame
    void Update()
    {
        // ���Ԃɉ�����Radius��ύX
        //float t = Mathf.PingPong(Time.time / duration, 1f);  // 0����1�̊Ԃŕω�
        //shapeModule.radius = Mathf.Lerp(radiusStart, radiusEnd, duration); // ���`��Ԃ�Radius��ύX
        
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
