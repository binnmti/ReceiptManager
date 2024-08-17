using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ReceiptManager.Model
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = "";

        //OCR
        public string OcrText { get; set; } = "";
        public string StoreName { get; set; } = "";     //店名
        public int Amaount { get; set; }                //金額
        public DateTime TransactionDate { get; set; }   //取引日
        // タイムスタンプ
        public DateTime Timestamp { get; set; }
        public string Number { get; set; } = "";

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
