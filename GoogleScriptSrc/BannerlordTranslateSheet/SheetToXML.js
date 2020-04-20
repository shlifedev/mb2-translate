

/**
 * 
 * @param {GoogleAppsScript.Spreadsheet.Sheet} sheet 
 */ 

function replaceAll(str, searchStr, replaceStr) {
    return str.split(searchStr).join(replaceStr);
 }

function sheetToXML(sheet)
{
    
    logSideMenu("please wait..", "init..");
    var rowSize = sheet.getLastRow();
    //get id, org, trs, filename, modname
    var selectRange = sheet.getRange(2, 1, rowSize, 5);
    var values = selectRange.getValues();

    
    var root = XmlService.createElement('base'); 
    // root.setAttribute('xmlns:xsi','b');
    // root.setAttribute('xmlns:xsd','a');
    // root.setAttribute('type','string');
  


    var tags = XmlService.createElement('tags');  
    var tag = XmlService.createElement('tag').setAttribute('language', '한국어');
    tags.addContent(tag);
    root.addContent(tags);
  
    var strings = XmlService.createElement('strings');  
    root.addContent(strings) 
 

    logSideMenu("Read and Write Sheet.", "init..");
    for(var i = 0; i < rowSize; i++)
    {
        var id = values[i][0];
        var org = values[i][1];
        var trs = values[i][2];
            trs = replaceAll(trs, '\\n', '\n');
        var fName = values[i][3];
        var mName = values[i][4]; 
        var stringchild = XmlService.createElement('string')
        .setAttribute('id', id)
        .setAttribute('text', trs); 
        strings.addContent(stringchild); 
        if(i % 1000 == 0)
        {
            logSideMenu("구글시트를 XML로 변환중 :" + Math.floor((i / rowSize) * 100)+ "% 진행됨..");
        }
    }
 
    var document = XmlService.createDocument(root);
    var xml = XmlService.getPrettyFormat().format(document);    
    return xml;
}

function downloadTranslateSheetToXML()
{ 
    var sheet = SpreadsheetApp.openById('1oY5F5P-tMBj1-kryB5gR4gS4T5KrlqmDc-tHQBrQBDo');
    var transheet = sheet.getSheetByName('번역시트');    
    var xml = sheetToXML(transheet);
    logSideMenu("create download link..", "init..");
    var file = DriveApp.createFile("LatestTranslate.xml", xml);
    var url = file.getDownloadUrl();
    showDownload(url);
}
 