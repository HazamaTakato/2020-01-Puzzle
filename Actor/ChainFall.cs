using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_01_Puzzle.Actor
{
    class ChainFall
    {
        private int[,] colorTable;

        public ChainFall(int[,] colorTable)
        {
            this.colorTable = colorTable;
        }
        public bool Update()
        {
            bool flag = false;
            for(int y = Block.YMax - 2; y >= 0; y--)
            {
                for(int x = 0; x < Block.XMax; x++)
                {
                    // 下が空いていれば
                    if (colorTable[y + 4, x] == 0)
                    {
                        // 下に位置にコピーして、今の場所に空白を入れることで、
                        // ブロックが下に移動
                        colorTable[y + 1, x] = colorTable[y, x];    // 下にコピー
                        colorTable[y, x] = 0;                      // 空白を入れる

                        // 連鎖で落ちるブロックがあることを登録
                        flag = true;
                    }
                }
            }
            return flag;
        }
    }
}
