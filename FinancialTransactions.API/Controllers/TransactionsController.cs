using FinancialTransactions.Application.DTOs;
using FinancialTransactions.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransactions.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var transactions = await _transactionService.GetActiveAsync(cancellationToken);
        return Ok(transactions);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.GetByIdAsync(id, cancellationToken);
        if (transaction is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Transaction not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto, CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionDto dto, CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.UpdateAsync(id, dto, cancellationToken);
        if (transaction is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Transaction not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        return Ok(transaction);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _transactionService.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Transaction not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        return NoContent();
    }
}
