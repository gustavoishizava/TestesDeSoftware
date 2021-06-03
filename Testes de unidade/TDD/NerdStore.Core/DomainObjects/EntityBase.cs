using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;

namespace NerdStore.Core.DomainObjects
{
    public abstract class EntityBase
    {
        private readonly List<Event> _notificacoes = new List<Event>();
        public Guid Id { get; protected set; }
        public IReadOnlyList<Event> Notificacoes => _notificacoes.AsReadOnly();

        protected EntityBase()
        {
            Id = Guid.NewGuid()
;
        }

        public void AdicionarEvento(Event evento)
        {
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event evento)
        {
            _notificacoes.Remove(evento);
        }

        public void LimparEventos()
        {
            _notificacoes.Clear();
        }
    }
}
