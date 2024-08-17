using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceiptManager.Model;

namespace ReceiptManager.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly FormRecognizerService _FormRecognizerService;
        private readonly AzureBlobStorageService _AzureBlobStorageService;
        

        public ReceiptsController(ApplicationDbContext context, 
            FormRecognizerService formRecognizerService,
            AzureBlobStorageService azureBlobStorageService)
        {
            _context = context;
            _FormRecognizerService = formRecognizerService;
            _AzureBlobStorageService = azureBlobStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadReceipt([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var stream = file.OpenReadStream();
            await _AzureBlobStorageService.UploadFileAsync(file.Name, stream);
            var recognitionResult = await _FormRecognizerService.RecognizeImageAsync(file.Name);
            var receipt = new Receipt
            {
                UserId = "currentUserId",  // Replace with actual user ID
                Timestamp = DateTime.UtcNow,
                OcrText = recognitionResult
            };
            _context.Receipt.Add(receipt);
            await _context.SaveChangesAsync();

            return Ok(receipt);
        }


    }
}
