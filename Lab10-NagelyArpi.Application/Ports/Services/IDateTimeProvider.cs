namespace Lab10_NagelyArpi.Application.Ports.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
