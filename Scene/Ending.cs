using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using _2020_01_Puzzle.Device;
using Microsoft.Xna.Framework.Input;

namespace _2020_01_Puzzle.Scene
{
    class Ending : IScene
    {
        private bool isEndFlag;
        private Sound sound;
        IScene backGroundScene;

        public Ending()
        {
        }

        public Ending(IScene scene)
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            backGroundScene = scene;

        }
        public void Draw(Renderer renderer)
        {
            backGroundScene.Draw(renderer);
            renderer.Begin();
            renderer.End();
        }
        public void Initialize()
        {
            isEndFlag = false;
        }
        public bool IsEnd()
        {
            return isEndFlag;
        }
        public Scene Next()
        {
            return Scene.Title;
        }
        public void Shutdown()
        {
            sound.StopBGM();
        }
        public void Update(GameTime gameTime)
        {
            //sound.PlayBGM("endingbgm");
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                //sound.PlaySE("endingse");


            }
        }
    }
}