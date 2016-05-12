using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntitySmokeFX : Entity
    {
        int age = 0;
        int maxAge = 20;

        public override void Update()
        {
            motionY = 0F;
            motionX = 0F;

            if(age < maxAge)
            {
                age++;
            }
            else
            {
                setDead();
            }

            base.Update();
        }
    }
}
