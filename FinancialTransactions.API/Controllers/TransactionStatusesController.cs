using FinancialTransactions.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransactions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TransactionStatusesController : ControllerBase
{
    private readonly ITransactionStatusService _transactionStatusService;

    public TransactionStatusesController(ITransactionStatusService transactionStatusService)
    {
        _transactionStatusService = transactionStatusService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var statuses = await _transactionStatusService.GetAllAsync(cancellationToken);
        return Ok(statuses);
    }
}
