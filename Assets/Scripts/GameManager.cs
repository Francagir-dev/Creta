using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("LOADING SCREEN COMPONENTS")] [SerializeField]
    private GameObject loadingScreen;

    [SerializeField] private ProgressBar loadingProgressBar;

    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    private List<AsyncOperation> m_ScenesLoading = new List<AsyncOperation>();

    public void LoadScene(int sceneToUnload, int sceneToLoad)
    {
        //Activate load screen
        loadingScreen.gameObject.SetActive(true);
        //Adds all actions to the async list
        m_ScenesLoading.Add(SceneManager.UnloadSceneAsync(sceneToUnload));
        m_ScenesLoading.Add(SceneManager.LoadSceneAsync(sceneToLoad));

        StartCoroutine(GetSceneLoadProgress());
    }

    private float m_TotalSceneProgress;

    //Will check if there's any action on the scenes (Loading or unloading)
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < m_ScenesLoading.Count; i++)
        {
            while (!m_ScenesLoading[i].isDone)
            {
                m_TotalSceneProgress = 0;
                foreach (AsyncOperation operation in m_ScenesLoading)
                {
                    m_TotalSceneProgress += operation.progress;
                }

                m_TotalSceneProgress = (m_TotalSceneProgress / m_ScenesLoading.Count) * 100f;
                loadingProgressBar.Current = Mathf.RoundToInt(m_TotalSceneProgress);
                //it it has not finished, will wait
                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
    }
}