using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h�p�̃p�l��")]
    Image panel;

    [SerializeField, Header("�t�F�[�h������܂ł̎���")]
    float fadeDuration;

    public bool panelEnabled { get { return panel.enabled; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FadeOut(string changeScene)
    {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0.0f);
        panel.enabled = true;                 // �p�l����L����
        float elapsedTime = 0.0f;                 // �o�ߎ��Ԃ�������
        Color startColor = panel.color;       // �t�F�[�h�p�l���̊J�n�F���擾
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); // �t�F�[�h�p�l���̍ŏI�F��ݒ�

        // �t�F�[�h�A�E�g�A�j���[�V���������s
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // �o�ߎ��Ԃ𑝂₷
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // �t�F�[�h�̐i�s�x���v�Z
            panel.color = Color.Lerp(startColor, endColor, t); // �p�l���̐F��ύX���ăt�F�[�h�A�E�g
            yield return null;                                     // 1�t���[���ҋ@
        }

        panel.color = endColor;  // �t�F�[�h������������ŏI�F�ɐݒ�
        //panel.enabled = false;
        SceneManager.LoadScene(changeScene); // �V�[�������[�h���ă��j���[�V�[���ɑJ��
    }

    private IEnumerator FadeIn()
    {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1.0f);
        panel.enabled = true;                 // �p�l����L����
        float elapsedTime = 0.0f;                 // �o�ߎ��Ԃ�������
        Color startColor = panel.color;       // �t�F�[�h�p�l���̊J�n�F���擾
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f); // �t�F�[�h�p�l���̍ŏI�F��ݒ�

        // �t�F�[�h�A�E�g�A�j���[�V���������s
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // �o�ߎ��Ԃ𑝂₷
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // �t�F�[�h�̐i�s�x���v�Z
            panel.color = Color.Lerp(startColor, endColor, t); // �p�l���̐F��ύX���ăt�F�[�h�A�E�g
            yield return null;                                     // 1�t���[���ҋ@
        }

        panel.color = endColor;  // �t�F�[�h������������ŏI�F�ɐݒ�
        panel.enabled = false;
    }

    public void FadeOutandLoadScene(string changeScene)
    {
        StartCoroutine(FadeOut(changeScene));
    }

    public void StartandFadeIn()
    {
        StartCoroutine(FadeIn());
    }
}
