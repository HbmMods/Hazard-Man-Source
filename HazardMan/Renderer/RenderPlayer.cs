namespace HazardMan
{
    class RenderPlayer : RenderEntity
    {
        public RenderPlayer(EntityPlayer player)
        {
            setColor(player.getColor());
            setRenderIcon('☻');
        }
    }
}
