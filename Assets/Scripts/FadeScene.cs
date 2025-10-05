using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
    [SerializeField, Header("フェード用のパネル")]
    Image panel;

    [SerializeField, Header("フェードしきるまでの時間")]
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
        panel.enabled = true;                 // パネルを有効化
        float elapsedTime = 0.0f;                 // 経過時間を初期化
        Color startColor = panel.color;       // フェードパネルの開始色を取得
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f); // フェードパネルの最終色を設定

        // フェードアウトアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // 経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // フェードの進行度を計算
            panel.color = Color.Lerp(startColor, endColor, t); // パネルの色を変更してフェードアウト
            yield return null;                                     // 1フレーム待機
        }

        panel.color = endColor;  // フェードが完了したら最終色に設定
        //panel.enabled = false;
        SceneManager.LoadScene(changeScene); // シーンをロードしてメニューシーンに遷移
    }

    private IEnumerator FadeIn()
    {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1.0f);
        panel.enabled = true;                 // パネルを有効化
        float elapsedTime = 0.0f;                 // 経過時間を初期化
        Color startColor = panel.color;       // フェードパネルの開始色を取得
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f); // フェードパネルの最終色を設定

        // フェードアウトアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // 経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // フェードの進行度を計算
            panel.color = Color.Lerp(startColor, endColor, t); // パネルの色を変更してフェードアウト
            yield return null;                                     // 1フレーム待機
        }

        panel.color = endColor;  // フェードが完了したら最終色に設定
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
