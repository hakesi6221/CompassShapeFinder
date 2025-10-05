using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

///<summary>
///


public class Compas : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject targetObject;
    [SerializeField]
    private AudioClip write;

    [SerializeField]
    private AudioClip update;

    [SerializeField]
    private AudioClip makeCircle;

    [SerializeField, Header("��]���x")]
    private float rotateSpeed;

    [SerializeField, Header("�~�̃v���n�u")]
    private GameObject circleObj;

    [SerializeField, Header("�R���p�X�̃y������")]
    private Transform pencil;

    [SerializeField, Header("���̃��X�g")]
    private List<GameObject> trails = new List<GameObject>();

    private GameObject currentCircle;
    [SerializeField]
    private Quaternion currentTargetRot;
    private bool isRotate = true;
    private bool isCircle = false;
    private bool moveAble = true;

    private float totalRotation = 0f; // �ݐω�]�ʂ�ǐ�
    private float previousAngle = 0f; // �O�t���[���̉�]�p�x

    private AudioSource audioSource;
    //public Quaternion C;
    // Start is called before the first frame update
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        currentTargetRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        if (moveAble)
        {
            transform.Rotate(0, 0, -360f * (Time.deltaTime / rotateSpeed));
            TrackRotation(); // ��]�ʂ�ǐ�
            if (isCircle == false)
            {
                DrawCircle();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Stop();
            StartCoroutine(UpdateNeedle());
        }
        
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(0, 0, 0.7f);
        //    GameObject clone = Instantiate(originalObject, targetObject.transform.position, targetObject.transform.rotation);
        //    Vector3 offset = new Vector3(0, 1, 0);
        //    clone.transform.Translate(translation: offset);
        //}

        
        
    }

    void DrawCircle()
    {
        if (isRotate)
        {
            GameObject clone = Instantiate(originalObject, pencil.position, Quaternion.identity);
            trails.Add(clone);
        }
        else
        {
            SEManager.instance.SE(makeCircle);
            isCircle = true;
            currentCircle = Instantiate(circleObj, transform.position, transform.rotation);
            ResetTraisl();
        }
    }

    IEnumerator UpdateNeedle()
    {
        moveAble = false;
        isRotate = true;
        SEManager.instance.SE(update);
        transform.position += transform.up * 2;
        transform.Rotate(0, 0, 180f);
        currentTargetRot.z = transform.eulerAngles.z;
        totalRotation = 0f;
        previousAngle = transform.eulerAngles.z;
        yield return new WaitForSeconds(0.2f);
        Destroy(currentCircle);
        isCircle = false;
        ResetTraisl();
        moveAble = true;
        audioSource.Play();
    }

    void ResetTraisl()
    {
        foreach (GameObject trail in trails)
        {
            Destroy(trail);
        }
    }
    void TrackRotation()
    {
        float currentAngle = transform.eulerAngles.z;

        // �O�t���[���Ƃ̉�]�ʂ��v�Z
        float deltaAngle = currentAngle - previousAngle;

        // �p�x���W�����v�����ꍇ��␳
        if (deltaAngle < -180f)
        {
            deltaAngle += 360f;
        }
        else if (deltaAngle > 180f)
        {
            deltaAngle -= 360f;
        }

        // �ݐω�]�ʂ����Z
        totalRotation += Mathf.Abs(deltaAngle);

        // ���]�i360�x�j�����m
        if (totalRotation >= 360f)
        {
            Debug.Log("���]���܂����I");
            isRotate = false;
            totalRotation = 0f; // �J�E���g�����Z�b�g
        }

        // ���݂̊p�x��ۑ�
        previousAngle = currentAngle;
    }
}
