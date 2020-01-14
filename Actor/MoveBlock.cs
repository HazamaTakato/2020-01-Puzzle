using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using _2020_01_Puzzle.Device;

namespace _2020_01_Puzzle.Actor
{
    class MoveBlock
    {
        Vector2 position;
        Vector2 position2;
        private bool isPressRightKey;
        private bool isPressLeftKey;
        bool aliveFlag;
        bool underFlag;
        Random rand;
        int color;
        StopBlock stopBlock;
        FutureBlock futureBlock;
        Vector2 tablePosition;
        float fallspeed;
        private int timer;
        public MoveBlock(StopBlock stopBlock)
        {
            this.stopBlock = stopBlock;
            futureBlock = new FutureBlock();
            position = new Vector2(0, 0);
            position2 = new Vector2(0, 0);
            rand = new Random();
            tablePosition = new Vector2();
        }
        public void Initialize()
        {
            isPressRightKey = false;
            isPressLeftKey = false;
            aliveFlag = false;
            underFlag = false;
            fallspeed=2;
            timer = 0;
        }
        public void Update()
        {

            if (aliveFlag)
            {
                MoveRightLeft();
                MoveDown();
                AliveCheck();
            }
            else
            {
                timer++;
                if (timer >= 60)
                {
                    if (stopBlock.GetChainMode() == Chain.Normal)
                    {
                        SetBlock();
                    }
                }
            }
        }
        public void Draw(Renderer renderer)
        {
            if (aliveFlag == true)
            {
                Rectangle rect = new Rectangle(Block.Size * (color-1), 0, Block.Size, Block.Size);
                renderer.DrawTexture("block", position + new Vector2(Block.StartX, Block.StartY), rect);
                //renderer.DrawTexture("block", position2 + new Vector2(Block.StartX, Block.StartY), rect);
            }
            futureBlock.Draw(renderer);
        }
        private void MoveDown()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += fallspeed;
                //position2.Y += fallspeed;
            }
            else
            {
                position.Y += fallspeed;
                //position2.Y += fallspeed;
            }
        }
        private void MoveRightLeft()
        {
            //対応する停止ブロック上の位置を計算
            SetTablePosition();
            // 右へ移動
            // 右キー、パッド右方向を押したら
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // 前回が押してなければ
                if (isPressRightKey == false)
                {
                    //右側が空いてあれば
                    if (stopBlock.GetBlockColor(tablePosition + new Vector2(1, 0)) == 0 &&
                        stopBlock.GetBlockColor(tablePosition + new Vector2(1, 1)) == 0)
                    {
                        position.X = position.X + Block.Size;   // ブロックのサイズだけ右へ移動
                        //position2.X += Block.Size;
                        isPressRightKey = true;// 「押した」に設定
                    }
                }
            }
            else　// 押してなければ
            {
                isPressRightKey = false;      // 「押してない」に設定
            }


            // 左へ移動
            // 左キー、パッド左方向を押したら
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                // 前回が押してなければ
                if (isPressLeftKey == false)
                {
                    // 左側が空いてあれば
                    if (stopBlock.GetBlockColor(tablePosition + new Vector2(-1, 0)) == 0 &&
                        stopBlock.GetBlockColor(tablePosition + new Vector2(-1, 1)) == 0)
                    {
                        position.X = position.X - Block.Size;       // ブロックのサイズだけ左へ移動
                        //position2.X -= Block.Size;
                        isPressLeftKey = true;// 「押した」に設定
                    }
                }
            }
            else　// 押してなければ
            {
                isPressLeftKey = false;// 「押してない」に設定
            }
        }
        private void AliveCheck()
        {
            //対応する停止ブロック上の位置を計算
            SetTablePosition();
            // 画面の下に到着
            if (position.Y >= Block.Size * (Block.YMax - 1)||position2.Y>=Block.Size*(Block.YMax-1)|| stopBlock.GetBlockColor(tablePosition + new Vector2(0, 1)) != 0&&!underFlag)
            {
                // 存在しない
                aliveFlag = false;
                //停止ブロックの発生
                stopBlock.SetBlock(tablePosition, color);
                underFlag = true;
                stopBlock.UnderMoveBlock(tablePosition, color);
                if (underFlag)
                {
                    //MoveRightLeft();
                    //MoveDown();
                    underFlag = false;
                }
            }
        }
        private void SetBlock()
        {
            // 存在する
            aliveFlag = true;

            // 座標の設定
            position.X = rand.Next(Block.XMax) * Block.Size;
            //position2.X = position.X + Block.Size;
            position.Y = 0;

            //色設定
            futureBlock.NextCreate();
            color = futureBlock.Color()+1;
        }
        private void SetTablePosition()
        {
            //表示座標をブロックサイズで割れば
            //対応する配列の位置が算出できる
            tablePosition = position / Block.Size;
            //tablePosition = position2 / Block.Size;
        }
    }
}
