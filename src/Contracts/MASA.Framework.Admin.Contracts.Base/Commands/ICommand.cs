namespace MASA.Framework.Admin.Contracts.Base.Commands;

public interface ICommand : MASA.BuildingBlocks.ReadWriteSpliting.CQRS.Commands.ICommand, ITransaction
{
}
