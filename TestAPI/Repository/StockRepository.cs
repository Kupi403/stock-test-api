using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Dtos.Stock;
using TestAPI.Helpers;
using TestAPI.Interfaces;
using TestAPI.Models;

namespace TestAPI.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _ctx;
        public StockRepository(ApplicationDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _ctx.Stocks.AddAsync(stockModel);
            await _ctx.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var stockModel = await _ctx.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) return null;

            _ctx.Stocks.Remove(stockModel);
            await _ctx.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _ctx.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));

            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            if (query.PageNumber <= 0) query.PageNumber = 1;


            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _ctx.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> StockExists(int id)
        {
            return _ctx.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockRequestDto updateDto)
        {
            var existingStock = await _ctx.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null) return null;

            existingStock.Symbol = updateDto.Symbol;
            existingStock.CompanyName = updateDto.CompanyName;
            existingStock.Purchase = updateDto.Purchase;
            existingStock.LastDividend = updateDto.LastDividend;
            existingStock.Industry = updateDto.Industry;
            existingStock.MarketCap = updateDto.MarketCap;

            await _ctx.SaveChangesAsync();

            return existingStock;
        }
    }
}
