mergeInto(LibraryManager.library, {

  SaveEntern: function(date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setStats(myobj);
  },

  LoadEntern: function() {
    player.getStats().then(_date => {
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage('Yandex', 'SetPlayerInfo', myJSON);
    });
  },

});
