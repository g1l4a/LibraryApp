using Microsoft.AspNetCore.Mvc;
using LibraryApp.Data;
using LibraryApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowRecordsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/BorrowRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecord>>> GetBorrowRecords()
        {
            return await _context.BorrowRecords.ToListAsync();
        }

        // GET: api/BorrowRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowRecord>> GetBorrowRecord(int id)
        {
            var borrowRecord = await _context.BorrowRecords.FindAsync(id);

            if (borrowRecord == null)
            {
                return NotFound();
            }

            return borrowRecord;
        }

        // POST: api/BorrowRecords
        [HttpPost]
        public async Task<ActionResult<BorrowRecord>> CreateBorrowRecord(BorrowRecord borrowRecord)
        {
            _context.BorrowRecords.Add(borrowRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrowRecord), new { id = borrowRecord.Id }, borrowRecord);
        }

        // PUT: api/BorrowRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBorrowRecord(int id, BorrowRecord borrowRecord)
        {
            if (id != borrowRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(borrowRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BorrowRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowRecord(int id)
        {
            var borrowRecord = await _context.BorrowRecords.FindAsync(id);
            if (borrowRecord == null)
            {
                return NotFound();
            }

            _context.BorrowRecords.Remove(borrowRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorrowRecordExists(int id)
        {
            return _context.BorrowRecords.Any(e => e.Id == id);
        }
    }
}
