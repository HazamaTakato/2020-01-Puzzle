using _2020_01_Puzzle.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2020_01_Puzzle.Actor
{
    class FutureBlock
    {
        private int[] colors = new int[]
        {
            0,1,2,3,4
        };
        private int[] futureBlock = new int[3];
        private GameDevice gameDevice;
        private Random rnd;
        private Vector2 pos;

        public FutureBlock()
        {
            pos = new Vector2(350, 100);
            //gameDevice = GameDevice.Instance();
            rnd = new Random();
            for(int i = 0;i < futureBlock.Length; i++)
            {
                futureBlock[i] = colors[rnd.Next(colors.Length)];

            }
        }

        public void Draw(Renderer renderer)
        {
            for(int i = 1;i < futureBlock.Length; i++)
            {
                Rectangle rect = new Rectangle(Block.Size * futureBlock[i], 0, Block.Size, Block.Size);
                renderer.DrawTexture("block", new Vector2(pos.X,pos.Y * i), rect);
            }
        }

        public int Color()
        {
            return futureBlock[0];
        }

        public void NextCreate()
        {
            for(int i = 0;i < futureBlock.Length - 1; i++)
            {
                futureBlock[i] = futureBlock[i + 1];
            }
            futureBlock[futureBlock.Length - 1] = colors[rnd.Next(colors.Length)];
        }
    }
}
