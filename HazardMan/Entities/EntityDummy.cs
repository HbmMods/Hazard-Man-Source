namespace HazardMan
{
    class EntityDummy : Entity
    {
        public EntityDummy(float x, float y)
        {
            setX(x);
            setY(y);
        }

        //This is a test

        public override void Update()
        {
            if (renderer == null)
            {
                renderer = new RenderDummy();
            }

            base.Update();       
        }
    }
}
