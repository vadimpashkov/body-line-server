namespace Domain.Abstractions.Data
{
    public interface IUnitOfWorkCreator
    {
        IUnitOfWork CreateUnitOfWork();
    }
}