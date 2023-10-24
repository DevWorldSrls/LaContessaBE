using System.Reflection;

namespace DevWorld.LaContessa.Persistance
{
    public class LaContessaDbContextOptions
    {
        public string? DatabaseName { get; set; }
        public string? ConnectionStringTemplate { get; set; }
        public int? CommandTimeout { get; set; }
        public Assembly MigrationsAssembly { get; set; } = null!;
    }

}
