/* writer : shlifedev@gmail.com */
/* writer 외에 절대 허락없이 수정하지 마세요. */
 
function onOpen(e) {
  var ui = SpreadsheetApp.getUi();
  var menu = SpreadsheetApp.getUi().createMenu("●마공카 매직툴●");
  menu.addItem('문의', 'showContactMsg').addToUi()
  menu.addItem('매직툴 개요', 'introduction').addToUi()
  menu.addItem('공식번역 패치', 'PatchAppendRow').addToUi()
  menu.addItem('모드번역 패치', 'PatchAppendRow').addToUi()  
  menu.addItem('XML 다운로드', 'downloadTranslateSheetToXML').addToUi()
  ui.alert("마공카 한글패치 번역시트 입니다. \n 시트를 고의로 훼손하거나 고의적으로 번역기를 돌린 텍스트를 넣지 마세요. 모든 수정 기록이 남습니다. \n\n 번역에 노력해주시는 번역가님들 너무 감사합니다. 오늘도 좋은 하루 되시길 바랍니다. \n 모드/번역의 출처 https://cafe.naver.com/warband\n 시트 관련 문제가 있을경우 shlifedev@gmail.com 이슈를 알려주세요.\n\n 마공카 관리자 허락없이 시트 데이터 무단 배포 절대 금지합니다.")
} 

function logSideMenu(html, title)
{ 
  var ui = SpreadsheetApp.getUi();
  var htmlOutput = HtmlService
  .createHtmlOutput(html)
  .setTitle(title);

  var side = ui.showSidebar(htmlOutput);  
}

function showDownload(downloadLink) {
  var html = Utilities.formatString('<span style="background-color: #2b2301; color: #fff; display: inline-block; padding: 3px 10px; font-weight: bold; border-radius: 5px;"><a href="%s"><font color=white>출력물 다운로드 하기</font></a></span> 버튼 미동작시 다운로드 링크 : %s', downloadLink, downloadLink);
  var userInterface = HtmlService.createHtmlOutput(html);
  SpreadsheetApp.getUi().showModalDialog(userInterface, "결과 출력물 다운로드")
}

function showContactMsg() {

  var ui = SpreadsheetApp.getUi();
  ui.alert("시트 툴 개발자 연락처 : shlifedev@gmail.com \n버그나 문제가 있을경우 연락처로 메일을 보내주세요. \n개발자가 회신하지 못할경우 다시 보내주세요.\n 공식 카페 https://cafe.naver.com/warband/")


}

function introduction() {
  var ui = SpreadsheetApp.getUi();
  ui.alert("반갑습니다. 마운트 앤 블레이드 공식 번역 시트입니다. \n 언제나 수고해주시는 번역가님들에게 감사드립니다.")
} 
