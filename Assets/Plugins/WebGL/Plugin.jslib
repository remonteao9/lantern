
mergeInto(LibraryManager.library, {
  ShowAlert: function (msgPtr) {
    const msg = UTF8ToString(msgPtr);
    alert("Unity says: " + msg);
  },

  LogToConsole: function (msgPtr) {
    const msg = UTF8ToString(msgPtr);
    console.log("[From Unity]:", msg);
  }
});
