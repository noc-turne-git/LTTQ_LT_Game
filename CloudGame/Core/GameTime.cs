using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudGame.Core
{       
    internal class GameTime
    {
        public TimeSpan DeltaTime { get; set; } = TimeSpan.Zero;
        public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
        public TimeSpan LastFrame { get; set; } = TimeSpan.Zero;
        public TimeSpan IntervalEnemySpawn { get; set;  } = TimeSpan.FromSeconds(1);


    }
}
