/* writer : shlifedev@gmail.com */
/* writer 외에 절대 허락없이 수정하지 마세요. */
function ToCSV(sheetName) {
  var sheets = SpreadsheetApp.getActive().getSheets();
  var csv = "";
  for (var i = 0; i < sheets.length; i++) {
    var name = sheets[i].getSheetName();
    if (name == sheetName) {
      var target = sheets[i];
      var rowCount = target.getLastRow(); // 세로
      var colCount = target.getLastColumn() // 가로 
      var allRange = target.getRange(1, 1, rowCount, colCount);
      var allValues = allRange.getValues();
      var resultCSV = "";
      /* read csv id values */
      for (var col = 0; col < colCount; col++) {

        var cellString = allValues[0][col];
        csv += cellString;
        if (col != colCount - 1)
          csv += ",";
        else
          csv += "\n";
      }

      for (var row = 1; row < rowCount; row++) {
        for (var col = 0; col < colCount; col++) {
          var cellString = allValues[row][col];
          csv += cellString;

          if (col != colCount - 1)
            csv += ",";
          else
            csv += "\n";
        }
      }
    }
  }

  return csv;
}