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
        private BlockManager[] blocks;

        private int[] futureBlock = new int[3];
        private BlockManager[] future = new BlockManager[3];
        private Random rnd;
        private Vector2 pos;

        public FutureBlock()
        {
            blocks = new BlockManager[]
            {
                new Block_L(),
                new Block_I(),
                new Block___(),
            };
            pos = new Vector2(350, 150);
            //gameDevice = GameDevice.Instance();
            rnd = new Random();
            //for (int i = 0; i < futureBlock.Length; i++)
            //{
            //    futureBlock[i] = colors[rnd.Next(colors.Length)];
            //}
            for (int i = 0; i < future.Length; i++)
            {
                future[i] = blocks[rnd.Next(blocks.Length)];
                futureBlock[i] = colors[rnd.Next(colors.Length)];
            }
        }

        public void Draw(Renderer renderer)
        {
            for (int i = 1; i < future.Length; i++)
            { 
               for(int j = 0;j < future[i].form.Length; j++)
                {
                    Rectangle rect = new Rectangle(Block.Size * futureBlock[i], 0, Block.Size, Block.Size);
                    renderer.DrawTexture("block", (Block.Size * future[i].form[j]) + new Vector2(pos.X, pos.Y * i), rect);

                }
            }
        }

        public int Color()
        {
            return futureBlock[0];
        }

        public void NextCreate()
        {
            for (int i = 0; i < future.Length - 1; i++)
            {
                future[i] = future[i + 1];
                futureBlock[i] = futureBlock[i + 1];
            }
            future[future.Length - 1] = blocks[rnd.Next(blocks.Length)];
            futureBlock[futureBlock.Length - 1] = colors[rnd.Next(colors.Length)];
        }
        public Vector2[] GetForm(int i)
        {
            return future[i].form; 
        }
    }
}
