mergeInto(LibraryManager.library, {

  SaveEntern: function(date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

  LoadEntern: function() {
    player.getData().then(_date => {
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage('Yandex', 'SetPlayerInfo', myJSON);
    });
  },

});