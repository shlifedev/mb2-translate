
function getNotExistIDDictonary(driveXmlFile, folder, patchAppendMap) {
    var dictionary = [] 
    var textBlob = driveXmlFile.getAs(MimeType.PLAIN_TEXT); 
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
        if (patchAppendMap[id] == null) {
            var text = stringsChildren[i].getAttribute("text").getValue();
            ui.alert("add str");  
            var addstr = [id, text, text, driveXmlFile.getName(), folder];
            dictionary.push(addstr)
        }
    }
   
    return dictionary;
    
}

 
function test22y()
{

}