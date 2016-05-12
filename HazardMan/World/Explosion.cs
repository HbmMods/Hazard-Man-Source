using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class Explosion
    {
        public enum explosiontype { ExplosionGeneric };
        public explosiontype type;
        public int strength;
        public int x;
        public int y;

        public Explosion(int strength, explosiontype type, int x, int y)
        {
            this.strength = strength;
            this.type = type;
            this.x = x;
            this.y = y;
        }

        public void Damage(int strength, explosiontype type, int x, int y)
        {

        }

        public void Destroy(int radius, explosiontype type, int posX, int posY)
        {
            for (int y = -radius; y <= radius; y++)
                for (int x = -radius; x <= radius; x++)
                    if (x * x + y * y <= radius * radius)
                        World.changeTerrainElement(posX + x, posY + y, null);
        }

        public void Particle(int strength, explosiontype type, int x, int y)
        {

        }
    }
}
