/* writer : shlifedev@gmail.com */
/* writer 외에 절대 허락없이 수정하지 마세요. */
var ui = SpreadsheetApp.getUi();
function onOpen(e) {
  var menu = SpreadsheetApp.getUi().createMenu("●마공카 매직툴●");
  menu.addItem('문의', 'showContactMsg').addToUi()
  menu.addItem('매직툴 개요', 'introduction').addToUi()
  menu.addItem('공식번역 패치', 'PatchAppendRow').addToUi()
  menu.addItem('모드번역 패치', 'PatchAppendRow').addToUi()
  ui.alert("마공카 한글패치 번역시트 입니다. \n 시트를 고의로 훼손하거나 고의적으로 번역기를 돌린 텍스트를 넣지 마세요. 모든 수정 기록이 남습니다. \n\n 번역에 노력해주시는 번역가님들 너무 감사합니다. 오늘도 좋은 하루 되시길 바랍니다. \n 모드/번역의 출처 https://cafe.naver.com/warband\n 시트 관련 문제가 있을경우 shlifedev@gmail.com 이슈를 알려주세요.\n\n 마공카 관리자 허락없이 시트 데이터 무단 배포 절대 금지합니다.")
} 