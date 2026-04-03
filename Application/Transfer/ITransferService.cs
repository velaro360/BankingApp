using Application.DTO;

namespace Application.Transfer
{
    public interface ITransferService
    {
        Task TransferAsync(TransferDTO transfer);
    }
}
