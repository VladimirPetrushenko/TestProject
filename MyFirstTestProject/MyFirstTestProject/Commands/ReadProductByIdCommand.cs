using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyFirstTestProject.Data;
using MyFirstTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstTestProject.Commands
{
    [UsedImplicitly]
    public class ReadProductByIdCommand : IRequest<Product>
    {
        public int Id { get; set; }

        public class ReadProductCommandHandler : IRequestHandler<ReadProductByIdCommand, Product>
        {
            private readonly IRepository<Product> _repository;

            public ReadProductCommandHandler(IRepository<Product> repository)
            {
                _repository = repository ?? throw new ArgumentException(null, nameof(repository));
            }

            public Task<Product> Handle(ReadProductByIdCommand request, CancellationToken cancellationToken)
            {
                var product = _repository.GetByID(request.Id);

                return Task.FromResult(product);
            }
        }
    }
}
