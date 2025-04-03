
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class SceneLoader : MonoBehaviour
    {
        public static event Action OnLoadingTeek;
        public static event Action<float> OnLoadingProgress;
        public static event Action OnLoadingComplete; 

        public void LoadSceneAsync(string sceneName) => StartCoroutine(LoadYourAsyncScene(sceneName));

        private IEnumerator LoadYourAsyncScene(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while(asyncLoad.isDone == false)
            {
                // �������� �������� �������� ����� �������
                OnLoadingTeek?.Invoke();
                OnLoadingProgress?.Invoke(asyncLoad.progress);
                yield return null;
            }

            // ����������, ��� �������� ���������
            OnLoadingComplete?.Invoke();
        }
    }
}
