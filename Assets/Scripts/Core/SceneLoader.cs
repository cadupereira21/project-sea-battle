using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core {
    public class SceneLoader : MonoBehaviour {
        
        [SerializeField]
        private LoadingScreen loadingScreen;
        
        public static SceneLoader Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else {
                Destroy(this.gameObject);
            }
        }

        public void LoadSceneAsync(string sceneName) {
            this.StopAllCoroutines();
            Instantiate(loadingScreen);
            this.StartCoroutine(CustomLoadSceneAsync(sceneName));
        }

        private IEnumerator CustomLoadSceneAsync(string sceneName) {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            Slider slider = loadingScreen.GetSlider();
            
            if (asyncLoad == null) {
                Debug.LogError($"[SceneLoader] Scene '{sceneName}' could not be loaded. Please check the scene name and ensure it is added to the build settings.");
                yield break;
            }

            while (!asyncLoad.isDone) {
                slider.value = asyncLoad.progress;
                yield return null;
            }
        }
    }
}