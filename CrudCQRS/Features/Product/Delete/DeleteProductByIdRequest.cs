using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Delete;

 public partial class DeleteProductByIdRequest : IRequest<ResultOf<int>>
 {
    public int Id { get; set; }
 }

