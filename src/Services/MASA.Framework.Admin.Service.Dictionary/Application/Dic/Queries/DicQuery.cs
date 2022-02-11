using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;

namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic.Queries
{
    public record DicQuery(Guid id) : Query<Domain.Entities.Dic>
    {
        public override Domain.Entities.Dic Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
