using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Core
    {
        private static Core instance;
        private bool running;

        Scene curScene;
        Dictionary<GroupScene, Scene> sceneDict;

        private Core() { }
        public static Core Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Core();
                }
                return instance;
            }
        }
        public void Run()
        {
            Init();
            while (running)
            {
                Render();
                Update();
            }
            Release();
        }
        private void Init()
        {
            running = true;

            sceneDict = new Dictionary<GroupScene, Scene>
            {
                { GroupScene.Title, new SceneTitle() },
                { GroupScene.Map, new SceneMap() },
                { GroupScene.Battle, new SceneBattle() },
                { GroupScene.GameOver, new SceneGameOver() },
            };

            foreach(Scene scene in sceneDict.Values)
            {
                scene.Init();
            }

            sceneDict[GroupScene.Prev] = null;

            curScene = sceneDict[GroupScene.Title];
            curScene.Enter();
        }
        private void Release()
        {
            foreach(Scene scene in sceneDict.Values)
            {
                scene.Release();
            }
            sceneDict.Clear();
            curScene = null;
        }
        private void Update() {
            curScene.Update();
        }
        private void Render()
        {
            curScene.Render();
        }
        public void SceneChange(GroupScene scene)
        {
            curScene.Exit();
            Scene tmp = curScene;
            curScene = sceneDict[scene];
            sceneDict[GroupScene.Prev] = tmp;
            curScene.Enter();
        }
        public void GameOver()
        {
            running = false;
        }
    }
}
