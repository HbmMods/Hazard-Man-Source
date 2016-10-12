namespace HazardMan
{
    class EntityBullet : EntityProjectile
    {
        public EntityBullet(float posX, float posY, float motionX, float motionY)
        {
            setX(posX);
            setY(posY);

            this.motionX = motionX;
            this.motionY = motionY;
        }

        public override bool executeAICheck()
        {
            lock(World.entities)
            {
                foreach (Entity entity in World.entities)
                {
                    if (entity is EntityPlayer)
                    {
                        if (entity.getX() < getX() + 1 && entity.getX() > getX() - 1 && entity.getY() < getY() + 1 && entity.getY() > getY() - 1)
                        {
                            entity.damage(1);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override void executeAITask()
        {
            setDead();
        }

        public override void Update()
        {
            motionY = 0F;
            motionX *= 2F;

            if (motionX < 0.1F && motionX > -0.1F)
                setDead();
            
            if (renderer == null)
                renderer = new RenderBullet();
            
            base.Update();
        }
    }
}
