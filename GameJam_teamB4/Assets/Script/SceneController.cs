using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace lws
{
    public enum Scenes
    {
        Openning = 0,
        Loading = 1,
        Floor1 = 2,
        Floor2 = 3,
        Floor3 = 4,
        Floor4 = 5,
        Floor5 = 6,
        Credit = 7
    }

    public class SceneController : Singleton<SceneController>
    {
        public static Scenes nextScene;
        public static Scenes currentScene;
        public static int clearLevel;

        [SerializeField] public Scenes _nextScene;
        [SerializeField] public Scenes _currentScene;
        [SerializeField] public int _clearLevel;

        protected override void Awake()
        {
            dontDestroyOnLoad = true;
            base.Awake();
        }

        private void Update()
        {
            _nextScene = nextScene;
            _currentScene = currentScene;
            _clearLevel = clearLevel;
        }

        public void Init(Scenes startSCene, int level)
        {
            currentScene = startSCene;
            clearLevel = level;
        }

        public void SceneChange(Scenes nextSceneEnum)
        {
            nextScene = nextSceneEnum;

            SceneManager.LoadScene(ScenesEnumToInt(Scenes.Loading));

            if ((Scenes.Floor1 <= nextSceneEnum) && (nextSceneEnum <= Scenes.Floor5))
            {
                Debug.Log("Compare Level");
                if (clearLevel <= ScenesEnumToInt(nextScene) - 1)
                {
                    clearLevel = ScenesEnumToInt(nextScene) - 1;
                    Debug.Log("Set Level");
                }
            }
            StartCoroutine(LoadScene());
        }
        private IEnumerator LoadScene()
        {

            var op = SceneManager.LoadSceneAsync(ScenesEnumToInt(nextScene));
            op.allowSceneActivation = false;

            while (!op.isDone)
            {
                yield return null;

                if (op.progress == 1.0f)
                {
                    currentScene = nextScene;
                    op.allowSceneActivation = true;
                    yield break;
                }

            }
        }

        public static bool IsPassedLevel()
        {
            return clearLevel > ScenesEnumToInt(currentScene) - 1;
        }

        public static int ScenesEnumToInt(Scenes scene)
        {
            return scene switch
            {
                Scenes.Openning => 0,
                Scenes.Loading => 1,
                Scenes.Floor1 => 2,
                Scenes.Floor2 => 3,
                Scenes.Floor3 => 4,
                Scenes.Floor4 => 5,
                Scenes.Floor5 => 6,
                Scenes.Credit => 7,
                _ => -1
            };
        }
        public static Scenes IntToScenesEnum(int scene)
        {
            return scene switch
            {
                0 => Scenes.Openning,
                1 => Scenes.Loading,
                2 => Scenes.Floor1,
                3 => Scenes.Floor2,
                4 => Scenes.Floor3,
                5 => Scenes.Floor4,
                6 => Scenes.Floor5,
                7 => Scenes.Credit,
                _ => throw new System.NotImplementedException()
            };
        }
    }
}