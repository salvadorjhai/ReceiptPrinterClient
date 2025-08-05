# ReceiptPrinterClient

```csharp
// sample usage (PDF) (send as byte)
var pdf = document.GeneratePdf(); // this is a byte[] , generated from QuestPDF
using var cl = new System.Net.WebClient();
cl.Headers.Add("Content-Type", "application/pdf");
cl.UploadData($"http://192.168.3.246:62223/", pdf);
     
// sample usage (pos/raw byte) (send as byte) (tested only with epson tm-t82x)
using var cl = new System.Net.WebClient();
cl.Headers.Add("Content-Type", "application/octet-stream");
cl.UploadData(printerLocation, data);
```