using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transfer
{
    public interface ITransferService
    {
        Task TransferAsync(TransferDTO transfer);
    }
}
