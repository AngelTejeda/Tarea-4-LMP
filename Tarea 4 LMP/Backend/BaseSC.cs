using Tarea_4_LMP.Data_Access;

namespace Tarea_4_LMP.Backend
{
    class BaseSC
    {
        public LMPEntities dbContext = new LMPEntities();

        public void CloseConnection()
        {
            dbContext.Dispose();
        }
    }
}
