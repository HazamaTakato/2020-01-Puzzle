using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_01_Puzzle.Actor
{
    class ChainStart
    {
        private int[,] colorTable;//ブロックの色
        private bool[,] effectXTable;//横エフェクト
        private bool[,] effectYTable;//縦
        private bool[,] effectXYTable;//斜め
        int gamePoint;
        int[] numberPoint = new int[] { 0 };//揃った数対応点
        int chainNumber;//連鎖数
        public ChainStart(int[,] colorTable,bool[,] effectXTable,bool[,] effectYTable, bool[,] effectXYTable)
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
            //横のチェック
            for(int y = 0; y < Block.YMax; y++)
            {
                for(int x=0;x<Block.XMax; x++)
                {
                    if (colorTable[y, x] != 0)
                    {
                        LineCheakX(x, y);
                    }
                }
            }
            //縦チェック
            for(int y = 0; y < Block.YMax; y++)
            {
                for(int x = 0; x < Block.XMax; x++)
                {
                    if (colorTable[y, x] != 0)
                    {
                        LineCheakY(x, y);
                    }
                }
            }
            //斜めチェック
            for(int y = 0; y < Block.YMax; y++)
            {
                for(int x = 0; x < Block.XMax; x++)
                {
                    if (colorTable[y, x] != 0)
                    {
                        LineCheakXY(x, y);
                    }
                }
            }
            if (gamePoint > 0)
            {
                chainNumber++;
            }
            else
            {
                chainNumber = 1;
            }
            return gamePoint;
        }
        private void LineCheakX(int x,int y)
        {
            if (effectXTable[y, x] == true)
            {
                return;
            }

            int total = 1;

            while (true)
            {
                int x2 = x + total;
                if (x2 >= Block.XMax)
                {
                    break;
                }
                else
                {
                    if (colorTable[y, x] == colorTable[y, x2])
                    {
                        total++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (total >= 3)
            {
                for(int i = 0; i < total; i++)
                {
                    effectXTable[y, x + i] = true;
                }
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
            if (total >= 3)
            {
                for (int i = 0; i < total; i++)
                {
                    effectYTable[y + i, x] = true;//エフェクトYを登録
                }
                //スコア加算
                gamePoint += numberPoint[total] * chainNumber;
            }
        }
        private void LineCheakXY(int x, int y)
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
                int x2 = x + total;
                if (y2 >= Block.YMax||x2>=Block.XMax)
                {
                    break;
                }
                else
                {
                    if (colorTable[y, x] == colorTable[y2, x2])
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
                    effectYTable[y + i, x + i] = true;//エフェクトYを登録
                }
                //スコア加算
                gamePoint += numberPoint[total] * chainNumber;
            }
        }
    }
}
