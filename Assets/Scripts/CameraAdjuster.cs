using UnityEngine;

public class AspectRatioController : MonoBehaviour
{
    // 目標のアスペクト比 (例: 16:9)
    public float targetAspect = 9.0f / 16.0f;

    void Update()
    {
        // 現在の画面のアスペクト比を取得
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // スケール係数を計算
        float scaleHeight = windowAspect / targetAspect;

        // カメラのViewportを調整
        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            // 画面が縦長の場合
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // 画面が横長の場合
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
