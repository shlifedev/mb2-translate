 
/* writer : shlifedev@gmail.com */
/* writer 외에 절대 허락없이 수정하지 마세요. */
function showDownload(downloadLink) {
  var html = Utilities.formatString('<span style="background-color: #2b2301; color: #fff; display: inline-block; padding: 3px 10px; font-weight: bold; border-radius: 5px;"><a href="%s"><font color=white>출력물 다운로드 하기</font></a></span> 버튼 미동작시 다운로드 링크 : %s', downloadLink, downloadLink);
  var userInterface = HtmlService.createHtmlOutput(html);
  SpreadsheetApp.getUi().showModalDialog(userInterface, "결과 출력물 다운로드")
}

function showContactMsg() {
  ui.alert("시트 툴 개발자 연락처 : shlifedev@gmail.com \n버그나 문제가 있을경우 연락처로 메일을 보내주세요. \n개발자가 회신하지 못할경우 다시 보내주세요.\n 공식 카페 https://cafe.naver.com/warband/")
}

function introduction() {
  ui.alert("반갑습니다. 마운트 앤 블레이드 공식 번역 시트입니다. \n 언제나 수고해주시는 번역가님들에게 감사드립니다.")
} 