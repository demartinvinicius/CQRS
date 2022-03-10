using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Delete;

 public partial class DeleteProductByIdRequest : IRequest<Result>
 {
    public int Id { get; set; }
 }

