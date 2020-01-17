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
        Vector2 tablePosition;
        float fallspeed;
        private int timer;
        FutureBlock future;
        Vector2[] blockForm = new Vector2[3];
        Vector2[] nextBlockForm = new Vector2[3];
        Vector2[] tablePositions = new Vector2[3];

        public MoveBlock(StopBlock stopBlock)
        {
            this.stopBlock = stopBlock;
            future = new FutureBlock();
            position = new Vector2(0, 0);
            position2 = new Vector2(0, 0);
            rand = new Random();
            tablePosition = new Vector2();
            for (int i = 0; i < future.GetForm(0).Length; i++)
            {
                blockForm[i] = future.GetForm(0)[i];
                blockForm[i] *= Block.Size;
            }
            

        }
        public void Initialize()
        {
            isPressRightKey = false;
            isPressLeftKey = false;
            aliveFlag = false;
            underFlag = false;
            fallspeed = 2;
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
                //Rectangle rect = new Rectangle(Block.Size * (color - 1), 0, Block.Size, Block.Size);
                //renderer.DrawTexture("block", position + new Vector2(Block.StartX, Block.StartY), rect);
                ////renderer.DrawTexture("block", position2 + new Vector2(Block.StartX, Block.StartY), rect);
                for (int i = 0; i < future.GetForm(0).Length; i++)
                {
                    Rectangle rect = new Rectangle(Block.Size * (color - 1), 0, Block.Size, Block.Size);
                    renderer.DrawTexture("block", blockForm[i] + new Vector2(Block.StartX, Block.StartY), rect);
                    //renderer.DrawTexture("block", position2 + new Vector2(Block.StartX, Block.StartY), rect);
                }
            }
            future.Draw(renderer);
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
                //position.Y += fallspeed;
                //position2.Y += fallspeed;
                for (int i = 0; i < future.GetForm(0).Length; i++)
                {
                    blockForm[i].Y += fallspeed;
                }
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
                //if (isPressRightKey == false)
                //{
                //    ////右側が空いてあれば
                //    if (stopBlock.GetBlockColor(tablePosition + new Vector2(1, 0)) == 0 &&
                //        stopBlock.GetBlockColor(tablePosition + new Vector2(1, 1)) == 0)
                //    {
                //        position.X = position.X + Block.Size;   // ブロックのサイズだけ右へ移動
                //        //position2.X += Block.Size;
                //        isPressRightKey = true;// 「押した」に設定
                //    }
                //}
                if (isPressRightKey == false)
                {
                    Vector2 right = tablePositions[0];
                    for (int i = 1; i < future.GetForm(0).Length; i++)
                    {
                        if (right.X < tablePositions[i].X)
                            right = tablePositions[i];
                    }
                    ////右側が空いてあれば
                    if (stopBlock.GetBlockColor(right + new Vector2(1, 0)) == 0 &&
                        stopBlock.GetBlockColor(right + new Vector2(1, 1)) == 0)
                    {
                        for (int i = 0; i < future.GetForm(0).Length; i++)
                        {
                            blockForm[i].X = blockForm[i].X + Block.Size;   // ブロックのサイズだけ右へ移動
                        }                                            //position2.X += Block.Size;
                    }
                    isPressRightKey = true;// 「押した」に設定
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
                //// 前回が押してなければ
                //if (isPressLeftKey == false)
                //{
                //    // 左側が空いてあれば
                //    if (stopBlock.GetBlockColor(tablePosition + new Vector2(-1, 0)) == 0 &&
                //        stopBlock.GetBlockColor(tablePosition + new Vector2(-1, 1)) == 0)
                //    {
                //        position.X = position.X - Block.Size;       // ブロックのサイズだけ左へ移動
                //        //position2.X -= Block.Size;
                //        isPressLeftKey = true;// 「押した」に設定
                //    }
                //}
                // 前回が押してなければ
                if (isPressLeftKey == false)
                {
                    Vector2 left = tablePositions[0];

                    for (int i = 1; i < future.GetForm(0).Length; i++)
                    {
                        if (left.X > tablePositions[i].X)
                            left = tablePositions[i];
                    }
                    ////←側が空いてあれば
                    if (stopBlock.GetBlockColor(left + new Vector2(-1, 0)) == 0 &&
                        stopBlock.GetBlockColor(left + new Vector2(-1, 1)) == 0)
                    {
                        for (int i = 0; i < future.GetForm(0).Length; i++)
                        {
                            blockForm[i].X = blockForm[i].X - Block.Size;   // ブロックのサイズだけ右へ移動
                        }                                            //position2.X += Block.Size;
                    }
                    isPressLeftKey = true;// 「押した」に設定
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

            //if (position.Y >= Block.Size * (Block.YMax - 1) || position2.Y >= Block.Size * (Block.YMax - 1) || stopBlock.GetBlockColor(tablePosition + new Vector2(0, 1)) != 0)
            //{
            //    //存在しない
            //   aliveFlag = false;
            //    //停止ブロックの発生
            //    stopBlock.SetBlock(tablePosition, color);

            //    underFlag = true;
            //    stopBlock.UnderMoveBlock(tablePosition, color);
            //}
            for (int i = 0; i < future.GetForm(0).Length; i++)
            {
                if (blockForm[0].Y >= Block.Size * (Block.YMax - 1) || position2.Y >= Block.Size * (Block.YMax - 1) || stopBlock.GetBlockColor(tablePositions[i] + new Vector2(0, 1)) != 0)
                {
                    aliveFlag = false;

                    //停止ブロックの発生
                    for (int j = 0; j < future.GetForm(0).Length; j++)
                    {
                        // 存在しない
                        stopBlock.SetBlock(tablePositions[j], color);

                    }

                    underFlag = true;
                    //stopBlock.UnderMoveBlock(tablePosition, color);
                    break;
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
            int rnd = rand.Next(Block.XMax);

            for (int i = 0; i < future.GetForm(1).Length; i++)
            {
                blockForm[i] = future.GetForm(1)[i];
                blockForm[i] *= Block.Size;
            }

            //色設定
            //color = rand.Next(5) + 1;
            future.NextCreate();
            color = future.Color() + 1;
        }
        private void SetTablePosition()
        {
            //表示座標をブロックサイズで割れば
            //対応する配列の位置が算出できる
            //tablePosition = position / Block.Size;

            //tablePosition = position2 / Block.Size;

            for (int i = 0; i < future.GetForm(0).Length; i++)
            {
                tablePositions[i] = blockForm[i] / Block.Size;
            }
        }
    }
}
