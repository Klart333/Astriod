using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]
    private TextMeshPro scoreText;

    public int Score { get; private set; }

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        Score = 0;
    }

    public async void IncreaseScore(int amount, Vector3 pos)
    {
        var text = Instantiate(scoreText, pos, Quaternion.identity);
        AnimateText(text);

        float t = 0;
        int scoreLeft = amount;

        while (t <= 1.0f)
        {
            t += Time.deltaTime;

            int right = Mathf.FloorToInt(scoreLeft * Math.EaseInCubic(t));
            scoreLeft -= right;
            Score += right;

            text.text = Score.ToString();

            await Task.Yield();
        }

        await Task.Delay(100);
        Destroy(text);
    }

    private async void AnimateText(TextMeshPro text)
    {
        float t = 0;

        Vector3 zero = Vector3.zero;
        Vector3 one = text.transform.localScale;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * 5;

            text.transform.localScale = Vector3.Lerp(zero, one, Math.EaseInCubic(t));

            await Task.Yield();
        }

        await Task.Delay(500);

        while (t >= 0.0f)
        {
            t -= Time.deltaTime * 5;

            text.transform.localScale = Vector3.Lerp(zero, one, Math.EaseInCubic(t));

            await Task.Yield();
        }
    }
}
