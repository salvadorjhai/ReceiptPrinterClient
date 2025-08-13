# ReceiptPrinterClient

using .net (within local network)
```csharp
// sample usage (PDF) (send as byte)
var pdf = document.GeneratePdf(); // this is a byte[] , generated from QuestPDF
using var cl = new System.Net.WebClient();
cl.Headers.Add("Content-Type", "application/pdf");
cl.UploadData($"http://192.168.3.246:62223/", pdf);
     
// sample usage (pos/raw byte) (send as byte) (tested only with epson tm-t82x)
using var cl = new System.Net.WebClient();
cl.Headers.Add("Content-Type", "application/octet-stream");
cl.UploadData($"http://192.168.3.246:62223/", data);
```

using javascript 
```js
function base64ToBytes(data) {
    const binaryString = atob(data);
    const len = binaryString.length;
    const bytes = new Uint8Array(len);
    for (let i = 0; i < len; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }
    return bytes;
}

function sendToReceiptPrinter(bytes, printReceiptAddr = "http://localhost:62223/") {
    fetch(printReceiptAddr, {
        method: 'POST',
        headers: { 'Content-Type': 'application/pdf' }, // change to application/octet-stream if esc/pos commands
        body: bytes
    });
}

// ...
// from blob (via ajax) (pdf)
blob.arrayBuffer().then(buffer => {
    const bytes = new Uint8Array(buffer);
    sendToReceiptPrinter(bytes)
});

// or from base64string (pdf)
const bytes = base64ToBytes(pdfBase64);
sendToReceiptPrinter(bytes)
```
