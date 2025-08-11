using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SayacApi.Data;
using SayacApi.Models;
using System.Diagnostics.Metrics;


namespace SayacApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SayacController : ControllerBase
    {
        //
        private readonly UygDbContext _dbContext;

        // Bir readonly'nin değerini iki yerde verebilirsiniz ilki constructor, diğeri tanımlandığı yerdir.
        // Ayrıca readonly ifadeler bir kez set edilebilir Kaynakça ; https://medium.com/kodcular/entity-framework-ile-asp-net-core-web-api-crud-i̇şlemleri-1236111ef463
        public SayacController(UygDbContext dbContext) // Constructor
        {
            _dbContext = dbContext;
        }

        [HttpGet]  //Listeleme metodu
        public async Task<ActionResult<IEnumerable<Sayac>>> SayaclarıListele()
        {
            return await _dbContext.Sayaclar.ToListAsync();
        }

        [HttpGet("{id}")] //id'ye göre listeleme metodu

        public async Task<ActionResult<Sayac>> SayacListele(int id)
        {
            var sayac = await _dbContext.Sayaclar.FindAsync(id);
            if (sayac == null)
            {
                return NotFound();
            }
            return sayac;
        }

        [HttpPost] //Yeni sayaç oluşturma-ekleme metodu

        public async Task<ActionResult<Sayac>> SayacOlustur(Sayac sayac)
        {
            if(sayac == null)
                return BadRequest();
            _dbContext.Sayaclar.Add(sayac);
            await _dbContext.SaveChangesAsync();

            return Ok(sayac);
            
        }

        [HttpPut("{id}")] //Sayaç güncellemek için metod

        public async Task<IActionResult> SayacGuncelle(int id, Sayac sayac)
        {
            if (id != sayac.Id)
                return BadRequest();
    
            _dbContext.Entry(sayac).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(sayac);
        }

        [HttpDelete("{id}")] //Sayaç silmek için metod

        public async Task<IActionResult> SayacSil(int id)
        {
            var sayac = await _dbContext.Sayaclar.FindAsync(id);
            if(sayac == null)
                return NotFound();
            _dbContext.Sayaclar.Remove(sayac);
            await _dbContext.SaveChangesAsync();
            return Ok(sayac);
        }
    }
}
