using Lab10_NagelyArpi.Application.Ports.Services;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Services;

public sealed class SystemDateTimeProvider : IDateTimeProvider
{
    // La BD del laboratorio usa timestamp without time zone. Se guarda el valor UTC sin Kind
    // para que Npgsql no rechace la escritura ni cambie la hora almacenada.
    public DateTime UtcNow => DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
}
