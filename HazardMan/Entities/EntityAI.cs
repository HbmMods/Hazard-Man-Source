namespace HazardMan
{
    abstract class EntityAI : Entity
    {
        public abstract bool executeAICheck();

        public abstract void executeAITask();
    }
}
