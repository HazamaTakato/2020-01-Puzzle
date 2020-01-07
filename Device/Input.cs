

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2020_01_Puzzle.Device
{
    sealed class Input
    {
        //移動量
        private static Vector2 velocity = Vector2.Zero;
        private static Vector2 padVelocity = Vector2.Zero;    //ゲームパッド用移動量
        //キーボード
        private static KeyboardState currentKey;//現在のキーの状態
        private static KeyboardState previousKey;//1フレーム前のキーの状態
        //マウス
        private static MouseState currentMouse;//現在のマウスの状態
        private static MouseState previousMouse;//1フレーム前のマウスの状態


        /// <summary>
        /// 更新
        /// </summary>
        public static void Update()
        {
            //キーボード
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            //マウス
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

        }
        #region　キーボード関連

        //キーボード関連
        public static Vector2 Velocity()
        {
            return velocity;
        }
        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>現在キーが押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IsKeyDown(Keys key)
        {
            return currentKey.IsKeyDown(key) && !previousKey.IsKeyDown(key);
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>押された瞬間ならtrue</returns>
        public static bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        /// <summary>
        /// キーが押されているか？
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns>キーが押されていたらtrue</returns>
        public static bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }
        #endregion   キーボード関連

    }
}