namespace HazardMan
{
    class EntitySmokeFX : Entity
    {
        int age;
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
