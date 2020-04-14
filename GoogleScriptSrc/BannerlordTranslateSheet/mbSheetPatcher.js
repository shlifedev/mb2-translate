/* writer : shlifedev@gmail.com */
/* writer 외에 절대 허락없이 수정하지 마세요. */ 


var TARGET_TRANSLATE_SHEET_NAME = "번역시트"
var ORIGINAL_STRING_DIR_ID = "1VKBYduzfg0OxoFG4iTfJrUgCvJMa82D9";
var patchMap = [] // Id, Original, Translate, File, Module  


function WriteCSV() {
  var str = "Id\tOriginal\tTranslate\tFile\tModule\n";
  for (var key in patchMap) {
    str += patchMap[key][0] + "\t" + "'" + patchMap[key][1] + "\t" + "'" + patchMap[key][2] + "\t" + patchMap[key][3] + "\t" + patchMap[key][4] + "\n";
  }
  var csv = str;

  var temp = DriveApp.getFilesByName("bannerlord_temporaryData.csv");
  if (temp.hasNext()) {
    var file = temp.next();
    file.setContent(csv);
    var link = file.getDownloadUrl()
    showDownload(link);
  }
  else {
    var file = DriveApp.createFile("bannerlord_temporaryData" + ".csv", csv);
    var link = file.getDownloadUrl()
    showDownload(link);
  }
}


/*
 
*/
function PatchAppendRow() {
  var ui = SpreadsheetApp.getUi();
  if (Session.getActiveUser().getEmail() != "shlifedev@gmail.com") {
    ui.alert("패치 권한이 없습니다. 관리자에게 요청하세요.");
    return;
  }
  var response = ui.alert("실행 전 주의사항!", "해당 작업은 시간이 오래 걸릴 수 있습니다. 실행하시겠습니까?", ui.ButtonSet.YES_NO);
  if (response == ui.Button.YES) {
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
        append_str = getNotExistIDDictonary(file, folder.getName(), patchAppendMap);
      }
    }

    if (append_str == null || append_str.length == 0) {
      ui.alert("패치 할 데이터가 없습니다. 최신버전입니다.");
      return;
    }
    for (var i = 0; i < sheets.length; i++) {
      var name = sheets[i].getSheetName();
      if (name == "번역시트_1239_old") {
        var sheet = sheets[i];
        var rowCount = sheet.getLastRow(); // 현재 세로위치  
        sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setValues(append_str);
        sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setBackground("blue");
        sheet.getRange(rowCount + 1, 1, append_str.length, append_str[0].length).setFontColor("white");
      }
    }

    ui.alert("패치가 완료되었습니다.");
  }
}

function Patch() {
  var ui = SpreadsheetApp.getUi();
  var response = ui.alert("실행 전 주의사항!", "해당 작업은 시간이 오래 걸릴 수 있습니다. 실행하시겠습니까?", ui.ButtonSet.YES_NO);
  if (response == ui.Button.YES) {
    patchMap = [];
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
        var textBlob = file.getAs(MimeType.PLAIN_TEXT);
        var text = textBlob.getDataAsString("UTF-8").replace("﻿", "");
        var doc = XmlService.parse(text);
        /* xml real read */
        var root = doc.getRootElement();


        /* read language config */
        var tag = root.getChild("tags").getChildren()[0];
        var lang = tag.getAttribute("language").getValue();

        /* read string child */
        var strings = root.getChild("strings");
        var stringsChildren = strings.getChildren();

        for (var i = 0; i < stringsChildren.length; i++) {
          var id = stringsChildren[i].getAttribute("id").getValue();
          var text = stringsChildren[i].getAttribute("text").getValue();
          /* create temporary data */
          var data = [id, "", "", file.getName(), folder.getName()];
          // 데이터가 null이아니면 dic에있는 데이터에 바인딩한다.
          if (patchMap[id] != null) {
            data[1] = patchMap[id][1];
            data[2] = patchMap[id][2];
          }
          if (lang == "한국어") {
            data[2] = text;
          }
          if (lang == "English") {
            data[1] = text;
          }
          patchMap[id] = data;
        }
      }
    }

    ui.alert("start xml patch");
    var sheets = SpreadsheetApp.getActive().getSheets();
    for (var i = 0; i < sheets.length; i++) {
      var name = sheets[i].getSheetName();
      if (name == TARGET_TRANSLATE_SHEET_NAME) {
        var target = sheets[i];
        var rowCount = target.getLastRow(); // 세로
        var colCount = target.getLastColumn() // 가로 
        var allRange = target.getRange(1, 1, rowCount, colCount);
        var allValues = allRange.getValues();

        for (var row = 0; row < rowCount; row++) {
          var id = allValues[row][0];
          var text = allValues[row][2];
          if (patchMap[id] != null) {
            patchMap[id][2] = text;
          }
        }
      }
    }
    WriteCSV();
  }
} 