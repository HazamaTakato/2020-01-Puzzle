

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using _2020_01_Puzzle.Device;

namespace _2020_01_Puzzle.Scene
{
    class SceneManager
    {
        private Dictionary<Scene, IScene> scenes = new Dictionary<Scene, IScene>();
        private IScene currentScene = null;


        public SceneManager()
        { }
        public void Add(Scene name, IScene scene)
        {
            if (scenes.ContainsKey(name))
            {
                return;
            }
            scenes.Add(name, scene);

        }
        public void Change(Scene name)
        {
            if (currentScene != null)
            {
                currentScene.Shutdown();
            }
            currentScene = scenes[name];
            currentScene.Initialize();

        }
        public void Update(GameTime gametime)
        {
            if (currentScene == null)
            {
                return;
            }
            currentScene.Update(gametime);
            if (currentScene.IsEnd())
            {
                Change(currentScene.Next());
            }
        }
        public void Draw(Renderer renderer)
        {
            if (currentScene == null)
            {
                return;
            }
            currentScene.Draw(renderer);
        }
    }
}