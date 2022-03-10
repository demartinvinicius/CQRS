using CrudCQRS.DTO;
using MediatR;
using Nudes.Retornator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudCQRS.Features.Product.Queries.ById;

public class GetProductByIdRequest : IRequest<ResultOf<ProductDTO>>
{
    public int Id { get; set; }
}
