window.downloadFile = function (fileName, byteBase64) {
    var link = document.createElement('a');
    link.href = 'data:application/pdf;base64,' + byteBase64;
    link.download = fileName;
    link.click();
}
