using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : Singleton<ScreenTransition>
{
    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene fromScene, Scene toScene)
    {
        FindObjectOfType<UITransitionHandler>().InSceneTransition();
    }

    public void LoadScene(int index)
    {
        LoadingLevel(index);
    }

    private async void LoadingLevel(int index)
    {
        UITransitionHandler transitionHandler = FindObjectOfType<UITransitionHandler>();
        if (transitionHandler != null)
        {
            transitionHandler.SceneTransition();
            await Task.Delay(700);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            await Task.Yield();
        }
    }
}
