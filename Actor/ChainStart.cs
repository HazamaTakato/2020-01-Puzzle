using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_01_Puzzle.Actor
{
    class ChainStart
    {
        private int[,] colorTable;
        private bool[,] effectXTable;   // 横が揃ったエフェクト
        private bool[,] effectYTable;   // 縦が揃ったエフェクト
        private bool[,] effectXYTable;
        int gamePoint;
        int[] numberPoint = new int[] { 0, 0, 0, 10, 30, 100, 300, 400, 400, 400,400,400,400,400,400,400,400,400,400 };//揃った数対応点
        int[][] num = new int[][]
        {
           new int[]{-1,0 },
           new int[]{0,1 },
           new int[]{1,0 },
           new int[]{0,-1 },
        };
        
        int point;
        int chainNumber;// 連鎖数
        public ChainStart(int[,] colorTable, bool[,] effectXTable, bool[,] effectYTable, bool[,] effectXYTable)
        {
            this.colorTable = colorTable;
            this.effectXTable = effectXTable;
            this.effectYTable = effectYTable;
            this.effectXYTable = effectXYTable;
        }
        public void Initialize()
        {
            chainNumber = 1;
        }
        public int Update()
        {
            gamePoint = 0;
            // 横に揃ったかのチェック
            // （縦横で２重ループ）
            //for (int y = 0; y < Block.YMax; y++)
            //{
            //    for (int x = 0; x < Block.XMax; x++)
            //    {
            //        // ブロックがあれば
            //        if (colorTable[y, x] != 0)
            //        {
            //            // 横方向のチェック
            //            LineCheakX(x, y);
            //        }
            //    }
            //}
            //// 縦に揃ったかのチェック
            //// （縦横で２重ループ）
            //for (int y = 0; y < Block.YMax; y++)
            //{
            //    for (int x = 0; x < Block.XMax; x++)
            //    {
            //        // ブロックがあれば
            //        if (colorTable[y, x] != 0)
            //        {
            //            // 縦方向のチェック
            //            LineCheakY(x, y);
            //        }
            //    }
            //}
            for (int y = 0; y < Block.YMax; y++)
            {
                for (int x = 0; x < Block.XMax; x++)
                {
                    // ブロックがあれば
                    if (colorTable[y, x] != 0)
                    {
                        // 横方向のチェック
                        //LineCheakXY(x, y);
                        point = 0;
                        DirectionXY(y, x);
                        if(point >= 4)
                        {
                            Delete(y, x);
                            gamePoint += numberPoint[point] * chainNumber;
                        }
                    }
                }
            }
            //揃ったブロックがあり、得点が有れば
            if (gamePoint > 0)
            {
                chainNumber++;// 連鎖数をアップ
            }
            // 揃ったブロックが無く、得点が無ければ
            else
            {
                chainNumber = 1;// 連鎖の初期化
            }
            return gamePoint;
        }
        private void LineCheakXY(int x, int y)
        {
            // 既に揃っていれば終了
            if (effectXYTable[y, x] == true)
            {
                return;
            }

            //同じ色が右方向にいくつあるかを調べる
            //同じ色は１個で初期化
            int total = 1;
            int totalX = 1;
            //永久ループ
            while (true)
            {
                //チェックする位置を計算
                int x2 = x + totalX;
                int y2 = y + total;
                //範囲外なら
                if (x >= Block.XMax || y >= Block.YMax)
                {
                    break;
                }
                //範囲内ならば
                else
                {
                    // 同じ色ならば
                    if (colorTable[y, x] == colorTable[y2, x])
                    {
                        total++;    // １を足す
                    }
                    else if (colorTable[y, x] == colorTable[y, x2])
                    {
                        totalX++;
                    }
                    else
                    {
                        break;
                    }
                }


            }
            // 同じ色が３個以上あれば
            if (total + totalX - 1 >= 4)
            {
                // 対応する位置を処理
                for (int i = 0; i < total; i++)
                {
                    for (int j = 0; j < totalX; j++)
                    {
                        effectXYTable[y + i, x + j] = true;//エフェクトXを登録
                    }
                }
                //スコア加算
                gamePoint += numberPoint[total] * chainNumber;
            }
        }
        private void LineCheakX(int x, int y)
        {
            // 既に揃っていれば終了
            if (effectXTable[y, x] == true)
            {
                return;
            }

            //同じ色が右方向にいくつあるかを調べる
            //同じ色は１個で初期化
            int total = 1;
            //永久ループ
            while (true)
            {
                //チェックする位置を計算
                int x2 = x + total;
                //範囲外なら
                if (x2 >= Block.XMax)
                {
                    break;
                }
                //範囲内ならば
                else
                {
                    // 同じ色ならば
                    if (colorTable[y, x] == colorTable[y, x2])
                    {
                        total++;    // １を足す
                    }
                    // 違う色ならば
                    else
                    {
                        break;// ループ終了
                    }
                }
            }
            // 同じ色が３個以上あれば
            if (total >= 4)
            {
                // 対応する位置を処理
                for (int i = 0; i < total; i++)
                {
                    effectXTable[y, x + i] = true;//エフェクトXを登録
                }
                //スコア加算
                gamePoint += numberPoint[total] * chainNumber;
            }
        }
        private void LineCheakY(int x, int y)
        {
            // 既に揃っていれば終了
            if (effectYTable[y, x] == true)
            {
                return;
            }

            int total = 1;

            while (true)
            {
                int y2 = y + total;

                if (y2 >= Block.YMax)
                {
                    break;
                }
                else
                {
                    if (colorTable[y, x] == colorTable[y2, x])
                    {
                        total++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (total >= 4)
            {
                for (int i = 0; i < total; i++)
                {
                    effectYTable[y + i, x] = true;//エフェクトYを登録
                }
                //スコア加算
                gamePoint += numberPoint[total] * chainNumber;
            }
        }
        private void DirectionXY(int y, int x)
        {
            var hold = colorTable[y, x];
            colorTable[y,x] = 0;
            point++;

            if (x + 1 < Block.XMax && colorTable[y, x + 1] == hold) DirectionXY(y, x + 1);
            if (y + 1 < Block.YMax && colorTable[y + 1, x] == hold) DirectionXY(y + 1, x);
            if (x - 1 >= 0 && colorTable[y, x - 1] == hold) DirectionXY(y, x - 1);
            if (y - 1 >= 0 && colorTable[y - 1,x] == hold) DirectionXY(y - 1, x);

            colorTable[y, x] = hold;

        }
        private void Delete(int y,int x)
        {
            effectXYTable[y, x] = true;
            int c = colorTable[y, x];
            colorTable[y, x] = 0;

            if (x + 1 < Block.XMax && colorTable[y, x + 1] == c) Delete(y, x + 1);
            if (y + 1 < Block.YMax && colorTable[y + 1, x] == c) Delete(y + 1, x);
            if (x - 1 >= 0 && colorTable[y, x - 1] ==c) Delete(y, x - 1);
            if (y - 1 >= 0 && colorTable[y - 1, x] == c) Delete(y - 1, x);
        }
    }
}
