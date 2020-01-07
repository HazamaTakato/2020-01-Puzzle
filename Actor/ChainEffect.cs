using _2020_01_Puzzle.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_01_Puzzle.Actor
{
    class ChainEffect
    {
        private int[,] colorTable;//ブロックの色
        private bool[,] effectXTable;//横
        private bool[,] effectYTable;//縦
        private bool[,] effectXYTable;
        private float effectTimer;
        private int gamePoint;// 揃えた得点
        public ChainEffect(int[,] colorTable, bool[,] effectXTable, bool[,] effectYTable, bool[,] effectXYTable)
        {
            this.colorTable = colorTable;
            this.effectXTable = effectXTable;
            this.effectYTable = effectYTable;
            this.effectXYTable = effectXYTable;
        }
        public void Initialize()
        {
            effectTimer = 0;
        }
        public bool Update()
        {
            effectTimer += 0.04f;
            // 一定時間経過でエフェクト中終了
            if (effectTimer >= 1)
            {
                // ブロック配列のクリア
                // （縦横で２重ループ）
                for (int y = 0; y < Block.YMax; y++)
                {
                    for (int x = 0; x < Block.XMax; x++)
                    {
                        // エフェクト中ならば
                        if (effectXTable[y, x] == true || effectYTable[y, x] == true || effectXYTable[y, x] == true)
                        {
                            // ブロックとエフェクトを消す
                            colorTable[y, x] = 0;
                            effectXTable[y, x] = false;
                            effectYTable[y, x] = false;
                            effectXYTable[y, x] = false;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Draw(Renderer renderer)
        {
            // 得点によりエフェクトの数を決定
            int number = 1;
            if (gamePoint >= 20) number = 3;
            if (gamePoint >= 50) number = 5;
            if (gamePoint >= 100) number = 10;

            // 半透明の強さを決定
            float effectColor = 1 - effectTimer;

            for (int y = 0; y < Block.YMax; y++)
            {
                for (int x = 0; x < Block.XMax; x++)
                {
                    if (effectXTable[y, x] == true || effectYTable[y, x] == true || effectXYTable[y, x] == true)
                    {
                        Vector2 position = new Vector2(Block.Size * x, Block.Size * y);
                        position += new Vector2(Block.StartX, Block.StartY);
                        Rectangle rect = new Rectangle(Block.Size * (colorTable[y, x] - 1), 0, Block.Size, Block.Size);

                        //for (int i = 0; i < number; i++)
                        //{
                        //    // 表示座標の補正を計算
                        //    float effectMove = effectTimer * 150;
                        //    effectMove += (i + 1) * effectTimer * 50;

                        //    renderer.DrawTexture("block", position + new Vector2(0, -effectMove),
                        //            rect, effectColor);
                        //    renderer.DrawTexture("block", position + new Vector2(0, effectMove),
                        //           rect, effectColor);



                        //    renderer.DrawTexture("block", position + new Vector2(-effectMove, 0),
                        //           rect, effectColor);
                        //    renderer.DrawTexture("block", position + new Vector2(effectMove, 0),
                        //           rect, effectColor);
                        //}
                    }
                }
            }
        }
    }
}
