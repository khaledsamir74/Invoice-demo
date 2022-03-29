
function OpenPdfNewTab(filename, byteBase64) {
    var blob = base64Blob(byteBase64);
    var blobURL = URL.createObjectURL(blob);
    window.open(blobURL);
}

function base64Blob(b64Data) {
    sliceSize = 512;
    var byteCahacters = atob(b64Data);
    var byteArrays = [];
    for (var offset = 0; offset < byteCahacters.length; offset += sliceSize) {
        var slice = byteCahacters.slice(offset, offset + sliceSize);
        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }
    var blob = new Blob(byteArrays, { type: 'application/pdf' });
    return blob;
}