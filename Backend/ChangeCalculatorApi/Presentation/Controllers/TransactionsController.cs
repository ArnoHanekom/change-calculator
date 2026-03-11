using Application.Dtos;
using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(TransactionService transactionService, ICurrencyProvider currencyProvider) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync([FromBody]TransactionRequestDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var transaction  = await transactionService.CreateTransactionAsync(request, cancellationToken);            
            return Ok(TransactionMapper.ToResponseDto(transaction));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionHistoryAsync(CancellationToken cancellationToken = default)
    {
        var history = await transactionService.GetAllTransactionHistoryAsync(cancellationToken);
        return Ok(history.Select(TransactionMapper.ToResponseDto));
    }

    [HttpGet("currencies")]
    public async Task<IActionResult> GetCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        var currencies = await currencyProvider.GetCurrenciesAsync(cancellationToken);
        return Ok(currencies.Keys);
    }
}