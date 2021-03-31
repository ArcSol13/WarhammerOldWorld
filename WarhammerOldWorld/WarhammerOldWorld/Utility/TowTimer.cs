using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace WarhammerOldWorld.Utility
{
    /// <summary>
    /// Wrapper for TaleWorlds' timer implementation.
    /// </summary>
    public class TowTimer
    {
        private Timer innerTimer;

        public TowTimer(float gameTime, float duration, bool autoReset = true)
        {
            innerTimer = new Timer(gameTime, duration, autoReset);
        }

        public bool Check(float gameTime)
        {
            return innerTimer.Check(gameTime);
        }

        public float ElapsedTime()
        {
            return innerTimer.ElapsedTime();
        }

        public void Reset(float gameTime)
        {
            innerTimer.Reset(gameTime);
        }

        public void Reset(float gameTime, float newDuration)
        {
            innerTimer.Reset(gameTime, newDuration);
        }

        public void AdjustStartTime(float deltaTime)
        {
            innerTimer.AdjustStartTime(deltaTime);
        }
    }
}
