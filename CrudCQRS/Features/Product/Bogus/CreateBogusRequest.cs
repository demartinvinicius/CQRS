using CrudCQRS.DTO;
using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Bogus;

public class CreateBogusRequest : IRequest<ResultOf<List<ProductDTO>>>
{
    public int? NumberItensToGenerate { get; set; }
    public bool? EraseDatabase { get; set; }
}
