using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class EventManager
    {
        private Queue<GroupScene> queueScene;
        private static EventManager instance;
        public static EventManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new EventManager();
                }
                return instance;
            }
        }
        private EventManager() { }
        public void Init()
        {
            queueScene = new Queue<GroupScene>();
        }
        public void Release()
        {
            queueScene.Clear();
        }
        public void Update()
        {
            // scene 변경
            while(queueScene.Count > 0)
            {
                GroupScene scene = queueScene.Dequeue();
                Core.Instance.SceneChange(scene);
            }
        }
        public void ReserveChangeScene(GroupScene scene)
        {
            queueScene.Enqueue(scene);
        }
    }
}
