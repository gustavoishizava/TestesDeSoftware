using MediatR;
using NerdStore.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Data
{
    public static class MediatorExtension
    {
        public static async Task PublicarEventos(this IMediator mediator, VendasContext ctx)
        {
            var domainsEntities = ctx.ChangeTracker
                .Entries<EntityBase>().Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainsEvents = domainsEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainsEntities.ToList().ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainsEvents.Select(async (domainEvent) =>
            {
                await mediator.Publish(domainEvent);
            });

            await Task.WhenAll(tasks);
        }
    }
}
