using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3902.Interfaces
{
    public interface ISword : ICollidable
    {
        public int DamageAmount { get; set; }
        public void Draw();
        public void Update();
    }
}
