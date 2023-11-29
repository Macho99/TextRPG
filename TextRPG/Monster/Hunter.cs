using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Hunter : Monster
    {
        char icon = 'H';
        public Hunter(Position pos) : base(pos, "헌터", 10, 0, 5, 20, 15)
        {

        }

        public override char GetIcon()
        {
            return icon;
        }

        public override void MoveAction(int[,] map)
        {
            throw new NotImplementedException();
        }
    }
}
