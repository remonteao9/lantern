
mergeInto(LibraryManager.library, {
  ShowAlert: function (msgPtr) {
    const msg = UTF8ToString(msgPtr);
    alert("Unity says: " + msg);
  },

  LogToConsole: function (msgPtr) {
    const msg = UTF8ToString(msgPtr);
    console.log("[From Unity]:", msg);
  },

  SetSceneName: function (sceneNamePtr, imgPathPtr, titlePtr, summaryPtr) {
    const sceneName = UTF8ToString(sceneNamePtr);
    const imgPath = 'images/thumb/' + UTF8ToString(imgPathPtr);
    const title = UTF8ToString(titlePtr);
    const summary = UTF8ToString(summaryPtr);
    const item = {
        title,
        sceneName,
        imgPath,
        summary
    };
    if (window.cardItems) {
        window.cardItems.push(item);
    }
    else {
        window.cardItems = [item];
    }
  },
  
  UpdateContent: function (now) {
    const activeSceneName = UTF8ToString(now);
    const gameTitle = document.getElementById("game-title");
    const gameSummary = document.getElementById("game-summary");
    const container = document.getElementById("rightColumn");

    const items = window.cardItems;
    if (!items) return;

    container.innerHTML = "";

    items.forEach((item) => {
      if (item.sceneName == activeSceneName) {
        gameTitle.textContent = item.title;
        gameSummary.innerHTML = item.summary;
        return; 
      }
      const card = document.createElement("div");
      card.className = "item-card";

      card.innerHTML = `
        <img class="item-thumb" src="${item.imgPath}" alt="${item.title}">
        <div class="item-info">
          <div class="item-title">${item.title}</div>
          <div class="item-desc">${item.sceneName}</div>
        </div>
    ` ;

      container.appendChild(card);

      card.addEventListener("click", () => {
        SendMessage("WebBridge", "LoadScene", item.sceneName);
      })
    });
  },

  
  UpdateGameItems: function () {
    const scrollList = document.getElementById("scrollList");

    const gameItems = [
      "images/item/item.png",
      "images/item/item.png",
      "images/item/item.png",
      "images/item/item.png"
    ];

    gameItems.forEach((src) => {
      const item = document.createElement("div");
      item.className = "scroll-item";

      const img = document.createElement("img");
      img.src = src;
      img.alt = "item";

      item.appendChild(img);
      scrollList.appendChild(item);
    });

    const count = gameItems.length;
    let scale = 1;
    if (count > 6 && count <= 12) scale = 0.9;
    if (count > 12) scale = 0.8;

    scrollList.style.transform = `scale(${scale})`;
    scrollList.style.transformOrigin = "center center";
  },

  UpdateItemText: function(itemNamePtr, itemIdPtr, sceneNamePtr) {
    const itemName = UTF8ToString(itemNamePtr);
    const itemId = UTF8ToString(itemIdPtr);
    const sceneName = UTF8ToString(sceneNamePtr);
    const gameSummary = document.getElementById("game-summary");

    gameSummary.innerHTML = gameSummary.innerHTML.replace(
        new RegExp(`(${itemName})`, "g"),
        `<button id="${itemId}" class="word-button">$1</button>`
    );
    setTimeout(() => {
        const btn = document.getElementById(itemId);
        console.log(btn);
        btn.addEventListener("click", () => {
            SendMessage("WebBridge", "SetGameItem", `${itemId}+${sceneName}` );
        });
    }, 0);
  }
});
