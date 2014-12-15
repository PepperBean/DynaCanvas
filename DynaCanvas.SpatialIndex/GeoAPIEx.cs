using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoAPI.Geometries;

namespace DynaCanvas.SpatialIndex
{
    internal static class GeoAPIEx
    {
        public static double Margin(this Envelope env)
        {
            return (env.Height + env.Width) * 2;
        }

        public static double EnlargementSize(this Envelope dest, Envelope addon)
        {
            var newEnv = dest.ExpandedBy(addon);
            return Math.Max(0, newEnv.Area - dest.Area);
        }
        public static double OverlapSize(this Envelope env1, Envelope env2)
        {
            if (env1.Contains(env2))
            {
                return env2.Area;
            }
            if (env2.Contains(env1))
            {
                return env1.Area;
            }
            if (!env1.Intersects(env2))
            {
                return 0;
            }
            if (env1 == default(Envelope)
             || env2 == default(Envelope))
            {
                return 0;
            }

            if (Math.Min(env1.MaxX, env2.MaxX) < Math.Max(env1.MinX, env2.MinX))
            {
                return 0;
            }
            if (Math.Min(env1.MaxY, env2.MaxY) < Math.Max(env1.MinY, env2.MinY))
            {
                return 0;
            }

            return new Envelope(Math.Min(env1.MaxX, env2.MaxX)
                , Math.Min(env1.MaxY, env2.MaxY)
                , Math.Max(env1.MinX, env2.MinX),
                               Math.Max(env1.MinY, env2.MinY)).Area;
        }

    }
}
