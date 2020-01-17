using _2020_01_Puzzle.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_01_Puzzle.Actor
{
    abstract class BlockManager
    {
        public Vector2[] form;
        public int[] colors = new int[]
        {
            0,1,2,3,4
        };
        public Random rnd = new Random();

        public BlockManager()
        {
            //form = new Vector2[]
            //{
            //    new Vector2(0,0)
            //};
        }

        public abstract void Draw(Renderer renderer);
    }
}
