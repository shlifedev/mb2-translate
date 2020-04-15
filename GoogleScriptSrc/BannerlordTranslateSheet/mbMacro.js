 


function WriteInspector() {
  var ui = SpreadsheetApp.getUi();
  var b = SpreadsheetApp.getActive().getSheetName() == "번역시트";
  if (!b) return;
  var response = ui.prompt("제보 (" + filter + ")", "설명을 적어주세요\n ex) 번역기로 의심됩니다.\n ex) ~~랑~~하는 퀘스트인데 번역이 안되어있습니다.", ui.ButtonSet.YES_NO);
}
function Report(filter) {
  var ui = SpreadsheetApp.getUi();
  var b = SpreadsheetApp.getActive().getSheetName() == "번역시트";
  if (!b) return;
  var response = ui.prompt("제보 (" + filter + ")", "설명을 적어주세요\n ex) 번역기로 의심됩니다.\n ex) ~~랑~~하는 퀘스트인데 번역이 안되어있습니다.", ui.ButtonSet.YES_NO);
  if (response.getSelectedButton() == ui.Button.YES) {
    var p = SpreadsheetApp.getActive().getCurrentCell();
    var rowPos = p.getRow();
    var colPos = p.getColumn();
    if (colPos == 3) {
      var reason = response.getResponseText();
      var transSheet = SpreadsheetApp.getActive().getSheetByName("번역시트")
      var id = transSheet.getRange(rowPos, colPos - 2).getValue();
      var original = transSheet.getRange(rowPos, colPos - 1).getValue();
      var translate = transSheet.getRange(rowPos, colPos).getValue();
      var reportSheet = SpreadsheetApp.getActive().getSheetByName("리포트");
      reportSheet.appendRow([id, "구현중", original, translate, Session.getActiveUser().getEmail(), reason, filter]);

    }
    else {
      ui.alert("한글 번역을 클릭한 후 해야합니다.");
    }
  }
  else {

  }
}


function Inspect(filter) {
  var ui = SpreadsheetApp.getUi();
  var b = SpreadsheetApp.getActive().getSheetName() == "번역시트";
  if (!b) return;
  var p = SpreadsheetApp.getActive().getCurrentCell();
  var rowPos = p.getRow();
  var colPos = p.getColumn();
  if (colPos == 3) {

    var transSheet = SpreadsheetApp.getActive().getSheetByName("번역시트")
    var id = transSheet.getRange(rowPos, colPos - 2).getValue();
    var original = transSheet.getRange(rowPos, colPos - 1).getValue();
    var translate = transSheet.getRange(rowPos, colPos).getValue();
    var reportSheet = SpreadsheetApp.getActive().getSheetByName("리포트");
    var response = ui.prompt("검수 :" + id, "영어 원문\n" + original + "\n\n번역 셀 내용 :\n" + translate, ui.ButtonSet.YES_NO);
    if (response.getSelectedButton() == ui.Button.YES) {

      var value = response.getResponseText();
      p.setValue(value);
      transSheet.getRange(rowPos, colPos + 5).setValue(Session.getActiveUser().getEmail());
    }
  }
  else {

  }
}

function Report_Translator() {
  Report("번역기 의심");
}

function Report_MisTranslate() {
  Report("오역 제보");
}

function Report_RequestTranslate() {
  Report("번역 요청");
}
