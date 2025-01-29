using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Configuraciones
{
    public class EspecialidadConfiguracion :IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NombreEspecialidad).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Descripcion).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Estado).IsRequired();
        }
    }
}
