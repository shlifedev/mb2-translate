/* writer : shlifedev@gmail.com */
/* writer 외에 절대 허락없이 수정하지 마세요. */


var TARGET_TRANSLATE_SHEET_NAME = "번역시트"
var ORIGINAL_STRING_DIR_ID = "1VKBYduzfg0OxoFG4iTfJrUgCvJMa82D9";
var patchMap = [] // Id, Original, Translate, File, Module     

 /**
 * @param {GoogleAppsScript.Spreadsheet.Sheet} targetSheet 시트기준으로 패치
 */ 
function processingPatch(targetSheet) {
   
  var ui = SpreadsheetApp.getUi();
  if (Session.getActiveUser().getEmail() != "shlifedev@gmail.com") {
    ui.alert("패치 권한이 없습니다. 관리자에게 요청하세요.");
    return;
  }
  var added = false;

  //요청 
  var response = ui.alert("실행 전 주의사항!", 
  "해당 작업은 시간이 오래 걸릴 수 있습니다. 실행하시겠습니까?", 
  ui.ButtonSet.YES_NO);
 
 
  //요청 동의
  if (response == ui.Button.YES) {
    var patchAppendMap = [];
    var append_str = [];
    var target = targetSheet;
    var rowCount = target.getLastRow(); // 세로
    var colCount = target.getLastColumn(); // 가로 
    var allRange = target.getRange(1, 1, rowCount, colCount);
    var allValues = allRange.getValues();
    for (var row = 1; row < rowCount; row++) {
      var data = [allValues[row][0], allValues[row][1], allValues[row][2], allValues[row][3], allValues[row][4]]
      patchAppendMap[allValues[row][0]] = data;
    }


    var stringFolder = DriveApp.getFolderById(ORIGINAL_STRING_DIR_ID).getFolders();
    while (stringFolder.hasNext()) {
      var folder = stringFolder.next();
      var files = folder.getFiles();
      /* read xml's */
      var count = 0;
      while (files.hasNext()) {
        /* open xml from file */
        var blobs = []; // Array for attachment.
        var file = files.next();
        var data = getNotExistIDDictonary(file, folder, patchAppendMap);
        if (data.length != 0) added = true;
        for (v in data) {
          append_str[v] = data[v];
        }
      }
    }


    var sheet = targetSheet;
    var rowCount = sheet.getLastRow(); // 현재 세로위치  
    sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setValues(append_str);
    sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setBackground("blue");
    sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setFontColor("white");


    if (added == false) {
      ui.alert("패치가 완료되었습니다.");
    }
    else {
      ui.alert("패치 할 데이터가 없습니다.");
    }
  }
}



 

function PatchAppendRow() {
  var ui = SpreadsheetApp.getUi();
  if (Session.getActiveUser().getEmail() != "shlifedev@gmail.com") {
    ui.alert("패치 권한이 없습니다. 관리자에게 요청하세요.");
    return;
  }
  var added = false;
  var response = ui.alert("실행 전 주의사항!", "해당 작업은 시간이 오래 걸릴 수 있습니다. 실행하시겠습니까?", ui.ButtonSet.YES_NO);
  if (response == ui.Button.YES) {
  
    logSideMenu("Reading Sheet...", "PatchLog");
  
    var patchAppendMap = [];
    //시트부터 읽는다. 
    var sheets = SpreadsheetApp.getActive().getSheets();
    var append_str = []; 
    for (var i = 0; i < sheets.length; i++) {
      var name = sheets[i].getSheetName();
      if (name == "번역시트_1239_old") {
        var target = sheets[i];
        var rowCount = target.getLastRow(); // 세로
        var colCount = target.getLastColumn(); // 가로 
        var allRange = target.getRange(1, 1, rowCount, colCount);
        var allValues = allRange.getValues();
        for (var row = 1; row < rowCount; row++) {
          var data = [allValues[row][0], allValues[row][1], allValues[row][2], allValues[row][3], allValues[row][4]]
          patchAppendMap[allValues[row][0]] = data;
        }
      }
    }

    logSideMenu("Reading XML...<br> 잠시만 기다려주세요.", "PatchLog");

    var stringFolder = DriveApp.getFolderById(ORIGINAL_STRING_DIR_ID).getFolders();
    while (stringFolder.hasNext()) {
      var folder = stringFolder.next();
      var files = folder.getFiles();
      /* read xml's */
      var count = 0;
      while (files.hasNext()) {
        /* open xml from file */
        var blobs = []; // Array for attachment.
        var file = files.next(); 
        var data = getNotExistIDDictonary(file, folder, patchAppendMap); 
        for (v in data) {
          append_str.push([data[v][0],data[v][1],data[v][2],data[v][3],data[v][4]]);
        }
      }
    }

 
    logSideMenu("Reading XML...<br> Append ..", "PatchLog");
    for (var i = 0; i < sheets.length; i++) {
      var name = sheets[i].getSheetName();
      if (name == "번역시트_1239_old") {
        var sheet = sheets[i];
        var rowCount = sheet.getLastRow(); // 현재 세로위치  
        logSideMenu("Reading XML...<br> setValues ..", "PatchLog");
        sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setValues(append_str);
        logSideMenu("Reading XML...<br> setBackground ..", "PatchLog");
        sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setBackground("blue");
        logSideMenu("Reading XML...<br> setFontColor ..", "PatchLog");
        sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setFontColor("white");
      }
    } 
    logSideMenu("<b>패치가 완료되었습니다!</b><br>파란색으로 추가된 행이 새로운 데이터입니다.", "패치 완료 알림");
  }
}  