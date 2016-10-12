namespace HazardMan
{
    abstract class EntityProjectile : EntityAI
    {
        public override bool executeAICheck() { return false; }

        public override void executeAITask() { }
    }
}
