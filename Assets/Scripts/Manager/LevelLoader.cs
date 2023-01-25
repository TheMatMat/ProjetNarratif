using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamSeven
{
    public class LevelLoader : MonoBehaviour
    {
        public static LevelLoader instance { get; private set; }

        [SerializeField] private float transitionTime;
        [SerializeField] private Animator animator;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        public void LoadNextScene()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        public void LoadMenuScene()
        {
            StartCoroutine(LoadLevel(0));
        }

        public void LoadSceneAnIndex(int index)
        {
            StartCoroutine(LoadLevel(index));
        }

        public void ExitApplication()
        {
            Application.Quit();
        }

        private IEnumerator LoadLevel(int index)
        {
            animator.SetTrigger("Transition");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(index);
        }
    }
}
