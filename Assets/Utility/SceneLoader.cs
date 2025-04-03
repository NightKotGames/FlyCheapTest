
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class SceneLoader : MonoBehaviour
    {
        public event Action<float> OnLoadingProgress;
        public event Action OnLoadingComplete; 

        public void LoadSceneAsync(string sceneName) => StartCoroutine(LoadYourAsyncScene(sceneName));

        private IEnumerator LoadYourAsyncScene(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while(asyncLoad.isDone == false)
            {
                // �������� �������� �������� ����� �������
                OnLoadingProgress?.Invoke(asyncLoad.progress);
                yield return null;
            }

            // ����������, ��� �������� ���������
            OnLoadingComplete?.Invoke();
        }
    }
}
