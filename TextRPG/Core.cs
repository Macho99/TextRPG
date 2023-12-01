using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TextRPG
{
    public class Core
    {
        private static Core instance;
        private bool running;

        Scene curScene;
        Dictionary<GroupScene, Scene> sceneDict;
        Stack<Scene> sceneStack;

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
                EventManager.Instance.Update();
                Render();
                Update();
            }
            Release();
        }
        private void Init()
        {
            running = true;

            EventManager.Instance.Init();

            // TODO: 씬 추가
            sceneStack = new Stack<Scene>();
            sceneDict = new Dictionary<GroupScene, Scene>
            {
                { GroupScene.Title, new SceneTitle() },
                { GroupScene.Map1, new SceneMap1() },
                { GroupScene.Map2, new SceneMap2() },
                { GroupScene.Battle, new SceneBattle() },
                { GroupScene.Inventory, new SceneInventory() },
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
            EventManager.Instance.Release();
            foreach(Scene scene in sceneDict.Values)
            {
                if(scene != null)
                {
                    scene.Release();
                }
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
            if(scene == GroupScene.Battle || scene == GroupScene.Inventory)
            {
                sceneStack.Push(curScene);
            }
            curScene.Exit();
            if(scene == GroupScene.Prev)
            {
                if(sceneStack.Count < 0) {
                    throw new Exception();
                }
                curScene = sceneStack.Pop();
            }
            else
            {
                curScene = sceneDict[scene];
            }
            curScene.Enter();
        }
        public Scene GetCurScene()
        {
            return curScene;
        }
        public void GameOver()
        {
            running = false;
        }
    }
}
