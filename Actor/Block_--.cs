using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2020_01_Puzzle.Device;
using Microsoft.Xna.Framework;

namespace _2020_01_Puzzle.Actor
{
    class Block___ : BlockManager
    {
        int color;
        public Block___()
        {
            form = new Vector2[]
            {
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(1,0),
            };
            color = colors[rnd.Next(colors.Length)];
        }

        public override void Draw(Renderer renderer)
        {
            for (int i = 0; i < form.Length; i++)
            {
                Rectangle rect = new Rectangle(Block.Size * color, 0, Block.Size, Block.Size);
                renderer.DrawTexture("block", Block.Size * (new Vector2(form[i].X, form[i].Y)), rect);
            }
        }
    }
}
